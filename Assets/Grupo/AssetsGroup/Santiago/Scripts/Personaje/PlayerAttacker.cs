using UnityEngine;

namespace SG
{
    public class PlayerAttacker : MonoBehaviour
    {
        private AnimatorHandle animatorHandler;
        private InputHandler inputHandler;
        private float comboWindow = 0.5f; // Ventana de tiempo para el combo
        private float lastAttackTime;
        private string lastAttack;
        private int comboCount;

        private void Awake()
        {
            animatorHandler = GetComponentInChildren<AnimatorHandle>();
            inputHandler = GetComponent<InputHandler>();
        }

        private void Update()
        {
            // Resetear el combo si pasa demasiado tiempo
            if (Time.time - lastAttackTime > comboWindow)
            {
                comboCount = 0;
                lastAttack = "";
            }
        }

        public void HandleWeaponCombo(WeaponItem weapon)
        {
            if (inputHandler.comboFlag && animatorHandler.anim.GetBool("canDoCombo"))
            {
                comboCount++;
                
                switch (comboCount)
                {
                    case 1:
                        animatorHandler.PlayTargetAnimation(weapon.OHLightAttack2, true);
                        lastAttack = weapon.OHLightAttack2;
                        break;
                    case 2:
                        animatorHandler.PlayTargetAnimation(weapon.OHLightAttack3, true);
                        lastAttack = weapon.OHLightAttack3;
                        break;
                    case 3:
                        animatorHandler.PlayTargetAnimation(weapon.OHLightAttack4, true);
                        lastAttack = weapon.OHLightAttack4;
                        comboCount = 0; // Resetear después del último ataque del combo
                        break;
                }
                
                lastAttackTime = Time.time;
            }
        }

        public void HandleLightAttack(WeaponItem weapon)
        {
            // Solo permitir nuevo combo si ha pasado el tiempo o es el primer ataque
            if (Time.time - lastAttackTime > comboWindow || comboCount == 0)
            {
                comboCount = 0;
                animatorHandler.anim.SetBool("canDoCombo", true);
            }
            
            animatorHandler.PlayTargetAnimation(weapon.OHLightAttack1, true);
            lastAttack = weapon.OHLightAttack1;
            lastAttackTime = Time.time;
        }

        public void HandleHeavyAttack(WeaponItem weapon)
        {
            // Resetear combo con ataque pesado
            comboCount = 0;
            animatorHandler.PlayTargetAnimation(weapon.OHHeavyAttack1, true);
            lastAttack = weapon.OHHeavyAttack1;
            lastAttackTime = Time.time;
        }
    }
}