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
            Chase();
            if (onCoolDown)
            {
                cooldown += Time.deltaTime;
                onCoolDown = !(cooldown > .5);
            }
        }
    }
    
    void UpdatePosition()
    {
        float horizontalMove = ((float)Random.Range(0, 99))/100f;
        float verticalMove = ((float)Random.Range(0, 99))/100f;

        if (cc.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        Debug.Log(move);
        cc.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        

        float spd = speed * verticalMove;
        anmCtrl.SetFloat("Speed_f", spd);
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
        // Rotate to look at player.
        Quaternion rotation= Quaternion.LookRotation(player.transform.position - transform.position);
        // Dampening will slow the turn speed of enemy.
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
        //    transform.LookAt(Target);
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
