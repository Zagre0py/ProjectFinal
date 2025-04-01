using UnityEngine;

public class PageController : MonoBehaviour
{
    public GameObject[] pages; // Asigna las páginas en el Inspector

    private int currentIndex = 0;
    private bool isActive = false;

    void Start()
    {
        isActive = true;
        ActivatePage(0); // Activa la primera página
    }

    void Update()
    {
        if (isActive && Input.GetKeyDown(KeyCode.E))
        {
            currentIndex++;

            if (currentIndex < pages.Length)
            {
                ActivatePage(currentIndex);
            }
            else
            {
                DeactivateAllPages();
            }
        }
    }

    void ActivatePage(int index)
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == index);
        }

        Time.timeScale = 0f; // Pausar el tiempo mientras hay páginas activas
    }

    void DeactivateAllPages()
    {
        foreach (GameObject page in pages)
        {
            page.SetActive(false);
        }

        isActive = false;
        Time.timeScale = 1f; // Reanudar el tiempo cuando todas las páginas se cierran
    }
}
