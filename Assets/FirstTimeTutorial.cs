using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTimeTutorial : MonoBehaviour
{
    [SerializeField] float showTime = 5f;
    bool closing;
    RectTransform rect;
    void Start()
    {
        rect = GetComponent<RectTransform>();
        if(PlayerPrefs.GetInt("doFTT",0)==1)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update() {

        if(!closing && Input.GetKeyDown(KeyCode.X))
        {
            StartCoroutine(ShowTutorial());
            PlayerPrefs.SetInt("doFTT", 1);
            closing = true;
        }
    }

    private IEnumerator ShowTutorial()
    {
        while(rect.position.x > -310)
        {
            rect.position -= new Vector3(Time.deltaTime * 500f,0,0);
            yield return null;
        }
        Destroy(this.gameObject);
    }
}
