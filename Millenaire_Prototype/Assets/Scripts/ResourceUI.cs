using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResourceUI : MonoBehaviour
{
    PlayerResources playerResources;
    [SerializeField] ResourcesScriptable resource;
    // Start is called before the first frame update
    void Start()
    {

        playerResources = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerResources>();
        playerResources.OnResourceChanged += UpdateUI;
    }

    private void UpdateUI()
    {
        foreach(var Key in playerResources.GetResources())
        {
            if(Key.Key == resource) 
            {
                transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = Key.Value.ToString();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
