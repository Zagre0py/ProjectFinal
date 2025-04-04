using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonidos : MonoBehaviour
{
    public void SonidoGolpe01(){

        AudioManager.instance.PlaySfx("Golpe01");
    }
    public void SonidoGolpe02(){

        AudioManager.instance.PlaySfx("Golpe02");
    }
    public void SonidoGolpe03(){

        AudioManager.instance.PlaySfx("Golpe03");
    }
    public void SonidoGolpe04(){

        AudioManager.instance.PlaySfx("Golpe04");
    }
}
