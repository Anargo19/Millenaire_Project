using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestList : MonoBehaviour
{
     [SerializeField] List<QuestScriptable> quests = new List<QuestScriptable>();

    public IEnumerable<QuestScriptable> GetQuests()
    {
        return quests;
    }

}
