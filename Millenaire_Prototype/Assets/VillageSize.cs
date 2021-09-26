using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VillageSize : MonoBehaviour
{
    [SerializeField] GameObject chest;
    ChestInventory Inventory;
    [SerializeField] ResourcesScriptable wood;
    [SerializeField] GameObject house;
    [SerializeField] GameObject distanceUI;
    // Start is called before the first frame update
    void Start()
    {
        Inventory = chest.GetComponent<ChestInventory>();   
    }

    // Update is called once per frame
    void Update()
    {
        if(Inventory.GetResourcesAmount(wood) >= 10)
        {
            Vector3 newPos = new Vector3(Random.Range(transform.position.x - 100, transform.position.x + 100), 0, Random.Range(transform.position.z - 100, transform.position.z + 100));
            if(!Physics.CheckSphere(newPos, 5))
            {
                Instantiate(house, newPos, Quaternion.identity);
                Inventory.ChangeRessources(wood, -10);
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                player.transform.LookAt(newPos);
                Debug.Log("Before test");
                StartCoroutine(Test(player.transform.position, newPos));
                
            }
        }
    }

    IEnumerator Test(Vector3 player, Vector3 newHouse)
    {
        Debug.Log("Launched");
        distanceUI.SetActive(true);
        distanceUI.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Distance : {Vector3.Distance(player, newHouse)}";
        yield return new WaitForSeconds(6);

        distanceUI.SetActive(false);
    }
}
