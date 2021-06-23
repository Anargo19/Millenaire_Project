using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RPG.Dialogue
{

    public class DialogueNode : ScriptableObject
    {
        [SerializeField] bool isPlayer = false;
        [SerializeField] string textDialogue;
        [SerializeField] List<string> childIDs = new List<string>();
        [SerializeField] Rect position = new Rect(0,0,200,100);
        [SerializeField] string onEnter;
        [SerializeField] string onExitAction;
        [SerializeField] bool isQuestChoice;

        public string GetText()
        {
            return textDialogue;
        }
        public bool CheckIfPlayer()
        {
            return isPlayer;
        }
        public bool QuestChoice()
        {
            return isQuestChoice;
        }
        public string GetOnEnterAction()
        {
            return onEnter;
        }
        public string GetOnExitAction()
        {
            return onExitAction;
        }
#if UNITY_EDITOR
        public void SetPlayer(bool player)
        {
            isPlayer = player;
            EditorUtility.SetDirty(this);
        }
        public void SetDialogue(string newDialogue)
        {
            if(textDialogue != newDialogue)
            {
            Undo.RecordObject(this, "Set Dialogue");
            textDialogue = newDialogue;
                EditorUtility.SetDirty(this);
            }
        }
        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "New Position");
            position.position = newPosition;
            EditorUtility.SetDirty(this);
        }
        public void AddChild(string newChild)
        {
            Undo.RecordObject(this, "Added Child");
            childIDs.Add(newChild);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string child)
        {
            Undo.RecordObject(this, "Removed Child");
            childIDs.Remove(child);
            EditorUtility.SetDirty(this);
        }
#endif
        public Rect GetRect()
        {
            return position;
        }
        public List<string> GetChildren()
        {
            return childIDs;
        }
    }
}