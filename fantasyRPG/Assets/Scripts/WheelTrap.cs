using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelTrap : MonoBehaviour
{
    private float initialPos;
    private float finalPos;

    private bool fromInitial = true;
    // Start is called before the first frame update
    
    void Start()
    {
        initialPos = transform.localPosition.x;
        finalPos = -initialPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (fromInitial)
        {
            var to = new Vector3(finalPos, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, to, Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0,0,transform.localRotation.eulerAngles.z - 1);
            if (transform.localPosition.x == finalPos)
            {
                fromInitial = false;
            }
        }
        else
        {
            var to = new Vector3(initialPos, transform.localPosition.y, transform.localPosition.z);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, to, Time.deltaTime);
            transform.localRotation = Quaternion.Euler(0,0,transform.localRotation.eulerAngles.z + 1);
            if (transform.localPosition.x == initialPos)
            {
                fromInitial = true;
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerLogic>().Attacked(50);
        } else if (col.transform.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(100);
        }
        
    }
}
