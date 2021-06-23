using RPG.Dialogue;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractUI : MonoBehaviour
{
    Interaction interaction;
    bool enable = false;
    // Start is called before the first frame update
    void Start()
    {
        interaction = GameObject.FindGameObjectWithTag("Player").GetComponent<Interaction>();
        interaction.onDetection += EnableUI;
        interaction.onNoDetection += DisableUI;
    }

    private void EnableUI()
    {
        gameObject.SetActive(true);
    }

    private void DisableUI()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
