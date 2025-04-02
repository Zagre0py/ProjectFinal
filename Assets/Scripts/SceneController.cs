using System;
using UnityEngine;
using static GameManager;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] GameObject changeLvlScreen;

    private void Start()
    {
        changeLvlScreen.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.P))
        {
            Debug.Log("click");
            changeLvlScreen.SetActive(true);
        }
    }

    public void goMenu()
   {
        SceneManager.LoadScene("UI");
        changeLvlScreen.SetActive(false) ;
   }
    public void Level1Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Tania");
        changeLvlScreen.SetActive(false);
    }
    public void Level2Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Chacho");
        changeLvlScreen.SetActive(false);
    }
    public void Level3Change()
    {
        Debug.Log("click");
        SceneManager.LoadScene("Santiago");
        changeLvlScreen.SetActive(false);
    }

    public void resetScene() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
