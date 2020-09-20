using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    [SerializeField] string destinationScene;
    [SerializeField] float transitionDuration = 1f;
    [SerializeField] float delayBeforeReveal = 0.3f;
    [SerializeField] KeyCode nextSceneKeycode;
    [SerializeField] Texture2D inTexOverride;
    [SerializeField] Texture2D outTexOverride;

    private bool animating;
    private Image image;
    private Material imageMat;
    private int cutoffPropertyID;
    private int transitionTexPropertyID;
    private static Texture defaultTex;
    private float smoothCorrection;

    void Start()
    {
        image = GetComponent<Image>();
        imageMat = image.material;
        cutoffPropertyID = Shader.PropertyToID("_Cutoff");
        transitionTexPropertyID = Shader.PropertyToID("_TransitionTex");
        if(defaultTex == null) {defaultTex = imageMat.GetTexture(transitionTexPropertyID);}
        //smoothCorrection = imageMat.GetFloat("_UseSmoothstep") * imageMat.GetFloat("_EdgeSmoothing");
        StartCoroutine(SwapSceneCoroutine(true));
    }
    
    public void SwapScene()
    {
        StopAllCoroutines();
        StartCoroutine(SwapSceneCoroutine(false));
    }

    private IEnumerator SwapSceneCoroutine(bool isEntering)
    {
        image.enabled = true;
        imageMat.SetFloat(cutoffPropertyID, 0);
        SetTexture(ref isEntering);
        animating = true;

        if(isEntering){
            yield return new WaitForSeconds(delayBeforeReveal);
        }
        else{
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Stutter", transform.position);
        }

        float timer = 0;
        while(timer < transitionDuration)
        {
            float progress = isEntering ? (timer/transitionDuration) + .02f : 1 - (timer/transitionDuration) - 0.02f - smoothCorrection;
            imageMat.SetFloat(cutoffPropertyID, progress);
            timer += Time.deltaTime;
            yield return null;
        }

        animating = false;
        if(!isEntering){SceneManager.LoadScene(destinationScene);}
        else{ image.enabled = false; }
        yield return null;
    }

    private void SetTexture(ref bool isEntering)
    {
        imageMat.SetTexture(transitionTexPropertyID, defaultTex);
        if(isEntering && inTexOverride != null)
        {
            imageMat.SetTexture(transitionTexPropertyID, inTexOverride);
        }
        else if(!isEntering && outTexOverride != null)
        {
            imageMat.SetTexture(transitionTexPropertyID, outTexOverride);
        }
    }

    private void OnDisable() {
        //preserve og default
        imageMat.SetTexture(transitionTexPropertyID, defaultTex);
    }
}
