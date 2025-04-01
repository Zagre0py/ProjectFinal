using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePLayer : MonoBehaviour
{
    public int damage;

    public GameObject boss;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag==("Boss")){

            boss.GetComponent<BossController>().vida -= damage;
        }

    }
}
