using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetailsUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI title;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] GameObject objectives;
    [SerializeField] GameObject objectivePrefab;

    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    public void ShowDetails(QuestStatus status)
    {
        Quest quest = status.GetQuest();
        title.text = quest.name;
        description.text = quest.GetDescription();

        foreach(Transform t in objectives.transform)
        {
            Destroy(t.gameObject);
        }

        foreach(Objective o in quest.GetObjectives())
        {
            Debug.Log(o.name);
            GameObject newObjective = Instantiate(objectivePrefab, objectives.transform);
            if (status.ObjectiveIsComplete(o.name)) newObjective.transform.GetChild(0).GetComponent<Image>().color = Color.green;
            newObjective.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = o.GetDescription();
            
        }
    }
}