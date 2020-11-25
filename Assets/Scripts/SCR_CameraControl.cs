using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    // Update is called once per frame
    void Update()
    {
    float xTarget = trackingTarget.position.x + xOffset;
    float yTarget = trackingTarget.position.y + yOffset;

    float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * followSpeed);
    float yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * followSpeed);

    transform.position = new Vector3(xNew, transform.position.y, transform.position.z);
    }
}
