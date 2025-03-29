using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangoBoss : MonoBehaviour
{
    public Animator anim;
    public Boss boss;
    public int melee;

    void OnTriggerEnter(Collider coll)
    {
        melee = Random.Range(0, 4);

        switch (melee)
        {

            case 0:
                //golpe 1
                anim.SetFloat("Skills", 0);
                boss.hitSelect = 0;
                break;

            case 1:

                //golpe 2

                anim.SetFloat("Skills", 0);
                boss.hitSelect = 1;
                break;

           /* case 2:
                //jump
                anim.SetFloat("Skills", 0);
                boss.hitSelect = 2;
                break;*/

           /* case 3:
                //fire ball
                if (boss.fase == 2)
                {

                    anim.SetFloat("Skills", 0);

                }

                 else
                    {

                        melee = 0;
                    }
                
                break;*/
        }

        anim.SetBool("Walk", false);
        anim.SetBool("Run", false);
        anim.SetBool("Attack", false);
        boss.atacando = true;
        GetComponent<CapsuleCollider>().enabled = false;

    }

    void Start()
    {

    }


    void Update()
    {

    }
}
