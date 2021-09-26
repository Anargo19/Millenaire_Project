using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Resource", menuName ="Millenaire/Resource")]
public class ResourcesScriptable : ScriptableObject
{
    [SerializeField] string type;
    [SerializeField] Sprite sprite;

    public Sprite GetSprite()
    {
        return sprite;
    }
}
