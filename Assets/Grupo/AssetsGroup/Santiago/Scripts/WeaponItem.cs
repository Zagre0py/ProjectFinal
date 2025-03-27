using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG
{

    [CreateAssetMenu(menuName = "Items/Weapon Item")]
    public class WeaponItem : Item
    {
        public GameObject modelPrefab;
        public bool isUnarmed;
        [Header("One Handed Attack Animation")]
        public string OHLightAttack1;
        public string OHLightAttack2;
        public string OHLightAttack3;
        public string OHLightAttack4;
        public string OHHeavyAttack1; 
    }
}