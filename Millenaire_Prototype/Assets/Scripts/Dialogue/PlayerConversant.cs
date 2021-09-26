using ECM.Components;
using ECM.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPG.Dialogue
{
    public class PlayerConversant : MonoBehaviour
    {
        Dialogue currentDialogue;
        AIConversant currentConversant = null;
        [SerializeField] DialogueNode currentNode = null;
        bool currentlyChoosing = false;


        [SerializeField] string playerName;

        public event Action onConversationUpdate;

        // Start is called before the first frame update
       

       /* IEnumerator Start(Dialogue dialogue)
        {
            yield return new WaitForSeconds(2);
            StartDialogue(dialogue);
        }*/

        public void Quit()
        {
                if (!currentDialogue.GetAllChildren(currentNode).Any())
                {
                    currentConversant.DialogueFinished(currentDialogue);
                }
            
            GetComponent<BaseFirstPersonController>().enabled = true;
            GetComponent<MouseLook>().SetCursorLock(true);
            GetComponent<MouseLook>().UpdateCursorLock();
            currentDialogue = null;
            currentlyChoosing = false;
            TriggerExitAction();
            currentNode = null;
            currentConversant = null;
            onConversationUpdate();
        }

        public void StartDialogue(AIConversant conversant, Dialogue newDialogue)
        {
            GetComponent<BaseFirstPersonController>().enabled = false;
            GetComponent<MouseLook>().SetCursorLock(false);
            GetComponent<MouseLook>().UpdateCursorLock();
            currentConversant = conversant;
            currentDialogue = newDialogue;
            currentNode = currentDialogue.GetRootNode();
            TriggerEnterAction();
            onConversationUpdate();
        }

        public bool isChoosing()
        {
            
            return currentlyChoosing;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.GetText();

        }
        public string GetName()
        {
            if (currentlyChoosing)
            {
                return playerName;
            }

            return currentConversant.GetName();

        }

        private IEnumerable<DialogueNode> FilterOnConditions(IEnumerable<DialogueNode> inputNode)
        {
            foreach(var node in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            return GetComponents<IPredicateEvaluator>();
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnConditions(currentDialogue.GetPlayerChildren(currentNode));
        }

        public void Next()
        {
            int numPlayerResponses = currentDialogue.GetPlayerChildren(currentNode).Count();
            if(numPlayerResponses > 0)
            {
                currentlyChoosing = true;
                TriggerExitAction();
                onConversationUpdate();
                return;
            }
                currentlyChoosing = false;



            DialogueNode[] children = currentDialogue.GetAIChildren(currentNode).ToArray();
            TriggerExitAction();
            currentNode = children[UnityEngine.Random.Range(0,children.Length)];
            TriggerEnterAction();
            onConversationUpdate();
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            TriggerEnterAction();
            currentlyChoosing = false;
            Next();
        }

        public bool hasNext()
        {
            return FilterOnConditions(currentDialogue.GetAllChildren(currentNode)).Count() > 0;
           /* if (currentDialogue.GetAllChildren(currentNode).Any())
            {
                return true;
            }
            else
            {
                return false;
            }*/
        }

        public bool isActive()
        {
            return currentDialogue != null;
        }

        private void TriggerEnterAction()
        {
            if(currentNode != null && currentNode.GetOnEnterAction() != "")
            {
                TriggerAction(currentNode.GetOnEnterAction());
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode != null && currentNode.GetOnExitAction() != "")
            {
                TriggerAction(currentNode.GetOnExitAction());
            }
        }

        void TriggerAction(string action)
        {
            Debug.Log(currentConversant);
            if (action == "") return;

            foreach(DialogueTrigger trigger in currentConversant.GetComponents<DialogueTrigger>())
            {
                trigger.Trigger(action);
            }
        }
    }
}
