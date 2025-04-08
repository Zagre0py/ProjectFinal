using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Awake()
    {
        pauseMenu.SetActive(false);

        // Verifica si la escena actual es el menú principal (UI)
        if (SceneManager.GetActiveScene().name != "UI")
        {
            // Estamos en una escena jugable, se cambia el estado a Playing
            GameManager.Instance?.CambiarEstado(GameManager.GameState.Playing);

            // Ocultamos y bloqueamos el cursor durante el gameplay
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            // Estamos en la escena del menú, se cambia el estado a Menu
            GameManager.Instance?.CambiarEstado(GameManager.GameState.Menu);

            // Mostramos y liberamos el cursor para interactuar con la UI
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("Se presionó la tecla P");

            if (GameManager.Instance == null)
            {
                Debug.LogWarning("GameManager.Instance es null");
                return;
            }

            Debug.Log("Estado actual del juego: " + GameManager.Instance.EstadoActual);

            if (GameManager.Instance.EsEstado(GameManager.GameState.Playing))
            {
                Debug.Log("El juego está en estado 'Playing', se pausará...");
                PausarJuego();
            }
            else if (GameManager.Instance.EsEstado(GameManager.GameState.Paused))
            {
                Debug.Log("El juego está en estado 'Paused', se reanudará...");
                ReanudarJuego();
            }
            else
            {
                Debug.Log("La tecla P fue presionada pero el estado actual no permite pausa/reanudación.");
            }
        }
    }



    private void PausarJuego()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.CambiarEstado(GameManager.GameState.Paused);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 0f;
    }

    private void ReanudarJuego()
    {
        pauseMenu.SetActive(false);
        GameManager.Instance.CambiarEstado(GameManager.GameState.Playing);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }

    public void goMenu()
    {
        SceneManager.LoadScene("UI");
        GameManager.Instance?.CambiarEstado(GameManager.GameState.Menu);
        pauseMenu.SetActive(false);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1f;
    }

    public void Level1Change()
    {
        CargarNivel("Tania");
    }

    public void Level2Change()
    {
        CargarNivel("Chacho");
    }

    public void Level3Change()
    {
        CargarNivel("Santiago");
    }

    public void resetScene()
    {
        CargarNivel(SceneManager.GetActiveScene().name);
    }

    public void cargarVictoria()
    {
        SceneManager.LoadScene("Victoria");
 
    }

    public void cargarDerrota()
    {
        SceneManager.LoadScene("Derrota");
      
    }

    private void CargarNivel(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
        GameManager.Instance?.CambiarEstado(GameManager.GameState.Playing);
        pauseMenu.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1f;
    }
}
