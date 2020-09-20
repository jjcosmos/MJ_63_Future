using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinPickerController : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string clickEvent;
    [SerializeField] SkinLoader skinLoader;
    [SerializeField] AchievementManager achievementManager;
    [SerializeField] Toggle skin0Togg;
    [SerializeField] Toggle skin1Togg;
    [SerializeField] Toggle skin2Togg;
    [SerializeField] Toggle skin3Togg;
    [SerializeField] Toggle skin4Togg;

    private IEnumerator Start() {

        yield return new WaitForSeconds(.1f);

        if(achievementManager.ACH_HasThresholdSkin)
        {
            skin1Togg.interactable = true;
        }
        if (achievementManager.ACH_HasMeadowsSkin)
        {
            skin2Togg.interactable = true;
        }
        if (achievementManager.ACH_HasVoidSkin)
        {
            skin3Togg.interactable = true;
        }
        if (achievementManager.ACH_HasImmortalSkin)
        {
            skin4Togg.interactable = true;
        }

        string skinName = PlayerPrefs.GetString("currentskin", "Default");
        Debug.Log(skinName);
        switch (skinName)
        {
            case ("Default"):
                skin0Togg.isOn = true;
                break;
            case ("Voyager"):
                skin1Togg.isOn = true;
                break;
            case ("Tranquility"):
                skin2Togg.isOn = true;
                break;
            case ("VoidWalker"):
                skin3Togg.isOn = true;
                break;
            case ("Immortal"):
                skin4Togg.isOn = true;
                break;
        }
    }

    public void SetSkin0On(bool isOn)
    {
        skinLoader.SwapSkin(0, isOn);
        if(isOn)PlaySound();
        
    }

    public void SetSkin1On(bool isOn)
    {
        skinLoader.SwapSkin(1, isOn);
        if (isOn) PlaySound();
    }

    public void SetSkin2On(bool isOn)
    {
        skinLoader.SwapSkin(2, isOn);
        if (isOn) PlaySound();
    }

    public void SetSkin3On(bool isOn)
    {
        skinLoader.SwapSkin(3, isOn);
        if (isOn) PlaySound();
    }

    public void SetSkin4On(bool isOn)
    {
        skinLoader.SwapSkin(4, isOn);
        if (isOn) PlaySound();
    }

    private void PlaySound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(clickEvent, transform.position);
    }
}
