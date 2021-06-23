using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    [SerializeField] bool asAQuest;
    [SerializeField] GameObject image;

    private void Start()
    {
        image.SetActive(asAQuest);
    }

    public bool HasQuest()
    {
        return asAQuest;
    }

    public void SetQuest(bool quest)
    {
        asAQuest = quest;
    }
}
