using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    [SerializeField] Dictionary<ResourcesScriptable, float> resources = new Dictionary<ResourcesScriptable, float>();
    [SerializeField] ResourcesScriptable wood;
    public event Action OnResourceChanged;
    // Start is called before the first frame update

    public void Start()
    {
        resources.Add(wood, 0);
    }

    public void AddResources(ResourcesScriptable resource)
    {
        Debug.Log(resources.ContainsKey(resource));
        if (resources.ContainsKey(resource))
        {
            resources[resource] += 1;
            OnResourceChanged();
        }
    }

    public Dictionary<ResourcesScriptable, float> GetResources()
    {
        return resources;
    }
    public float GetSpecificResources(ResourcesScriptable key)
    {
        return resources[key];
    }
    public ResourcesScriptable GetWood()
    {
        return wood;
    }
    public ResourcesScriptable GetResourceScriptable(string resourcename)
    {
        foreach(var resource in resources)
        {
            if (resource.Key.name == resourcename) return resource.Key;
        }
        return null;
    }

    public void ChangeRessources(ResourcesScriptable resource, int value)
    {
        resources[resource] += value;
        OnResourceChanged();
    }
}
