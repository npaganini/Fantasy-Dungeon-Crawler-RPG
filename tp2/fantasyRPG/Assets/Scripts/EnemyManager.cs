using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    
    private int life = 100;

    private bool onCoolDown = false;
    private float cooldown = 0;
    private CharacterController cc;
    private float gravity = 30.87f;
    private float verticalSpeed = 0;
    public float speed = 2f;
    public GameObject player;
    public float Damping= 6.0f;
    public float fov= 160.0f;

    public Animator anmCtrl;

    // Start is called before the first frame update
    void Start()
    {
        anmCtrl = GetComponent<Animator>();
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (life <= 0)
        {
            anmCtrl.SetBool("Dead", true);
        }
        else
        {
            if (LineOfSight())
            {
                Chase();
            }
            else
            {
                anmCtrl.SetFloat("Speed_f", 0);
                
            }

            if (onCoolDown)
            {
                cooldown += Time.deltaTime;
                onCoolDown = !(cooldown > .5);
            }
        }
    }
    private void Chase ()
    {
        LookAt();
        var moveDirection = transform.forward;
     
        moveDirection *= speed;
     
        moveDirection.y -= gravity * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);
        anmCtrl.SetFloat("Speed_f", speed);
         
    }
    
    private void LookAt(){
        Quaternion rotation= Quaternion.LookRotation(player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
    }
    
    public bool LineOfSight ()
    {
        var distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= fov) {
            return true;
        }
        return false;
    }

    public void Attack(int damage)
    {
        if (!onCoolDown)
        {
            life -= damage;
            Debug.Log(life);
            onCoolDown = true;
            cooldown = 0;
        }
    }
}
