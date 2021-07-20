using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    [SerializeField] Quest quest;
    [SerializeField] List<string> completedObjectives = new List<string>();
    // Start is called before the first frame update

    public QuestStatus(Quest quest)
    {
        this.quest = quest;
    }
    public Quest GetQuest()
    {
        return quest;
    }


    // Update is called once per frame
    public int GetCompletedObjectives()
    {
        return completedObjectives.Count;
    }

    public bool ObjectiveIsComplete(string s)
    {
        Debug.Log(s);
        if (completedObjectives.Contains(s)) return true;

        return false;
    }

    
}
