using ECM.Components;
using ECM.Controllers;
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

    public event Action onDetection;
    public event Action onNoDetection;
    AIConversant aIConversant;

    GameObject detectedObject;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //On initialise un RaycastHit pour stocker les objets détectés
        RaycastHit atRange;

        //On lance un RayCast depuis le joueur, sur une distance de 3 et devant lui
        if (Physics.Raycast(transform.GetChild(0).GetChild(0).position, transform.forward, out atRange, 5) && atRange.transform.tag == "NPC" || Physics.Raycast(transform.GetChild(0).GetChild(0).position, transform.forward, out atRange, 5) && atRange.transform.tag == "Resources")
        {
            //On dessine le trait
            Debug.DrawRay(transform.GetChild(0).GetChild(0).position, transform.TransformDirection(Vector3.forward) * atRange.distance, Color.blue);

            //On stocke l'objet détecté
            detectedObject = atRange.transform.gameObject;
            if (atRange.transform.tag == "NPC") { detectedObject.transform.LookAt(transform); }
            onDetection();
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
                    aIConversant = hit.transform.GetComponent<AIConversant>();

                    GetComponent<PlayerConversant>().StartDialogue(aIConversant, aIConversant.GetDialogue());

                    Animator animator = hit.transform.GetComponent<Animator>();
                    animator.SetInteger("Action", 59);
                    animator.SetBool("Trigger", true);
                }

                if(hit.transform.tag == "Resources")
                {
                    Debug.Log(hit.transform.GetComponent<ResourceHolder>().GetResource());
                    GetComponent<PlayerResources>().AddResources(hit.transform.GetComponent<ResourceHolder>().GetResource());
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
