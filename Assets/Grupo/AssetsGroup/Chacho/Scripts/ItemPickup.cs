using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("El jugador entró en el trigger del objeto"); // Mensaje de depuración

        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto fue recogido por el jugador"); // Confirmación de que la condición se cumple

            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                Debug.Log("Timer encontrado, llamando a SetItemCollected()");

                timer.SetItemCollected();
            }
            else
            {
                Debug.Log("Error: No se encontró el Timer en la escena.");
            }
            Destroy(gameObject);
            Debug.Log("Leche recogida");
        }
    }
}
