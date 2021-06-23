using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest")]
public class QuestScriptable : ScriptableObject
{
    [SerializeField] string[] objectives;
    [SerializeField] string description;

    public IEnumerable<string> GetObjectives()
    {
        return objectives;
    }

    public string GetDescription()
    {
        return description;
    }
}
