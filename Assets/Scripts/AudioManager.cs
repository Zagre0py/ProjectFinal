using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para manejar escenas

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Reproduce la música inicial
        PlayMusic("Fondo3");

        // Suscribirse al evento de carga de escenas
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += EscenaPerdiste;
        SceneManager.sceneLoaded += EscenaVictoria;
    }

    // Método que se ejecuta cada vez que se carga una escena
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es el escenario 1
        if (scene.name == "Andrea21") // Cambia "Escenario1" por el nombre exacto de tu escena
        {
            // Cambia la música al entrar al escenario 1
            PlayMusic("Escenario01"); // Cambia "ThemeEscenario1" por el nombre de la música que quieres reproducir
        }
    }
    private void EscenaPerdiste (Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es el escenario 1
        if (scene.name == "GameOver") // Cambia "Escenario1" por el nombre exacto de tu escena
        {
            // Cambia la música al entrar al escenario 1
            PlayMusic("Theme01"); // Cambia "ThemeEscenario1" por el nombre de la música que quieres reproducir
        }
    }
    private void EscenaVictoria (Scene scene, LoadSceneMode mode)
    {
        // Verifica si la escena cargada es el escenario 1
        if (scene.name == "GameVictory") // Cambia "Escenario1" por el nombre exacto de tu escena
        {
            // Cambia la música al entrar al escenario 1
            PlayMusic("Theme01"); // Cambia "ThemeEscenario1" por el nombre de la música que quieres reproducir
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sounds not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sounds not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}