using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Objective")]
public class Objective : ScriptableObject
{
    enum ObjectiveType
    {
        GetResources
    }
    [SerializeField] string description;
    [SerializeField] ObjectiveType type;
    [SerializeField] int resources;

    public string GetObjectiveType()
    {
        return type.ToString();
    }
    public string GetDescription()
    {
        return description;
    }
}
