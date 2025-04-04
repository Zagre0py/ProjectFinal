using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("click");
            pauseMenu.SetActive(true);
        }
    }

    public void goMenu()
   {
        SceneManager.LoadScene("UI");
        pauseMenu.SetActive(false) ;
   }
    public void Level1Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Tania");
        pauseMenu.SetActive(false);
    }
    public void Level2Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Chacho");
        pauseMenu.SetActive(false);
    }
    public void Level3Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Santiago");
        pauseMenu.SetActive(false);
    }

    public void resetScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
