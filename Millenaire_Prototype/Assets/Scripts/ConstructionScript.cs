using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionScript : MonoBehaviour
{
    [SerializeField] GameObject house;
    // Start is called before the first frame update
    public void Construct()
    {
        GameObject newBuilding = Instantiate(house, transform.position, transform.rotation);
        Destroy(gameObject);

    }
}
