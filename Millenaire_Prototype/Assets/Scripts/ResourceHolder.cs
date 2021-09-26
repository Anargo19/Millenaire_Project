using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceHolder : MonoBehaviour
{
    [SerializeField] ResourcesScriptable resource;
    [SerializeField] float hold;
    // Start is called before the first frame update
    
    public ResourcesScriptable GetResource()
    {
        if (hold > 0)
        {
            Debug.Log("Resource removed");
            hold--;
            return resource;
        }

        Destroy(gameObject);
        return null;
    }
}
