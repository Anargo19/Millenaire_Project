using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChestInventory : MonoBehaviour
{
    Dictionary<ResourcesScriptable, int> inventory = new Dictionary<ResourcesScriptable, int>();
    [SerializeField] ResourcesScriptable wood;

    [SerializeField] int amount;

    [SerializeField] GameObject QuestList;
    [SerializeField] GameObject QuestContent;
    [SerializeField] GameObject InventoryUI;

    public int GetResourcesAmount(ResourcesScriptable resource)
    {
        return inventory[resource];
    }

    // Start is called before the first frame update
    void Start()
    {
        inventory.Add(wood, 0);
        inventory[wood] = amount;

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void GetInventory()
    {
        InventoryUI.SetActive(true);
        Refresh();
    }

    public void Refresh()
    {
            foreach (Transform t in QuestList.transform)
            {
                Destroy(t.gameObject);
            }

        foreach (var item in inventory)
        {
            GameObject newResource = Instantiate(QuestContent, QuestList.transform);
            Debug.Log(newResource.name);
            Debug.Log(item.Key.GetSprite());
            Debug.Log($"{item.Key.name} : {item.Value}");
            newResource.transform.GetChild(0).GetComponent<Image>().sprite = item.Key.GetSprite();
            newResource.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"{item.Key.name} : {item.Value}";
        }
        
    }

    public void ChangeRessources(ResourcesScriptable resource, int value)
    {
        inventory[resource] += value;
    }
}
