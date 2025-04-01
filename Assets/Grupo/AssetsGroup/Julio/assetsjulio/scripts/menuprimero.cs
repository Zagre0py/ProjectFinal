using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuprimero : MonoBehaviour
{
    // Start is called before the first frame update
   public  void Jugar()
    {
        SceneManager.LoadScene(2);
    }

    // Update is called once per frame
   public void Salir()
    {
        Debug.Log("salir....");
        Application.Quit();
    }
    public void Creditos()
    {
        SceneManager.LoadScene(4);
    }
}