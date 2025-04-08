using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject ArepaImg1;
    [SerializeField] GameObject ArepaImg2;
    [SerializeField] GameObject ArepaImg3;
    [SerializeField] GameObject pantallaDerrota;

    public bool itemCollected = false;

    private void Start()
    {
        // Desactivamos las imágenes de estado de la arepa al inicio
        ArepaImg2.SetActive(false);
        ArepaImg3.SetActive(false);

        // Aseguramos que la pantalla de derrota esté desactivada al iniciar
        pantallaDerrota.SetActive(false);

        // Ocultamos y bloqueamos el cursor al empezar el juego
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        TimeCounter();
    }

    void TimeCounter()
    {
        // Si el jugador ya recogió el ítem, detenemos el conteo
        if (itemCollected)
        {
            // Podrías cargar la siguiente escena aquí si lo deseas
            return;
        }

        // Mientras quede tiempo, seguimos descontando
        else if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Cambiamos las imágenes según el tiempo restante
            if (remainingTime > 240)
            {
                ArepaImg1.SetActive(true);
            }
            if (remainingTime <= 240)
            {
                ArepaImg1.SetActive(false);
                ArepaImg2.SetActive(true);
            }
            if (remainingTime <= 120)
            {
                ArepaImg2.SetActive(false);
                ArepaImg3.SetActive(true);
            }
        }

        // Si el tiempo se agota, mostramos la pantalla de derrota
        else if (remainingTime <= 0)
        {
            remainingTime = 0;
            pantallaDerrota.SetActive(true); // Activamos la UI de derrota
            timerText.color = Color.red;
            Debug.Log("Se quemó la arepa :c");

            // Mostramos y liberamos el cursor para que el jugador pueda interactuar con la UI
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        // Actualizamos el texto del temporizador en pantalla (mm:ss)
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void SetItemCollected() // Método para marcar la recogida del ítem
    {
        Debug.Log("SetItemCollected() fue llamado, deteniendo el tiempo");
        itemCollected = true;

        // Podrías liberar el cursor aquí si vas a mostrar una pantalla de victoria
        // Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
    }
}
