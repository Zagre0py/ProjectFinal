using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmDamage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.CompareTag("Boss")){

            coll.GetComponent<Boss>().hpMin -= 50;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
