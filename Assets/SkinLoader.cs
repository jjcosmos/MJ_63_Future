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
            case ("Transcendence"):
                SwapSkin(4, true);
                break;
        }
    }
    public void SwapSkin(int index, bool turnOn)
    {
        if(!turnOn) return;
        SkinScriptableObject skin = availibleSkins[index];
        rend.materials = skin.materialSwap;
    }
}
