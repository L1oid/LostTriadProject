using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_LivesBar : MonoBehaviour
{
    private Transform [] hearts=new Transform[5];
    private SCR_Player character;
    // Start is called before the first frame update
    private void Awake(){
       
    character= FindObjectOfType<SCR_Player>();

    for (int i = 0; i < hearts.Length; i++)
    {
    hearts[i]=transform.GetChild(i);
    }

    }


    public void Refresh()
        {

    for (int i = 0; i < hearts.Length; i++)
          {
        if(i<character.Lives)hearts[i].gameObject.SetActive(true);
        else {hearts[i].gameObject.SetActive(false);}
          }
        }




}
