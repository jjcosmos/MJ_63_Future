using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen : MonoBehaviour
{
    [SerializeField] float time;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene("FlightMovement");
    }

    
}
