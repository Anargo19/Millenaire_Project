using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Quest")]
public class Quest : ScriptableObject
{
    
    //[SerializeField] string[] objectives;
    [SerializeField] string description;
    [SerializeField] Objective[] objectives;

    private void Awake()
    {
        foreach(Objective o in objectives)
        {
            if(o.GetObjectiveType() == "GetResources")
            {

            }
        }
    }

    public IEnumerable<Objective> GetObjectives()
    {
        foreach(Objective o in objectives)
        {
            yield return o;
        }
    }

    public string GetDescription()
    {
        return description;
    }
}
