using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionOfShield : EnemyDrop
{
    void Start()
    {
        TypeOfDrop = BuffItem.Shield;
    }
}
