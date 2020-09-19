using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KitScriptableObject", order = 1)]
public class KitScriptableObject : ScriptableObject
{
    public string name;
    public List<GameObject> kitObjects;
    
}