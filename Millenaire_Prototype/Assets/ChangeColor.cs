using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeColor : MonoBehaviour
{
   public void ChangeImageColor()
    {
        GetComponent<Image>().color = new Color(255, 201, 0);
    }
}
