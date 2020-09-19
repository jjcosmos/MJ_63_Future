using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Skin", menuName = "ScriptableObjects/SkinScriptableObject", order = 1)]
public class SkinScriptableObject : ScriptableObject
{
    
    public string achievementName;
    public string skinName;
    [Header("Primary, fin, band, cockpit")]
    public Material[] materialSwap;

}