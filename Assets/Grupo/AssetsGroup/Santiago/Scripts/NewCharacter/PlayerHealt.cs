using UnityEngine;
using UnityEngine.UI;



public class PlayerHealth : MonoBehaviour
{

    public int vidaPlayer;
    public Slider vidaVisual;
    public Animator anim;
    public SceneController sceneController;

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
            Invoke("CargarEscenaDerrota", 3);
            Debug.Log("Se acabo");
        }
    }

    void CargarEscenaDerrota()
    {
        sceneController.cargarDerrota();
    }
}