using System.Collections;
using System.Collections.Generic;
using SG;
using UnityEngine;

namespace SG{
public class PlayerManager : MonoBehaviour
{
   InputHandler inputHandler;
   Animator anim;
   public bool canDoCombo;
   public bool isInteracting;

    void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        anim = GetComponentInChildren<Animator>();
    }

     void Update()
    {
        inputHandler.isInteracting = anim.GetBool("isInteracting");
        canDoCombo = anim.GetBool("canDoCombo");
        inputHandler.rollflag = false;
        inputHandler.rb_Input = false;
        inputHandler.rt_Input = false;
    } 
}
}
