using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/KitScriptableObject", order = 1)]
public class KitScriptableObject : ScriptableObject
{
    public AchievementManager.EAchievement_Type linkedAchievement;
    public string name;
    public Material material;
    public Color skyboxColor;
    public List<GameObject> kitObjects;
    
}