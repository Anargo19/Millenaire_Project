using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject gameManager;

    bool firstEncounter = true;
    bool hasAQuest = true;
    bool questGiven = false;

    [SerializeField] string firstEncounterDialogue;
    [SerializeField] string questAdvancementQuestion;
    [SerializeField] string questDialogue;
    [SerializeField] List<string> randomDialogue = new List<string>();

    string positiveAnswer;
    string negativeAnswer;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public (string,bool,float) GetDialogue()
    {
        if (firstEncounter)
        {
            firstEncounter = false;
            return (firstEncounterDialogue,false,0);
            
        }
        else
        {
            if (hasAQuest)
            {
                return (questDialogue,true,2);
            }
            else if (questGiven)
            {
                return (questAdvancementQuestion,true,2.3f);
            }
            else
            {
                return (randomDialogue[Random.Range(0, randomDialogue.Count)],false,0);
            }
        }
    }

    public string QuestGiveResponse(bool accept, float ID)
    {

        if (accept)
        {
            if(ID == 2)
            {
                hasAQuest = false;
                questGiven = true;
                return positiveAnswer;
            }
            else {
                return "ui";
            }



        }
        else
        {
            if (ID == 2)
            {
                hasAQuest = false;
                questGiven = false;
                return negativeAnswer;
            }
            else
            {
                return "no";
            }


        }
    }
}
