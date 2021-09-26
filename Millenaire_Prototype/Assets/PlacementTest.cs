using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementTest : MonoBehaviour
{
    [SerializeField] GameObject test;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Vector3 Pos = transform.GetChild(0).GetChild(0).position + Vector3.forward * 5;
            Pos.y = 150;
            RaycastHit hit;
            Vector3 newPos = new Vector3(Pos.x, -5, Pos.z);
            Debug.Log($"{Pos} + {newPos}");
            Debug.DrawRay(Pos, newPos, Color.red);
           if(Physics.Linecast(Pos, newPos, out hit))
            {
                if(hit.transform.tag == "Ground")
                {
                    Debug.Log(hit.point.y);
                    Instantiate(test);
                    test.transform.position = new Vector3(Pos.x, hit.point.y, Pos.z);
                }
            }
        }
    }
}
