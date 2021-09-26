using NodeCanvas.DialogueTrees;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueListNPC : MonoBehaviour
{
    public Dictionary<DialogueTreeController, bool> dialogueList = new Dictionary<DialogueTreeController, bool>();

    public List<DialogueTreeController> dialogues = new List<DialogueTreeController>();
    public List<bool> oneShot = new List<bool>();
    // Start is called before the first frame update
    void Start()
    {
        int i = 0;
        foreach(DialogueTreeController d in dialogues)
        {
            dialogueList.Add(d, oneShot[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    

    public DialogueTreeController GetDialogue()
    {
        foreach(DialogueTreeController k in dialogueList.Keys)
        {
            return k;
        }

        return null;

    }


    public void DeleteDialogue(DialogueTreeController dialogue)
    {
        if (dialogueList[dialogue])
        {
            dialogueList.Remove(dialogue);
            Destroy(dialogue.gameObject);
        }
    }
}
