using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;











public class SCR_PauseMenu : MonoBehaviour
{

    public bool isOpened = false;
    public AudioSource audioMixer;
    public Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private int currentResolution = 0;
    public GameObject menucanvas;
    public Slider musicSlider;



    public GameObject fullscreenToggle;
 
    public bool isFullScreen;










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
        isOpened = !isOpened;
        if (isOpened == true)
        {
            menucanvas.SetActive(true);
        }

        else if (isOpened == false)
        {
            menucanvas.SetActive(false);
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


    public void Exit()
    {
        Application.Quit();
    }



    public void Menu()
    {
        SceneManager.LoadScene(0);
    }







    // Update is called once per frame
    void Update()
    {

        audioMixer.volume = musicSlider.value;
        Screen.fullScreen = isFullScreen;
    
    
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowHideMenu();
        }
    }
}
