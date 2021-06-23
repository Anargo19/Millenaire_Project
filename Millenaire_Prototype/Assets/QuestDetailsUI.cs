using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
    public void ShowDetails(QuestScriptable quest)
    {
        title.text = quest.name;
        description.text = quest.GetDescription();

        foreach(Transform t in objectives.transform)
        {
            Destroy(t.gameObject);
        }

        foreach(string s in quest.GetObjectives())
        {
            GameObject newObjective = Instantiate(objectivePrefab, objectives.transform);
            newObjective.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = s;
        }
    }
}
