using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestList : MonoBehaviour, IPredicateEvaluator
{
     [SerializeField] List<QuestStatus> questStatus = new List<QuestStatus>();

    public bool? Evaluate(string predicator, string[] parameters)
    {
        if (predicator == "HasResources")
        {

            return GetComponent<PlayerResources>().GetSpecificResources(GetComponent<PlayerResources>().GetResourceScriptable(parameters[0])) >= int.Parse(parameters[1]);
        }
        return null;
    }

    public IEnumerable<QuestStatus> GetQuests()
    {
        return questStatus;
    }

    public void AddQuest(Quest quest)
    {
        QuestStatus newStatus = new QuestStatus(quest);
        questStatus.Add(newStatus);
       
    }

    public void RemoveQuest(Quest quest)
    {
        foreach(QuestStatus q in questStatus.ToArray())
        {
            if(q.GetQuest() == quest)
            {
                questStatus.Remove(q);
            }
        }
    }


}
