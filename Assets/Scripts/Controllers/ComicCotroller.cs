using UnityEngine;

public class PageController : MonoBehaviour
{
    public GameObject[] pages; // Asigna las páginas en el Inspector

    private int currentIndex = 0;
    private bool isActive = false;

    void Start()
    {
        isActive = true;

        // Al iniciar el cómic, activamos la primera página
        ActivatePage(0);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Mientras el cómic esté activo y se presione la tecla E, se avanza a la siguiente página
        if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            currentIndex++;

            // Si aún quedan páginas por mostrar, activamos la siguiente
            if (currentIndex < pages.Length)
            {
                ActivatePage(currentIndex);
            }
            else
            {
                // Si ya no hay más páginas, cerramos el cómic
                DeactivateAllPages();
            }
        }
    }

    void ActivatePage(int index)
    {
        // Activamos la página actual y desactivamos las demás
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        // Pausamos el tiempo del juego mientras se muestra el cómic
        Time.timeScale = 0f;

        // Mostramos y liberamos el cursor para que el jugador pueda interactuar
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void DeactivateAllPages()
    {
        // Desactivamos todas las páginas del cómic
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        // Marcamos el cómic como inactivo
        isActive = false;

        // Reanudamos el tiempo del juego
        Time.timeScale = 1f;

        // Ocultamos y bloqueamos el cursor al volver al gameplay
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
