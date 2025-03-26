using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SG{
public class Item : ScriptableObject
{
    [Header("Item information")]
    public Sprite itemIcon;
    public string itemName;
}
}