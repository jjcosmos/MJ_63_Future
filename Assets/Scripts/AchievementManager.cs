using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AchievementManager : MonoBehaviour
{
    public bool ACH_HasThresholdSkin;
    public bool ACH_HasMeadowsSkin;
    public bool ACH_HasVoidSkin;
    public bool ACH_HasImmortalSkin;

    private void Awake() 
    {
        GlobalVars.achievementManager = this;
        
    }

    void Start()
    {
        ACH_HasThresholdSkin = Convert.ToBoolean(PlayerPrefs.GetInt("ach_hasthresholdskin",1));
        ACH_HasMeadowsSkin = Convert.ToBoolean(PlayerPrefs.GetInt("ach_hasmeadowsskin",0));
        ACH_HasVoidSkin = Convert.ToBoolean(PlayerPrefs.GetInt("ach_hasvoidskin",0));
        ACH_HasImmortalSkin = Convert.ToBoolean(PlayerPrefs.GetInt("ach_hasimmortalskin",0));
    }

    public enum EAchievement_Type {ACH_ThresholdSkin,ACH_MeadowsSkin, ACH_VoidSkin, ACH_ImmortalSkin}

    public void SetAchievementComplete(EAchievement_Type achievement_Type, bool hasCompleted)
    {
        Debug.Log($"<color=red>Player has completed {achievement_Type}!</color>");
        int completed = hasCompleted ? 1 : 0;
        switch(achievement_Type)
        {
            case(EAchievement_Type.ACH_ThresholdSkin):
                PlayerPrefs.SetInt("ach_hasthresholdskin", completed);
                break;
            case (EAchievement_Type.ACH_MeadowsSkin):
                PlayerPrefs.SetInt("ach_hasmeadowsskin", completed);
                break;
            case (EAchievement_Type.ACH_VoidSkin):
                PlayerPrefs.SetInt("ach_hasvoidskin", completed);
                break;
            case (EAchievement_Type.ACH_ImmortalSkin):
                PlayerPrefs.SetInt("ach_hasimmortalskin", completed);
                break;
        }
    }

    public void RecieveAchivementRequest(EAchievement_Type achievement_Type)
    {
        switch (achievement_Type)
        {
            case (EAchievement_Type.ACH_ThresholdSkin):
                if(!ACH_HasThresholdSkin){SetAchievementComplete(achievement_Type, true);}
                break;
            case (EAchievement_Type.ACH_MeadowsSkin):
                if (!ACH_HasMeadowsSkin) { SetAchievementComplete(achievement_Type, true); }
                break;
            case (EAchievement_Type.ACH_VoidSkin):
                if (!ACH_HasVoidSkin) { SetAchievementComplete(achievement_Type, true); }
                break;
            case (EAchievement_Type.ACH_ImmortalSkin):
                if (!ACH_HasImmortalSkin) { SetAchievementComplete(achievement_Type, true); }
                break;
        }
    }
}
