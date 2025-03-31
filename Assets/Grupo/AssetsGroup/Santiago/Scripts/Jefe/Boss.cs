using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Rendering;

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

    private bool canChangeState = true;

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

    public void FinalAnim(){

        rutina = 0;
        anim.SetBool("Attack", false);
        rango.GetComponent<CapsuleCollider>().enabled = true;
       // lanzaLlamas = false;
        jumpDistance = 0;
        directionSkill = false;
    }

    public void DirectionAttackStart(){

        directionSkill = true;
    }

    public void DirectionAttackFinal(){

        directionSkill = false;
    }

    
    ///  MELEE//
    
    public void ColliderWeaponTrue(){

        hit[hitSelect].GetComponent<SphereCollider>().enabled = true;
    }

    public void ColliderWeaponFlase(){

        hit[hitSelect].GetComponent<SphereCollider>().enabled = false;
    }

    //LANZA LLAMAS//

    /*public GameObject GetBala(){

        for (int i = 0; i < pool.Count; i++){

            if(!pool[i].activeInHierarchy){

                pool[i].SetActive(true);
                return pool[i];
            }
        }

        GameObject obj = Instantiate(fire, cabeza.transform.position, cabeza.transform.rotation) as GameObject;
        pool.Add(obj);
        return obj;
        
    }*/
    /*public void LanzaLlamasSkill(){

        cronometro2 += 1*Time.deltaTime;
        if(cronometro2 > 0.1f){

            //GameObject obj = GetBala();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            cronometro2 = 0;
        }
    }*/

   /* public void StartFire(){

        lanzaLlamas = true;
        
    }
    public void StopFire(){

        lanzaLlamas = false;
    }*/ 

    //BOLA DE FUEGO//
    /*public GameObject GetFireBall(){

        for (int i = 0; i < pool2.Count; i++){

            if(!pool2[i].activeInHierarchy){

                pool2[i].SetActive(true);
                return pool[i];
            }
        }
        GameObject obj = Instantiate(fireBall, point.transform.position, point.transform.rotation) as GameObject;
        pool2.Add(obj);
        return obj;
    }
    
     public void FireBallSkill(){

        cronometro2 += 1*Time.deltaTime;
        if(cronometro2 > 0.1f){

            GameObject obj = GetfireBall();
            obj.transform.position = cabeza.transform.position;
            obj.transform.rotation = cabeza.transform.rotation;
            
        }
    }
    */

    public void Vivo(){

        if(hpMin < 500){

            fase = 2;
            timeRutina = 1;
        }
        ComportamientoBoss();

       /* if(lanzaLlamas){

            LanzaLlamasSkill();
        }*/

    }
    
    void Start()
    {
        anim = GetComponent<Animator>();
        target = GameObject.Find("Character2");
    }

    void Update()
    {
        barra.fillAmount = hpMin / hpMax;
        if(hpMin > 0){

            Vivo();
        }
        else{

            if(!muerte){

                anim.SetTrigger("Dead");
                //musica.enabled = false;
                muerte = true;
            }
        }
    }

    public void ComportamientoBoss()
{
    if (Vector3.Distance(transform.position, target.transform.position) < 15)
    {
        var lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        if (Vector3.Distance(transform.position, target.transform.position) > 1 && !atacando && canChangeState)
        {
            switch (rutina)
            {
                case 0: // Walk
                    if (transform.rotation == rotation)
                    {
                        transform.Translate(Vector3.forward * speed * Time.deltaTime);
                    }
                    break;

                case 1: // Run
                    if (transform.rotation == rotation)
                    {
                        transform.Translate(Vector3.forward * speed * 2 * Time.deltaTime);
                    }
                    break;

                case 2: // Ataque en Salto
                    if (fase == 2)
                    {
                        StartCoroutine(JumpAttack(rotation));
                    }
                    break;
            }
        }
    }
}
private IEnumerator JumpAttack(Quaternion targetRotation)
{
    canChangeState = false;
    atacando = true;
    anim.SetBool("Attack", true);
    
    float attackDuration = 1.5f; // Ajusta según tu animación
    float timer = 0f;

    while (timer < attackDuration)
    {
        timer += Time.deltaTime;
        
        if (directionSkill)
        {
            transform.Translate(Vector3.forward * 8 * Time.deltaTime);
        }
        
        yield return null;
    }
     anim.SetBool("Attack", false);
    atacando = false;
    canChangeState = true;
    rutina = 0; // Volver a estado neutral

}
}
