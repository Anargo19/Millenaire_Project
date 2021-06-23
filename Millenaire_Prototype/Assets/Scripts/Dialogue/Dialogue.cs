using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue")]
    public class Dialogue : ScriptableObject, ISerializationCallbackReceiver
    {
        [SerializeField] List<DialogueNode> nodes = new List<DialogueNode>();
        [SerializeField] bool isFirstDialogue;
        [SerializeField] bool isQuestDialogue;

        [SerializeField] Vector2 newNodeOffSet = new Vector2(250, 0);
        Dictionary<string, DialogueNode> nodeLookup = new Dictionary<string, DialogueNode>();


        private void Awake()
        {
            OnValidate();
        }


        private void OnValidate()
        {
            nodeLookup.Clear();
            foreach(DialogueNode node in GetAllNodes())
            {
                nodeLookup[node.name] = node;
            }
        }

        public bool GetIfFirstDialogue()
        {
            return isFirstDialogue;
        }
        internal bool GetIfQuestDialogue()
        {
            return isQuestDialogue;
        }

        public IEnumerable<DialogueNode> GetAllNodes()
        {
            return nodes;
        }

        public DialogueNode GetRootNode()
        {
            return nodes[0];
        }

        public IEnumerable<DialogueNode> GetAllChildren(DialogueNode node)
        {
            foreach(string childID in node.GetChildren())
            {
                if (nodeLookup.ContainsKey(childID))
                {
                    //Debug.Log(childID);
                    yield return nodeLookup[childID];
                }

            }
        }

        public IEnumerable<DialogueNode> GetPlayerChildren(DialogueNode currentNode)
        {
            foreach(DialogueNode node in GetAllChildren(currentNode))
            {
                if (node.CheckIfPlayer())
                {
                    yield return node;
                }
            }
        }


        public IEnumerable<DialogueNode> GetAIChildren(DialogueNode currentNode)
        {
            foreach (DialogueNode node in GetAllChildren(currentNode))
            {
                if (!node.CheckIfPlayer())
                {
                    yield return node;
                }
            }
        }

#if UNITY_EDITOR
        public void CreateNode(DialogueNode parent)
        {
            
            DialogueNode newNode = CreateInstance<DialogueNode>();
            newNode.name = Guid.NewGuid().ToString();
            
            Undo.RegisterCreatedObjectUndo(newNode, "Added Dialogue Node");
            if(parent != null)
            {
                parent.AddChild(newNode.name);
                if (parent.CheckIfPlayer())
                {
                    newNode.SetPlayer(false);
                }
                else
                {
                    newNode.SetPlayer(true);
                }
                newNode.SetPosition(parent.GetRect().position + newNodeOffSet);
            }
            if (AssetDatabase.GetAssetPath(this) != "")
            {
                Undo.RecordObject(this, "Added Dialogue Node");
            }
            nodes.Add(newNode);
            OnValidate();
        }
        public void DeleteNode(DialogueNode nodeToDelete)
        {
            Undo.RecordObject(this, "Deleted Dialogue Node");
            nodes.Remove(nodeToDelete);
            OnValidate();
            DeleteGhostChildren(nodeToDelete);
            Undo.DestroyObjectImmediate(nodeToDelete);
        }

        private void DeleteGhostChildren(DialogueNode nodeToDelete)
        {
            foreach (DialogueNode node in GetAllNodes())
            {
                node.RemoveChild(nodeToDelete.name);
            }
        }
#endif
        public void OnBeforeSerialize()
        {
#if UNITY_EDITOR
            if (nodes.Count == 0)
            {
                CreateNode(null);

            }

            if (AssetDatabase.GetAssetPath(this) != "")
            {
                foreach(DialogueNode node in GetAllNodes())
                {
                    if(AssetDatabase.GetAssetPath(node) == "")
                    {
                        AssetDatabase.AddObjectToAsset(node, this);
                    }
                }
            }
#endif
        }

        public void OnAfterDeserialize()
        {
        }
    }   
}


    