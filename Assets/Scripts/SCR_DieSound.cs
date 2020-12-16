using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_DieSound : MonoBehaviour
{
 [SerializeField] AudioSource DiePavuk;

    // Start is called before the first frame update
    void Start()
    {
     StartCoroutine (PlayOnDestroy());   
    }


    private IEnumerator PlayOnDestroy(){
        yield return new WaitUntil(() => DiePavuk.isPlaying == false);
        Destroy(gameObject);

    }




    // Update is called once per frame
    void Update()
    {
        
    }
}
