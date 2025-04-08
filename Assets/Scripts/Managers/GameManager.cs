using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public enum GameState { Menu, Playing, Paused, GameOver, Win }
    public GameState EstadoActual { get; private set; }

    public bool Lvl1Win { get; private set; }
    public bool Lvl2Win { get; private set; }
    public bool Lvl3Win { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CambiarEstado(GameState nuevoEstado)
    {
        EstadoActual = nuevoEstado;
        Debug.Log("Estado cambiado a: " + EstadoActual);
    }

    public bool EsEstado(GameState estado)
    {
        return EstadoActual == estado;
    }

    public void MarcarNivelGanado(int nivel)
    {
        if (nivel == 1) Lvl1Win = true;
        else if (nivel == 2) Lvl2Win = true;
        else if (nivel == 3) Lvl3Win = true;
    }
}
