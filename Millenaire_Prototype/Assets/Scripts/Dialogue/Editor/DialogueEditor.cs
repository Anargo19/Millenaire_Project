using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace RPG.Dialogue.Editor
{
    public class DialogueEditor : EditorWindow
    {
        Dialogue selectedDialogue = null;

        [NonSerialized] GUIStyle nodeStyle;
        [NonSerialized] GUIStyle playerStyle;

        [NonSerialized] DialogueNode draggingNode = null;
        [NonSerialized] Vector2 draggingOffset;

        [NonSerialized] DialogueNode creatingNode = null;
        [NonSerialized] DialogueNode deletingNode = null;
        [NonSerialized] DialogueNode linkingParentNode = null;

        Vector2 scrollPosition;

        [NonSerialized] bool draggingView = false;
        [NonSerialized] Vector2 draggingViewOffset;

        const float canvasSize = 4000;
        const float backgroundSize = 50;

        [MenuItem("Window/Dialogue Editor")]
        public static void ShowEditorWindow()
        {
            GetWindow(typeof(DialogueEditor), false, "Dialogue Editor");
        }

        [OnOpenAsset(1)]
        public static bool OpenDialogue(int instanceID, int line)
        {
            Dialogue dialogue = EditorUtility.InstanceIDToObject(instanceID) as Dialogue;
            if(dialogue != null)
            {
                ShowEditorWindow();
                return true;
            }
            return false;
        }

        private void OnEnable()
        {
            Selection.selectionChanged += OnSelectionChanged;

            nodeStyle = new GUIStyle();
            nodeStyle.normal.background = EditorGUIUtility.Load("node0") as Texture2D;
            nodeStyle.normal.textColor = Color.white;
            nodeStyle.padding = new RectOffset(20, 20, 20, 20);
            nodeStyle.border = new RectOffset(12, 12, 12, 12);

            playerStyle = new GUIStyle();
            playerStyle.normal.background = EditorGUIUtility.Load("node1") as Texture2D;
            playerStyle.normal.textColor = Color.white;
            playerStyle.padding = new RectOffset(20, 20, 20, 20);
            playerStyle.border = new RectOffset(12, 12, 12, 12);
        }

        private void OnSelectionChanged()
        {
            Dialogue dialogue = Selection.activeObject as Dialogue;
            if (dialogue != null)
            {
                selectedDialogue = dialogue;
                Repaint();
            }
        }

        private void OnGUI()
        {
            if(selectedDialogue == null)
            {

                EditorGUILayout.LabelField("No Dialogue Selected");
            }
            else
            {
                ProcessEvent();

                scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);

                Rect canvas = GUILayoutUtility.GetRect(canvasSize, canvasSize);

                Rect texCoords = new Rect(0, 0, canvasSize/backgroundSize, canvasSize / backgroundSize);

                Texture2D backgroundTexture = Resources.Load("background") as Texture2D;

                GUI.DrawTextureWithTexCoords(canvas, backgroundTexture, texCoords);

                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawConnections(node);
                }
                foreach (DialogueNode node in selectedDialogue.GetAllNodes())
                {
                    DrawNode(node);
                }

                EditorGUILayout.EndScrollView();

                if (creatingNode != null)
                {
                    selectedDialogue.CreateNode(creatingNode);
                    creatingNode = null;
                }
                if (deletingNode != null)
                {
                    selectedDialogue.DeleteNode(deletingNode);
                    deletingNode = null;
                }
            }
        }

        private void ProcessEvent()
        {
            if (Event.current.type == EventType.MouseDown && draggingNode == null)
            {
                draggingNode = GetNodeAtPoint(Event.current.mousePosition + scrollPosition);
                if (draggingNode != null)
                {
                    draggingOffset = draggingNode.GetRect().position - Event.current.mousePosition;
                    Selection.activeObject = draggingNode;
                }
                else
                {
                    draggingView = true;
                    draggingViewOffset = Event.current.mousePosition + scrollPosition;
                    Selection.activeObject = selectedDialogue;
                }
            }
            else if (Event.current.type == EventType.MouseDrag && draggingNode != null)
            {
                Undo.RecordObject(selectedDialogue, "MoveDialogueNode");
                draggingNode.SetPosition(Event.current.mousePosition + draggingOffset);
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseDrag && draggingView)
            {
                scrollPosition = draggingViewOffset - Event.current.mousePosition;
                GUI.changed = true;
            }
            else if (Event.current.type == EventType.MouseUp && draggingNode != null)
            {
                draggingNode = null;
            }
            else if (Event.current.type == EventType.MouseUp && draggingView)
            {
                draggingView = false;
            }
        }

        

        private void DrawNode(DialogueNode node)
        {
            GUIStyle style = nodeStyle;
            if (node.CheckIfPlayer())
            {
                style = playerStyle;
            }

            GUILayout.BeginArea(node.GetRect(), style);
            node.SetDialogue(EditorGUILayout.TextField(node.GetText()));

           
            GUILayout.BeginHorizontal();
            if(GUILayout.Button("Add"))
            {
                creatingNode = node;
                
            }
            if (GUILayout.Button("Delete"))
            {
                deletingNode = node;

            }
            GUILayout.EndHorizontal();

            if(linkingParentNode == null)
            {
                if (GUILayout.Button("Link"))
                {
                    linkingParentNode = node;
                }
            }
            else 
            {
                if (linkingParentNode.GetChildren().Contains(node.name))
                {
                    if (GUILayout.Button("Unlink"))
                    {
                        linkingParentNode.RemoveChild(node.name);
                        linkingParentNode = null;
                    }
                }
                else if(linkingParentNode.name == node.name)
                {
                    if (GUILayout.Button("Cancel"))
                    {
                        linkingParentNode = null;
                    }
                }
                else
                {
                    if (GUILayout.Button("Child"))
                    {
                        linkingParentNode.AddChild(node.name);
                        linkingParentNode = null;
                    }
                }

            }
            

            GUILayout.EndArea();
        }

        private void DrawConnections(DialogueNode node)
        {
            Vector3 startPosition = new Vector3(node.GetRect().xMax, node.GetRect().center.y, 0);
            foreach (DialogueNode childNode in selectedDialogue.GetAllChildren(node))
            {
                Vector3 endPosition = new Vector3(childNode.GetRect().xMin, childNode.GetRect().center.y, 0);
                Vector3 controlPointOffset = endPosition - startPosition;
                controlPointOffset.y = 0;
                controlPointOffset.x *= 0.8f;

                Handles.DrawBezier(startPosition, endPosition, startPosition + controlPointOffset, endPosition - controlPointOffset, Color.white, null, 4f);
            }
        }
        private DialogueNode GetNodeAtPoint(Vector2 mousePosition)
        {
            DialogueNode dialogueNode = null;
            foreach (DialogueNode node in selectedDialogue.GetAllNodes())
            {
                if (node.GetRect().Contains(mousePosition))
                {
                    dialogueNode = node;
                }
            }

            return dialogueNode;
        }
    }
}