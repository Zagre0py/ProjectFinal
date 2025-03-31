using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBoss : MonoBehaviour
{
    public int damage;

    public GameObject player;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Player")){

            player.GetComponent<PlayerHealth>().vidaPlayer -= damage;
        }

    }
}
