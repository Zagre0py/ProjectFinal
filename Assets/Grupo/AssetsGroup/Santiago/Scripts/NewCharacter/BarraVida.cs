using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Slider slider;  // Referencia al componente Slider de U
    public PlayerCombat playerCombat;


    // Método para actualizar la barra
    public void ActualizarBarra(float vidaActual, float vidaMaxima)
    {
        slider.value = vidaActual / vidaMaxima;

        
    }

    
}