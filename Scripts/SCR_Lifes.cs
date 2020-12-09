using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SCR_Lifes : MonoBehaviour
{

    [SerializeField]
    private GameObject mainPanel;

    [SerializeField]
    private GameObject heartPrefab;

    private List<GameObject> hearts;

    [SerializeField]
    public int lives = 4;

    // Start is called before the first frame update
    void Start()
    {
       hearts = new List<GameObject>();

        for (int i = 0; i < lives; i++)
        {
            UpdateUIAdd();
        } 
    }





private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            lives--;
            UpdateUIDel();
            if (lives <= 0)
            {
                Destroy(gameObject);
                var id = SceneManager.GetActiveScene().buildIndex;
                SceneManager.UnloadSceneAsync(id);
                SceneManager.LoadScene(id);
            }
        }
        else if (other.gameObject.CompareTag("Heart"))
        {
            lives++;
            UpdateUIAdd();
            Destroy(other.gameObject);
        }
    }


private void UpdateUIDel()
    {
        var h = hearts.Last();
        Destroy(h);
        hearts.Remove(h);
    }

    private void UpdateUIAdd()
    {
        var heart = Instantiate(heartPrefab);
        hearts.Add(heart);
        heart.transform.SetParent(mainPanel.transform);
      // heart.transform.position=new Vector3(-3.797674f, 1.757887f, 0);
    }














    // Update is called once per frame
    void Update()
    {
        
    }
}
