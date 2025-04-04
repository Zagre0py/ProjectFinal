using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    
    // Nombres de las canciones entre las que quieres elegir al inicio
    public string[] initialMusicOptions = {"Menu01", "Menu02"}; // Añade más si necesitas

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
        // Selecciona y reproduce una canción aleatoria al inicio
        PlayRandomInitialMusic();
        
        // Suscribirse al evento de carga de escenas
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += EscenaPerdiste;
        SceneManager.sceneLoaded += EscenaVictoria;
    }

    // Método para reproducir música inicial aleatoria
    private void PlayRandomInitialMusic()
    {
        if (initialMusicOptions.Length == 0) return;
        
        int randomIndex = UnityEngine.Random.Range(0, initialMusicOptions.Length);
        string selectedMusic = initialMusicOptions[randomIndex];
        PlayMusic(selectedMusic);
    }

    // Resto de tus métodos permanecen igual...
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Tania")
        {
            PlayMusic("Nivel1");
        }
        if(scene.name == "Chacho")
        {
            PlayMusic("Nivel2");
        }
        if(scene.name == "Santiago")
        {
            PlayMusic("Nivel3");
        }
    }
    
    private void EscenaPerdiste(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameOver")
        {
            PlayMusic("Theme01");
        }
    }
    
    private void EscenaVictoria(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "GameVictory")
        {
            PlayMusic("Theme01");
        }
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
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
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
}