using UnityEngine;

public class RotacionConstante : MonoBehaviour
{
    public float rotationSpeed = 50f; // Velocidad de rotación en grados por segundo

    void Update()
    {
        /*transform.rotation = Quaternion.Euler(0,0, transform.rotation.eulerAngles.z + (rotationSpeed * Time.deltaTime));*/
    }
}
