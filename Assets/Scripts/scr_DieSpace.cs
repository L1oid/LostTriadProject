using UnityEngine;

public class scr_DieSpace : MonoBehaviour
{
    public GameObject respawn;
    public GameObject DeathScreen; 

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.tag == "Player")
        {
            DeathScreen.SetActive(true);
        }
    }

}
