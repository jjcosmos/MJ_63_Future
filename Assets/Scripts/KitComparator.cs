using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitComparator : MonoBehaviour
{
    public KitScriptableObject currentKit;

    private void Start() {

        GlobalVars.playerKitComparator = this;
        currentKit = GlobalVars.seededSpawner.availibleKits[0];
    }

    public void UpdateCurrentKitProgression(KitScriptableObject newKit)
    {
        if(newKit != currentKit)
        {
            Debug.Log($"entering new kit: {newKit.name} from {currentKit.name}");
            currentKit = newKit;

            GlobalVars.achievementManager.RecieveAchivementRequest(currentKit.linkedAchievement);
        }
    }
}
