using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singleton: Permite que haya solo una instancia de GameManager en la escena
    public static GameManager Instance { get; private set; }

    // Definición de los posibles estados del juego
    public enum GameState { Menu, Playing, Paused, GameOver, Win }
    public GameState EstadoActual { get; private set; }

    // Variables para indicar si el jugador ganó o perdió
    public bool Lvl1Win { get; private set; }
    public bool Lvl2Win { get; private set; }
    public bool Lvl3Win { get; private set; }

    private void Awake()
    {
        // Implementación del patrón Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // No destruir al cambiar de escena
        }
        else
        {
            Destroy(gameObject); // Si ya existe otra instancia, eliminar esta
        }
    }

    private void Start()
    {
        // Establecer el estado inicial del juego en el menú
        CambiarEstado(GameState.Menu);
    }

    // Método para cambiar el estado del juego
    public void CambiarEstado(GameState nuevoEstado)
    {
        EstadoActual = nuevoEstado;
        Debug.Log("Estado cambiado a: " + EstadoActual);

        // Reiniciar las banderas de victoria y derrota
        Lvl1Win = false;
        Lvl2Win = false;
        Lvl3Win = false;

        // Realizar acciones según el estado actual del juego
        switch (EstadoActual)
        {
            case GameState.Menu:
                // Aquí se pueden agregar acciones específicas para el menú
                break;

            case GameState.Playing:
                Time.timeScale = 1f; // Restablecer la velocidad del tiempo
                break;

            case GameState.Paused:
                Time.timeScale = 0f; // Pausar el juego
                break;

            case GameState.GameOver:
                // Aquí se pueden agregar acciones específicas para el menú
                break;

            case GameState.Win:
                // Aquí se pueden agregar acciones específicas para el menú
                break;
        }
    }

    // Método para verificar si el juego está en un estado específico
    public bool EsEstado(GameState estado)
    {
        return EstadoActual == estado;
    }
}
