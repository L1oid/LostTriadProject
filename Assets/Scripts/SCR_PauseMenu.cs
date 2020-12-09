using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class SCR_PauseMenu : MonoBehaviour
{

    public GameObject offcontrol;
    public bool isOpened = false;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int currentResolution = 0;
    public GameObject menucanvas;
    public GameObject settingscanvas;
    public Slider musicSlider;
    public GameObject fullscreenToggle;
    public bool isFullScreen;
    public bool isPause=false;
    public float timer;
    public bool isSettings;
    public AudioSource audioMixer;







    // Start is called before the first frame update
    void Start()
    {


        Screen.fullScreen = !Screen.fullScreen;



        resolutionDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        List<string> options = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].Equals(Screen.currentResolution))
            {
                currentResolution = i;
            }
        }

        resolutionDropdown.AddOptions(options);

        resolutionDropdown.value = currentResolution;
        
        resolutionDropdown.RefreshShownValue();

        Screen.SetResolution(resolutions[resolutions.Length - 1].width, resolutions[resolutions.Length - 1].height, isFullScreen);
    }

    public void ShowHideMenu()
    {
       // isPause = !isPause;
        if (isPause == true)
        {
            menucanvas.SetActive(true);
            timer = 0;
        }

        else if (isPause == false)
        {
            menucanvas.SetActive(false);
            timer = 1f;
        }
    }


 

    public void ChangeResolution(int index)
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, isFullScreen);
    }

    public void ToggleFullscreen()
    {
        isFullScreen = !isFullScreen;

    }


    public void Again(){

        SceneManager.LoadScene(3);
    }


    public void Continue()
    {
        isPause = false;
        menucanvas.SetActive(false);
        offcontrol.SetActive(true);
        timer = 1f;
    }


    public void Settings()
    {
        isSettings = true;
        if (isSettings == true)
        {
            menucanvas.SetActive(false);
            settingscanvas.SetActive(true);
        }

    }




    public void Menu()
    {
        SceneManager.LoadScene(0);
        menucanvas.SetActive(false);
        isPause = false;
    }



    public void Exit()
    {
        Application.Quit();
        menucanvas.SetActive(false);
        isPause = false;
    }



    // Update is called once per frame
    void Update()
    {
        audioMixer.volume = musicSlider.value;
        Time.timeScale = timer;
        Screen.fullScreen = isFullScreen;
    
    
        if (Input.GetKeyDown(KeyCode.Escape) && isPause==false)
        {
            isPause = true;
            offcontrol.SetActive(false);
            ShowHideMenu();
        }


     else if (Input.GetKeyDown(KeyCode.Escape) && isPause == true)
        {
            isPause = false;
            offcontrol.SetActive(true);
            ShowHideMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && isSettings == true)
        {
            menucanvas.SetActive(true);
            settingscanvas.SetActive(false);
            isSettings = false;
        }






    }
}
