using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public int currentDifficultyMod = 1;
    public int timePerLevel = 15;
    private float levelTimer = 0;
    
    private void Awake() 
    {
        GlobalVars.difficultyManager = this;
    }

    void Update()
    {
        levelTimer+=Time.deltaTime;
        currentDifficultyMod = (int)(levelTimer/timePerLevel);
    }
}
