using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SCR_CameraControl : MonoBehaviour
{

    [SerializeField]
    protected Transform trackingTarget;
    [SerializeField]
    float xOffset;
    [SerializeField]
    float yOffset;
    [SerializeField]
    protected float followSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Play()
    {
       SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        Application.Quit();
    }





    // Update is called once per frame
    void Update()
    {
        float xTarget = trackingTarget.position.x + xOffset;
        float yTarget = trackingTarget.position.y + yOffset;

        float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
        float yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }

}