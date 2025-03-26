using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
public class PlayerAttacker : MonoBehaviour
{
    AnimatorHandle animatorHandler;

    private void Awake() {
        animatorHandler = GetComponentInChildren<AnimatorHandle>();
    }
    public void  HandleLightAttack(WeaponItem weapon){

        animatorHandler.PlayTargetAnimation(weapon.OHLightAttack1, true);

    }

    public void HandleHeavyAttack(WeaponItem weapon){

        animatorHandler.PlayTargetAnimation(weapon.OHHeavyAttack1, true);

    }
}
}