using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class CameraHandler : MonoBehaviour
    {

    public GameObject target;
    public Vector3 v3;
    public float speed;
    public float maxLook;
    public float minLook;
    public Quaternion camRotation;

    // Use this for initialization
    void Start()
    {
        camRotation = transform.localRotation;
    }

    public void Cam()
{
    if (target)
    {
        transform.position = target.transform.position + v3;
    }

    camRotation.y += Input.GetAxis("Mouse X") * speed;
    camRotation.x += Input.GetAxis("Mouse Y") * speed * -1;

    camRotation.x = Mathf.Clamp(camRotation.x, minLook, maxLook);

    transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
}

    void Update()
    {
        Cam();
    }

}


    

