using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{

    public int vidaPlayer;
    public Slider vidaVisual;
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    private void Update()
    {

        vidaVisual.GetComponent<Slider>().value = vidaPlayer;
        if (vidaPlayer <= 0)
        {

            GetComponent<Personaje3D>().canMove = false;
            GetComponent<Animator>().Play("Muerte");
            GetComponent<Personaje3D>().OnDeath();

            anim.SetBool("IsDead", true);
            Debug.Log("Se acabo");
        }
    }
}