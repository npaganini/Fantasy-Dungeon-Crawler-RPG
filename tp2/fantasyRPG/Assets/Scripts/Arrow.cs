using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Vector3 direction;

    private Rigidbody rb;

    public Camera camera;
    private Vector3 directionVector;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();       
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit)) {
            directionVector = hit.point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position ,directionVector) > .1 )
        {       
            float step =  20 * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, directionVector, step);
        }
        else
        {
            rb.constraints =  RigidbodyConstraints.FreezePosition;
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        if (string.Compare(col.gameObject.tag, "Enemy", StringComparison.Ordinal) == 0)
        {
            col.gameObject.GetComponent<EnemyManager>().Attacked(20);
        }
        Destroy(gameObject);
    }
}
