using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Dialogue;
using TMPro;
using UnityEngine.UI;
using System;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] TextMeshProUGUI AIName;
        [SerializeField] Button nextButton;
        [SerializeField] Button quitButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;

        


        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            nextButton.onClick.AddListener(() => { playerConversant.Next(); });
            quitButton.onClick.AddListener(() => { playerConversant.Quit(); } );
            playerConversant.onConversationUpdate += UpdateUI; 

            UpdateUI();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.isActive());

            if (!playerConversant.isActive())
            {
                return;
            }
            AIName.text = playerConversant.GetName();
            AIResponse.SetActive(!playerConversant.isChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.isChoosing());
            if (playerConversant.isChoosing())
            {
                BuildChoiceList();
            }
            else
            {
                AIText.text = playerConversant.GetText();
                Debug.Log(playerConversant.hasNext());
                nextButton.gameObject.SetActive(playerConversant.hasNext());
            }
            
        }


        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject newChoice = Instantiate(choicePrefab, choiceRoot);
                newChoice.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choice.GetText();
                //Debug.Log(newChoice.GetComponentInChildren<Button>());
                Button button = newChoice.GetComponentInChildren<Button>();
                if (choice.QuestChoice())
                {
                    if (playerConversant.GetComponent<PlayerResources>().GetSpecificResources(playerConversant.GetComponent<PlayerResources>().GetWood()) >= 10)
                    {
                        button.onClick.AddListener(() =>
                        {
                            playerConversant.SelectChoice(choice);
                        });
                    }
                    else if (playerConversant.GetComponent<PlayerResources>().GetSpecificResources(playerConversant.GetComponent<PlayerResources>().GetWood()) <= 10)
                    {
                            button.enabled = false;
                            newChoice.GetComponent<Image>().color = new Color(102, 102, 102, 0.5f);
                        
                    }
                }
                    
                    button.onClick.AddListener(() => 
                    {
                        playerConversant.SelectChoice(choice);
                    });
            }
        }
        //public void BuildChoiceInterract()
        //{
        //    int i = 1;
        //    foreach (Transform item in choiceRoot)
        //    {
        //        Destroy(item.gameObject);
        //    }
        //    foreach (DialogueNode choice in playerConversant.GetChoices())
        //    {
        //        GameObject newChoice = Instantiate(choicePrefab, choiceRoot);
        //        newChoice.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = choice.GetText();
        //        Debug.Log(newChoice.GetComponentInChildren<Button>());
        //        Button button = newChoice.GetComponentInChildren<Button>();
        //        if(playerConversant.GetComponent<PlayerResources>().GetSpecificResources(playerConversant.GetComponent<PlayerResources>().GetWood()) >= 10 || i != 1)
        //        {
        //            button.onClick.AddListener(() =>
        //            {
        //                playerConversant.SelectChoice(choice);
        //            });
        //        }
        //        else if(playerConversant.GetComponent<PlayerResources>().GetSpecificResources(playerConversant.GetComponent<PlayerResources>().GetWood()) <= 10)
        //        {
        //            if(i == 1)
        //            {
        //                button.enabled = false;
        //                newChoice.GetComponent<Image>().color = new Color(102,102,102,0.5f);
        //            }
        //        }
        //        i++;
        //    }
        //}

        // Update is called once per frame
        void Update()
        {

        }
    }
}
