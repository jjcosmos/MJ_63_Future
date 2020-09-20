using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutscenePlayer : MonoBehaviour
{
    [SerializeField] GameObject particles;
    [SerializeField] Animator animator;
    public void StartPFX()
    {
        animator.Play("CutscenePlayer");
        particles.SetActive(true);
    }
}
