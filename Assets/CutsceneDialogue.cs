using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CutsceneDialogue : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] SceneTransitionController sceneTransitionController;
    [TextArea]
    [SerializeField] List<string> dialogue;

    int index = 0;
    public void GoNext()
    {
        if(index>=dialogue.Count)
        {
            sceneTransitionController.SwapScene();
            return;
        }
        Debug.Log("Gamers");
        text.text = dialogue[index];
        index ++;
    }
}
