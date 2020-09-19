using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseHelper : MonoBehaviour
{
    [SerializeField] GameObject pauseParent;
    [SerializeField] FlightMovement playerFlight;
    [SerializeField] Toggle useMouseToggle;
    [SerializeField] Toggle invertYToggle;
    [SerializeField] Slider sensSlider;
    private bool isPaused;

    private void Awake() 
    {
        bool useMouse = (PlayerPrefs.GetInt("usemouse", 0) == 0) ? false : true;
        bool invertY = (PlayerPrefs.GetInt("inverty", 1) == 0) ? false : true;
        float sens = PlayerPrefs.GetFloat("mousesens", 1f);

        useMouseToggle.isOn = useMouse;
        invertYToggle.isOn = invertY;
        sensSlider.value = sens;

        UpdateUseMouse(useMouse);
        UpdateInvertY(invertY);
        UpdateMouseSens(sens);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && isPaused)
        {
            pauseParent.SetActive(false);
            Time.timeScale = 1;
            isPaused = false;
            bool useMouse = (PlayerPrefs.GetInt("usemouse", 0) == 0) ? false : true;
            if(useMouse)
            {
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
        else if(Input.GetKeyDown(KeyCode.P) && !isPaused)
        {
            pauseParent.SetActive(true);
            Time.timeScale = 0;
            isPaused = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UpdateUseMouse(bool useMouse)
    {
        int boolConv = useMouse ? 1 : 0;
        PlayerPrefs.SetInt("usemouse", boolConv);
        playerFlight.useMouse = useMouse;
    }

    public void UpdateInvertY(bool invertY)
    {
        int boolConv = invertY ? 1 : 0;
        PlayerPrefs.SetInt("inverty", boolConv);
        playerFlight.invertY = invertY;
    }

    public void UpdateMouseSens(float sens)
    {
        PlayerPrefs.SetFloat("mousesens", sens);
        playerFlight.mouseSens = sens;
    }

    
}
