using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FakeWallActivateEnemies : MonoBehaviour
{
    
    public List<EnemyManager> enemiesToActivate;
    
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.CompareTag("Player"))
        {
            foreach (var enemy in enemiesToActivate)
            {
                enemy.Activate();
            }
        }
    }
}
