using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class GlobalVars : MonoBehaviour
{
    public static SeededSpawner seededSpawner;
    public static GradientCreator worldGradient;
    public static CinemachineFreeLook mainCineCam;
    public static ScoreTracker scoreTracker;
    public static DifficultyManager difficultyManager;
    public static KitComparator playerKitComparator;
    public static int seed = 100;

    public int sampleRate;
    public static int SampleRate;

    private void Awake() {
        SampleRate = sampleRate;
    }
    private void Update() {
        SampleRate = sampleRate;
    }

    public static bool SetHighScore(int currentScore)
    {
        if(PlayerPrefs.GetInt("highscore", 0) < currentScore)
        {
            PlayerPrefs.SetInt("highscore", currentScore);
            Debug.Log("Setting high score to " + currentScore);
            return true;
        }
        else return false;
    }
}

