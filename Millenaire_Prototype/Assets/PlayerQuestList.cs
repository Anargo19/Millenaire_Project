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

            return GetComponent<PlayerResources>().GetSpecificResources(GetComponent<PlayerResources>().GetResourceScriptable(parameters[0])) >= 10;
        }
        return null;
    }

    public IEnumerable<QuestStatus> GetQuests()
    {
        return questStatus;
    }

    public void RemoveQuest(QuestStatus quest)
    {
        questStatus.Remove(quest);
    }


}
