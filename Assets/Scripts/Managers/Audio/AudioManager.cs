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
    

  void Awake()
{
    if (instance == null)
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        
        // Suscribir los eventos
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneLoaded += EscenaPerdiste;
        SceneManager.sceneLoaded += EscenaVictoria;
    }
    else
    {
        Destroy(gameObject);
    }
}
   


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
{
    // Detener siempre la música actual primero
    musicSource.Stop();
    
    // Reproducir música según la escena
    switch(scene.name)
    {
        case "UI":
            PlayMusic("UI");
            break;
        case "Tania":
            PlayMusic("Nivel1");
            break;
        case "Chacho":
            PlayMusic("Nivel2");
            break;
        case "Santiago":
            PlayMusic("Nivel3");
            break;
        default:
            // Opcional: música por defecto para escenas no listadas
            break;
    }
}
    
    private void EscenaPerdiste(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Derrota")
        {
            PlayMusic("Theme01");
        }
    }
    
    private void EscenaVictoria(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Victoria")
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