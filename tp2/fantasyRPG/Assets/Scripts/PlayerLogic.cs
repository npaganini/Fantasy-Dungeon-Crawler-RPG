using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    public CharacterController cc;
    public Animator anmCtrl;
    public float speed = 2f;
    
    // camera and rotation
    public Transform cameraHolder;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    
    
    private float gravity = 30.87f;
    private float verticalSpeed = 0;

    void Start()
    {
        cc = GetComponent<CharacterController>();

        anmCtrl = GetComponent<Animator>();
    }
    
    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
        Rotate();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
                Debug.Log("Hit " + hitInfo.transform.gameObject.name);
            }
        }
    }
    
    public void Rotate()
    {
        float horizontalRotation = Input.GetAxis("Mouse X");
        float verticalRotation = Input.GetAxis("Mouse Y");
        
        transform.Rotate(0, horizontalRotation * mouseSensitivity, 0);
        cameraHolder.Rotate(-verticalRotation*mouseSensitivity,0,0);

        Vector3 currentRotation = cameraHolder.localEulerAngles;
        if (currentRotation.x > 180) currentRotation.x -= 360;
        currentRotation.x = Mathf.Clamp(currentRotation.x, upLimit, downLimit);
        cameraHolder.localRotation = Quaternion.Euler(currentRotation);
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
}
