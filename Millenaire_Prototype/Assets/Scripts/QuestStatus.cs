using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestStatus
{
    [SerializeField] Quest quest;
    [SerializeField] List<string> completedObjectives;
    // Start is called before the first frame update
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
        if (completedObjectives.Contains(s)) return true;

        return false;
    }
}
