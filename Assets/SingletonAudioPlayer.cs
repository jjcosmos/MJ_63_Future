using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonAudioPlayer : MonoBehaviour
{
    FMOD.Studio.EventInstance ost;
    public static SingletonAudioPlayer instance;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            SceneManager.sceneLoaded+=MySceneLoaded;
        }
        else{
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        ost = FMODUnity.RuntimeManager.CreateInstance("event:/Music/OST");
        ost.start();
    }

    private void MySceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name != "FlightMovement")
        {
            ost.setParameterByName("Lowpass", -20f);
        }
        else{
            ost.setParameterByName("Lowpass", 0f);
        }
    }
}
