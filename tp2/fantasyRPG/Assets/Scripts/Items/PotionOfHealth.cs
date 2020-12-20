using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionOfHealth : EnemyDrop
{
    void Start()
    {
        TypeOfDrop = BuffItem.Health;
    }
}
