using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoss : MonoBehaviour
{
    public int damage;

    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("PJ")){

           // coll.GetComponent//<nombre del codigo de la vida del personaje//>().hpMin -= damage;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
