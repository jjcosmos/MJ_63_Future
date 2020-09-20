using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZoneTextController : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Animator animator;
    int animHash;
    FMOD.Studio.EventInstance playerIntro;

    void Start()
    {
        animHash = Animator.StringToHash("SwingInText");
        PlayIntro("THRESHOLD");
        
    }

    public void PlayIntro(string toBlit)
    {
        text.text = toBlit;
        animator.Play(animHash);
        PickSound(toBlit);
        Debug.Log("Animating for new zone");
    }

    void PickSound(string name)
    {
        Debug.Log(name[0]);
        if(name[0]=='T')
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/NewZoneThreshold", transform.position);
        }
        if (name[0] == 'F')
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/NewZoneFalse", transform.position);
        }
        if (name[0] == 'V')
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/NewZoneVoid", transform.position);
        }
    }

   
}
