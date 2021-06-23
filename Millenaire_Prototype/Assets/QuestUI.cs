using ECM.Components;
using ECM.Controllers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestUI : MonoBehaviour
{
    [SerializeField] GameObject QuestList;
    [SerializeField] GameObject QuestContent;
    [SerializeField] GameObject QuestDetails;

    PlayerQuestList playerQuestList;


    private void Awake()
    {

        playerQuestList = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerQuestList>();

        foreach (Transform t in QuestList.transform)
        {
            Destroy(t.gameObject);
        }

        foreach(QuestScriptable quest in playerQuestList.GetQuests())
        {
            GameObject newQuest = Instantiate(QuestContent, QuestList.transform);
            newQuest.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = quest.name;
            newQuest.GetComponent<Button>().onClick.AddListener(() => { ShowQuestDetails(quest); }); ;
        }

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void ShowQuestDetails(QuestScriptable quest)
    {
        QuestDetails.SetActive(true);
        QuestDetails.GetComponent<QuestDetailsUI>().ShowDetails(quest);
    }
}
