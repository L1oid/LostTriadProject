using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SCR_Pause : MonoBehaviour
{
    public float timer;
    public bool ispause;
    public bool guipause;

    void Update()
    {
        Time.timeScale = timer;
        if (Input.GetKeyDown(KeyCode.Escape) && ispause == false)
        {
            ispause = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && ispause == true)
        {
            ispause = false;
        }
        if (ispause == true)
        {
            timer = 0;
            guipause = true;

        }
        else if (ispause == false)
        {
            timer = 1f;
            guipause = false;

        }
    }
    public void OnGUI()
    {
        if (guipause == true)
        {
            Cursor.visible = true;
            if (GUI.Button(new Rect((float)(Screen.width / 2.5f), (float)(Screen.height / 2) - 150f, 150f, 45f), "Продолжить"))
            {
                ispause = false;
                timer = 0;
                Cursor.visible = false;
            }
            if (GUI.Button(new Rect((float)(Screen.width / 2.5f), (float)(Screen.height / 2) - 100f, 150f, 45f), "Настройки"))
            {

            }
            
            if (GUI.Button(new Rect((float)(Screen.width / 2.5f), (float)(Screen.height / 2), 150f, 45f), "Выход"))
            {
                Application.Quit();
            }


            if (GUI.Button(new Rect((float)(Screen.width / 2.5f), (float)(Screen.height / 2) - 50f, 150f, 45f), "Меню"))
            {
                Application.Quit();
                ispause = false;
                timer = 0;
                SceneManager.LoadScene(0);
            }





        }
    }
}