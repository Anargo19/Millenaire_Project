using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Dialogue
{

    public class AIConversant : MonoBehaviour
    {
        [SerializeField] Dialogue dialogue;
        [SerializeField] Dialogue questDialogue;
        [SerializeField] Dialogue questingDialogue;
        [SerializeField] Dialogue firstdialogue;
        [SerializeField] string npcName;
        [SerializeField] bool hasQuest;
        QuestStatus quest;
        bool firstMet = true;
        bool questing = false;
        /*public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (dialogue == null)
            { 
                return false;
            }
            if (Input.GetMouseButtonDown(0))
            {
                callingController.GetComponent<PlayerConversant>().StartDialogue(this, dialogue);

            }
            return true;
        }*/
        private void Awake()
        {
        }

        public string GetName()
        {
            return npcName;
        }
        public Dialogue GetDialogue()
        {
            
            if (firstMet && firstdialogue != null)
            {
                return firstdialogue;
            }
            if (hasQuest)
            {
                return questDialogue;
            }

            
            if (questing)
            {
                return questingDialogue;
            }
            return dialogue;
        }

        public void DialogueFinished(Dialogue dialogue)
        {
            if (dialogue.GetIfFirstDialogue())
            {
                firstMet = false;
            }
            if (dialogue.GetIfQuestDialogue())
            {
                hasQuest = false;
                questing = true;
            }
        }
    }
}
