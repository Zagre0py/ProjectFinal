using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    public float timeRutina;
    public Animator anim;
    public Quaternion angulo;
    public float grado;
    public GameObject target;
    public bool atacando;
    public RangoBoss rango;
    public float speed;
    public GameObject[] hit;
    public int hitSelect;


    ////// ATAQUE EN SALTO///////
    public float jumpDistance;
    public bool directionSkill;

    /////////////////////////////
    
    public int fase = 1;
    public float hpMin;
    public float hpMax;
    public Image barra;
    public AudioSource musica;
    public bool muerte;


    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("PLayer");
    }

    public void ComportamientoBoss(){

        if(Vector3.Distance(transform.position, target.transform.position)<15){

            var lookPos = target.transform.position = transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
           // point.tranform.lookAt(target.transform.position); CODIGO PARA ATAQUE A DISTANCIA EJ: BOLAS DE FUEGO

           if(Vector3.Distance(transform.position, target.transform.position)> 1 && !atacando){

            switch(rutina){

                case 0:
                //walk//
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                anim.SetBool("Walk", true);
                anim.SetBool("Run", false);

                if(transform.rotation == rotation){

                    transform.Translate(Vector3.forward * speed * Time.deltaTime);
                }
                anim.SetBool("Attack", false);
                cronometro += 1 * Time.deltaTime;
                if(cronometro > timeRutina){
                    rutina = Random.Range(0,5);
                    cronometro = 0;
                }
                break;
                case 1:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                anim.SetBool("Walk", false);
                anim.SetBool("Run", true);

                if(transform.rotation == rotation){

                    transform.Translate(Vector3.forward * speed*2 * Time.deltaTime);
                }
                anim.SetBool("Attack", false);
                break;

                case 2:
                ///Lanza Llamas/// aun no se usa!!!!
                anim.SetBool("Walk", false);
                anim.SetBool("Run", false);
                anim.SetBool("Attack", false);
                anim.SetFloat("Skills", 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);

                rango.GetComponent<CapsuleCollider>().enabled = false;
                break;

                case 3:
                ///Ataque en Salto///
                if(fase == 2){

                    jumpDistance += 1 * Time.deltaTime;
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Attack", true);
                    anim.SetFloat("Skills", 0);
                    hitSelect = 3;
                
                    rango.GetComponent<CapsuleCollider>().enabled = false;

                    if(directionSkill){

                    
                        if(jumpDistance < 1f){
                            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
                        }
                        transform.Translate(Vector3.forward * 8 * Time.deltaTime);
                    }
                }
                else{
                    rutina = 0;
                    cronometro = 0;
                }
                break;

                case 4:
                    ///Fire ball///
                    
                    if(fase == 2){
                    anim.SetBool("Walk", false);
                    anim.SetBool("Run", false);
                    anim.SetBool("Attack", false);
                    anim.SetFloat("Skills", 0);
                    rango.GetComponent<CapsuleCollider>().enabled = false;
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 0.5f);
                    }

                    else{

                        rutina = 0;
                        cronometro = 0;
                    }
                    break;
            }
           }
        }
    }

}
