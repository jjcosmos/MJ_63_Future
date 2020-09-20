using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class ZoneTextController : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] Animator animator;
    int animHash;
    void Start()
    {
        animHash = Animator.StringToHash("SwingInText");
        PlayIntro("THRESHOLD");
        
    }

    public void PlayIntro(string toBlit)
    {
        text.text = toBlit;
        animator.Play(animHash);
        Debug.Log("Animating for new zone");
    }

   
}
