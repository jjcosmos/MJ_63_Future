using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class WorldSettingsHelper : MonoBehaviour
{
    [SerializeField] TMP_Text worldTypeText;
    [SerializeField] TMP_InputField seedText;

    int worldTypeVal = 0;

    private void Start() {
        worldTypeVal = PlayerPrefs.GetInt("worldtype",0);
        seedText.text = PlayerPrefs.GetInt("seed",100).ToString();
        UpdateText();
    }

    public void ProgressTextValue()
    {
        worldTypeVal++;
        if(worldTypeVal >= 3)
        {
            worldTypeVal = 0;
        }
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Click", transform.position);
        UpdateText();
    }

    private void UpdateText()
    {
        PlayerPrefs.SetInt("worldtype", worldTypeVal);
        switch(worldTypeVal)
        {
            case(0):
                worldTypeText.text = "Normal";
                break;
            case (1):
                worldTypeText.text = "Superflat";
                break;
            case (2):
                worldTypeText.text = "Amplified";
                break;
        }
    }

    public void UpdateSeed(string input)
    {
        int result = 100;
        try
        {
            result = Int32.Parse(input);
            
        }
        catch (FormatException)
        {
            Debug.Log($"Unable to parse '{input}'");
        }
        PlayerPrefs.SetInt("seed", result);
    }
}
