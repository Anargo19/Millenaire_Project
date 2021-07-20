using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] ResourcesScriptable resource;
    [SerializeField] float hold = 10;
    // Start is called before the first frame update
    
    public ResourcesScriptable GetResource()
    {
        if (hold > 0)
        {
            hold--;
            return resource;
        }

        Destroy(gameObject);
        return null;
    }
}
