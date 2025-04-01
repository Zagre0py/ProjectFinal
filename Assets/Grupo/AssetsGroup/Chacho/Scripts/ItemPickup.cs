using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemPickup : MonoBehaviour
{
    private static int index = 2; // Se mantiene en todas las escenas

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("El objeto fue recogido por el jugador");

            int nextIndex = index;
            index++; // Aumentar el índice solo una vez

            Timer timer = FindObjectOfType<Timer>();
            if (timer != null)
            {
                timer.SetItemCollected();
            }

            Destroy(gameObject);
            Debug.Log("Leche recogida");

            SceneManager.LoadScene(nextIndex);
        }
    }
}
