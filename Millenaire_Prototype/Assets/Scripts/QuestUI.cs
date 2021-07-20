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
        Refresh();

    }

    public void Refresh()
    {
        foreach (Transform t in QuestList.transform)
        {
            Destroy(t.gameObject);
        }

        foreach (QuestStatus quest in playerQuestList.GetQuests())
        {
            Quest questScript = quest.GetQuest();
            GameObject newQuest = Instantiate(QuestContent, QuestList.transform);
            newQuest.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = questScript.name;
            newQuest.GetComponent<Button>().onClick.AddListener(() => { ShowQuestDetails(quest); }); ;
        }
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }

    void ShowQuestDetails(QuestStatus status)
    {
        QuestDetails.SetActive(true);
        QuestDetails.GetComponent<QuestDetailsUI>().ShowDetails(status);
    }
}
