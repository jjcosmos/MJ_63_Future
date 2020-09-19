using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTracker : MonoBehaviour
{
    
    [SerializeField] TMP_Text scoreTxt;
    public int score {get; private set;}
    void Awake()
    {
        score = 0;
        GlobalVars.scoreTracker = this;
    }

    public void ModScore(int mod)
    {
        score += mod;
        scoreTxt.text = "Score: " + score;
    }
}
