using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLogic : MonoBehaviour
{
    public CharacterController cc;
    public Animator anmCtrl;
    public float speed = 2f;
    public float interactDistance = 4f;
    // camera and rotation
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    public Weapon[] weapons;
    public Weapon equipped;
    private int eqIndex = 0;
    private float gravity = 30.87f;
    private float verticalSpeed = 0;

    private float switchCooldown = 1f;
    private float switchTimer = 0f;
    private bool switchOnCd = false;
    private float life = 100;
    private float resetCoolDown;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();

        anmCtrl = GetComponent<Animator>();
        equipped = weapons[2];
    }
    
    // Update is called once per frame
    void Update()
    {
        if (life <= 0)
        {
            resetCoolDown += Time.deltaTime;
            if (resetCoolDown > 3)
            {
                SceneManager.LoadScene("Demo");
            }
            return;
        }
        UpdatePosition();
        Rotate();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");
        }

        if (switchOnCd)
        {
            switchTimer += Time.deltaTime;
            if(switchTimer >= switchCooldown)
            {
                switchOnCd = false;
            }
        }
        //interact key
        if (Input.GetKey(KeyCode.E)) 
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.CompareTag("Door")){
                    if (Vector3.Distance(transform.position, hit.transform.position) <= interactDistance)
                    {
                        hit.transform.GetComponent<Door>().OpenClose();
                        anmCtrl.Play("Open_door");
                    }
                        
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipped.Attack(anmCtrl);
            anmCtrl.SetFloat("Speed_f", 0f);
        }
        if (Input.GetKey(KeyCode.Tab) && !switchOnCd)
        {
            equipped.gameObject.SetActive(false);
            eqIndex++;
            if (eqIndex == weapons.Length)
                eqIndex = 0;
            equipped = weapons[eqIndex];
            equipped.gameObject.SetActive(true);
            switchTimer = 0f;
            switchOnCd = true;
        }

        // if(Input.GetKeyDown(KeyCode.Escape))
        // {
        //     PauseMenu.Pause();
        // }
    }
    
    public void Rotate()
    {
        if (Time.deltaTime != 0)
        {
            float horizontalRotation = Input.GetAxis("Mouse X");
            float verticalRotation = Input.GetAxis("Mouse Y");

            transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
            cameraHolder.Rotate(-verticalRotation * mouseSensitivity, 0, 0);

            Vector3 currentRotation = cameraHolder.localEulerAngles;
            if (currentRotation.x > 180) currentRotation.x -= 360;
            currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
            cameraHolder.localRotation = Quaternion.Euler(currentRotation);
        }
    }

    void UpdatePosition()
    {
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (cc.isGrounded) verticalSpeed = 0;
        else verticalSpeed -= gravity * Time.deltaTime;
        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        
        Vector3 move = transform.forward * verticalMove + transform.right * horizontalMove;
        cc.Move(speed * Time.deltaTime * move + gravityMove * Time.deltaTime);
        
        //Debug.Log("HORIZONTAL" + horizontalMove);
        //Debug.Log("VERTICAL " + verticalMove);

        float spd = speed * verticalMove;
        anmCtrl.SetFloat("Speed_f", spd);
    }

    public void Attacked( float damage)
    {
        life -= damage;
        Debug.Log("PLAYER LIFE:" + life);
        if (life <= 0)
        {
            anmCtrl.SetBool("Dead", true);
            
        }
    }
}
