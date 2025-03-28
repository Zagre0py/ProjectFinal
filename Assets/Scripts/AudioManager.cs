using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio; // Importar AudioMixer

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Arrastrar el Audio Mixer desde el Inspector
    public Slider sliderMusica;
    public Slider sliderFX;
    public GameObject panelSonido;

    void Start()
    {
        // Cargar valores guardados (Por defecto en 1)
        sliderMusica.value = PlayerPrefs.GetFloat("volumenMusica", 1f);
        sliderFX.value = PlayerPrefs.GetFloat("volumenFX", 1f);

        // Aplicar el volumen inicial
        CambiarVolumenMusica(sliderMusica.value);
        CambiarVolumenFX(sliderFX.value);

        // Eventos de los sliders
        sliderMusica.onValueChanged.AddListener(CambiarVolumenMusica);
        sliderFX.onValueChanged.AddListener(CambiarVolumenFX);

        // Ocultar el panel de sonido al inicio
        panelSonido.SetActive(false);
    }

    public void CambiarVolumenMusica(float volumen)
    {
        float volumenDB = Mathf.Log10(volumen) * 20;
        audioMixer.SetFloat("VolumenMusica", volumenDB);
        PlayerPrefs.SetFloat("volumenMusica", volumen);
    }

    public void CambiarVolumenFX(float volumen)
    {
        float volumenDB = Mathf.Log10(volumen) * 20;
        audioMixer.SetFloat("VolumenFX", volumenDB);
        PlayerPrefs.SetFloat("volumenFX", volumen);
    }

    // Método para activar/desactivar el panel
    public void MostrarPanelSonido()
    {
        panelSonido.SetActive(!panelSonido.activeSelf);
    }
}

