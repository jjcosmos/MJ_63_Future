using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    MeshRenderer rend;
    public string currentEquippedSkin;
    [SerializeField] SkinScriptableObject[] availibleSkins;
    
    private void Awake() 
    {
        rend = GetComponent<MeshRenderer>();   
    }
    private void Start() 
    {
        currentEquippedSkin = PlayerPrefs.GetString("currentskin","Default");
        SetSkin(currentEquippedSkin);
    }

    void SetSkin(string skinName)
    {
        switch(skinName)
        {
            case("Default"):
                SwapSkin(0, true);
                break;
            case ("Voyager"):
                SwapSkin(1, true);
                break;
            case ("Tranquility"):
                SwapSkin(2, true);
                break;
            case ("VoidWalker"):
                SwapSkin(3, true);
                break;
            case ("Immortal"):
                SwapSkin(4, true);
                break;
        }
    }
    public void SwapSkin(int index, bool turnOn)
    {
        if(!turnOn) return;
        SkinScriptableObject skin = availibleSkins[index];
        rend.materials = skin.materialSwap;

        switch (index)
        {
            case (0):
                PlayerPrefs.SetString("currentskin", "Default");
                break;
            case (1):
                PlayerPrefs.SetString("currentskin", "Voyager");
                break;
            case (2):
                PlayerPrefs.SetString("currentskin", "Tranquility");
                break;
            case (3):
                PlayerPrefs.SetString("currentskin", "VoidWalker");
                break;
            case (4):
                PlayerPrefs.SetString("currentskin", "Immortal");
                break;
        }
    }

    public void ToggleSkin(int skinIndex)
    {
        SkinScriptableObject skin = availibleSkins[skinIndex];
        rend.materials = skin.materialSwap;
    }
}
