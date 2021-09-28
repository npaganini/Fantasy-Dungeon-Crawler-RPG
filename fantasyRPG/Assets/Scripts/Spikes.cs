using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update

    private float accum = 0;
    public States currentState;

    private Vector3 up;
    private Vector3 down;
    public float speed = 1f;
    public bool active = true;
    public enum States
    {
        up,
        restUp,
        down,
        restDown
    }

    private void Start()
    {
        if (currentState == States.restDown)
        {
            down = transform.position;
            up = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        }
        else
        {
            up = transform.position;
            down = new Vector3(transform.position.x, transform.position.y - 5, transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        accum += Time.deltaTime;
        if (!active)
        {
            return;
        }
        switch (currentState)
        {
            case States.up:
                transform.position = Vector3.MoveTowards(transform.position, up, speed * Time.deltaTime);
                if (accum > 6)
                {
                    currentState = States.restUp;
                    accum = 0;
                }
                break;
            case States.down:
                transform.position = Vector3.MoveTowards(transform.position, down, speed * Time.deltaTime);
                if (accum > 6)
                {
                    currentState = States.restDown;
                    accum = 0;
                }
                break;
            case States.restUp:
                if (accum > 1)
                {
                    currentState = States.down;
                    accum = 0;
                }
                break;
            case States.restDown:
                if (accum > 1)
                {
                    currentState = States.up;
                    accum = 0;
                }
                break;


        }
    }

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
