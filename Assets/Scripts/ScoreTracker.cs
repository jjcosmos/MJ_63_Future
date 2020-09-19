using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ScoreTracker : MonoBehaviour
{
    
    [SerializeField] TMP_Text scoreTxt;
    public int score {get; private set;}
    int hightscore;
    void Awake()
    {
        score = 0;
        GlobalVars.scoreTracker = this;
        hightscore = PlayerPrefs.GetInt("highscore",0);
        scoreTxt.text = $"Score: {score}\n<size=50%>Highscore: {hightscore}";
    }

    public void ModScore(int mod)
    {
        score += mod;
        if(score > hightscore){hightscore = score;}
        //scoreTxt.text = "Score: " + score + "\n<size=50%>Highscore: " + hightscore;
        scoreTxt.text = $"Score: {score}\n<size=50%>Highscore: {hightscore}";
    }
}
