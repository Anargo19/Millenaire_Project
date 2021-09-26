using ECM.Components;
using ECM.Controllers;
using NodeCanvas.DialogueTrees;
using RPG.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Interaction : MonoBehaviour
{
    [SerializeField] GameObject questLog;

    [SerializeField] Material detected;
    [SerializeField] Material notdetected;

    [SerializeField] TextMeshProUGUI interractText;
    DialogueTreeController dialogueTreeController;

    GameObject dialogueNPC;

    public event Action onDetection;
    public event Action onNoDetection;
    AIConversant aIConversant;

    GameObject detectedObject;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void UpdateMovement(bool obj)
    {

        GetComponent<BaseFirstPersonController>().enabled = true;
        GetComponent<MouseLook>().SetCursorLock(true);
        GetComponent<MouseLook>().UpdateCursorLock();
        dialogueNPC.GetComponent<DialogueListNPC>().DeleteDialogue(dialogueTreeController);

        Debug.Log(dialogueNPC.GetComponent<DialogueListNPC>().dialogueList.Count);

        dialogueNPC = null;
        dialogueTreeController = null;

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.rotation.z != 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }
        //On initialise un RaycastHit pour stocker les objets détectés
        RaycastHit atRange;

        //We launch a RayCast from the player, on a distance of 5 and in front of him
        if (Physics.Raycast(transform.GetChild(0).GetChild(0).position, transform.forward, out atRange, 5))
        {
            //We drawn the Ray on the Scene view
            Debug.DrawRay(transform.GetChild(0).GetChild(0).position, transform.TransformDirection(Vector3.forward) * atRange.distance, Color.blue);

            //Switch case to detect specifig tags
            switch (atRange.transform.tag)
            {
                case "NPC":
                    //We stock the detected object
                    detectedObject = atRange.transform.gameObject;
                    //We make the NPC look at us
                    detectedObject.transform.LookAt(transform); 
                    //We launch the Event onDetection
                    onDetection();
                    break;
                case "Resources":
                    detectedObject = atRange.transform.gameObject;
                    onDetection();
                    break;
                case "Door":
                    detectedObject = atRange.transform.gameObject;
                    interractText.text = "Open";
                    onDetection();
                    break;
                case "Chest":
                    detectedObject = atRange.transform.gameObject;
                    interractText.text = "Open";
                    onDetection();
                    break;
            }

        }
        else
        {
            onNoDetection();
            Debug.DrawRay(transform.GetChild(0).GetChild(0).position, transform.GetChild(0).GetChild(0).TransformDirection(Vector3.forward) * 3, Color.yellow);
            if (detectedObject != null)
            {
                detectedObject = null;
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            
            if (Physics.Raycast(transform.GetChild(0).GetChild(0).position, transform.forward, out hit, 5) )
            {
                if(hit.transform.tag == "NPC")
                {
                    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    Debug.Log(hit.transform.gameObject.name);
                    hit.transform.LookAt(transform);

                    GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    GetComponent<BaseFirstPersonController>().enabled = false;
                    GetComponent<MouseLook>().SetCursorLock(false);
                    GetComponent<MouseLook>().UpdateCursorLock();

                    if(hit.transform.GetComponent<DialogueListNPC>() != null) {
                        dialogueTreeController = hit.transform.GetComponent<DialogueListNPC>().GetDialogue();
                        dialogueNPC = hit.transform.gameObject;

                        dialogueTreeController.StartDialogue(UpdateMovement);
                    }
                    
                    /*aIConversant = hit.transform.GetComponent<AIConversant>();

                    GetComponent<PlayerConversant>().StartDialogue(aIConversant, aIConversant.GetDialogue());

                    Animator animator = hit.transform.GetComponent<Animator>();
                    animator.SetInteger("Action", 59);
                    animator.SetBool("Trigger", true);*/
                }

                if (hit.transform.tag == "Resources")
                {
                    ResourcesScriptable resource = hit.transform.GetComponent<ResourceHolder>().GetResource();
                    Debug.Log(resource);
                    if (resource != null) GetComponent<PlayerResources>().AddResources(resource);
                }
                if (hit.transform.tag == "Chest")
                {
                    Debug.Log("Hit Chest");
                    hit.transform.GetComponent<ChestInventory>().GetInventory();
                }

                if (hit.transform.tag == "Door")
                {
                    foreach(Transform transform in hit.transform.parent)
                    {
                        Animator animator = transform.GetComponent<Animator>();
                        Debug.Log(!animator.GetBool("Open"));
                        animator.SetBool("Open", !animator.GetBool(0));
                    }
                }
                
            }
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            bool isActiveQuest = questLog.activeSelf;
            
            questLog.SetActive(!isActiveQuest);
            questLog.GetComponent<QuestUI>().Refresh();
            if (isActiveQuest)
            {


                GameObject.FindGameObjectWithTag("Player").GetComponent<BaseFirstPersonController>().enabled = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().SetCursorLock(true);
                GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().UpdateCursorLock();
            }
            else
            {


                GameObject.FindGameObjectWithTag("Player").GetComponent<BaseFirstPersonController>().enabled = false;
                GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().SetCursorLock(false);
                GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>().UpdateCursorLock();
            }
            
        }
    }

}
