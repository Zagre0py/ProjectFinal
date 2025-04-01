using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePLayer : MonoBehaviour
{
    public int damage;

    public GameObject Boss;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Player")){

            Boss.GetComponent<BossController>().vida -= damage;
        }

    }
}
