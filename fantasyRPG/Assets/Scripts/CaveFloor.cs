using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveFloor : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerLogic>().Attacked(100);
        } else if (col.transform.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyManager>().Kill();
        }
        
    }
}
