using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerLogic : MonoBehaviour
{
    // camera and rotation
    public Transform cameraHolder;
    protected AudioSource audiosource;
    public CharacterController cc;
    public Animator anmCtrl;
    public float speed = 2f;
    public float interactDistance = 4f;
    public Slider healthSlider;
    public float mouseSensitivity = 2f;
    public float upLimit = -50;
    public float downLimit = 50;
    public Weapon[] weapons;
    public Weapon equipped;
    public Image equippedWeaponIcon;
    private int eqIndex = 0;
    private float gravity = 30.87f;
    private float verticalSpeed = 0;
    private float switchCooldown = 1f;
    private float switchTimer = 0f;
    private bool switchOnCd = false;
    private float life = 100;
    private float resetCoolDown;
    private int keys = 0;
    public TextMeshProUGUI amountOfKeys;

    private bool isRegenerating = false;
    private float regenCd = 8f;
    private float regenTimer = 0f;
    private bool win = false;
    
    void Start()
    {
        cc = GetComponent<CharacterController>();
        audiosource = GetComponent<AudioSource>();
        anmCtrl = GetComponent<Animator>();
        SetEquippedWeapon(0);
        UpdateHealth();
    }
    
    // Update is called once per frame
    void Update()
    {
        Debug.Log(anmCtrl.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        if (win)
        {
            Invoke("returnToMenu", 5.0f);
            return;
        }
        
        if(life < 100 && isRegenerating)
        {
            life += 1;
            UpdateHealth();
            if (life == 100)
                isRegenerating = false;
        }
        if(life < 100 && !isRegenerating)
        {
            regenTimer += Time.deltaTime;
            if(regenTimer >= regenCd)
            {
                isRegenerating = true;
                regenTimer = 0f;
            }

        }

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
            RaycastHit[] allHits;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            allHits = Physics.RaycastAll(ray, 100).OrderBy(h=>h.distance).ToArray();
            foreach(RaycastHit hit in allHits){
                Debug.Log(hit.transform.name);
                if (!hit.transform.CompareTag("Player"))
                {
                    if (hit.transform.CompareTag("Door"))
                    {
                        if (Vector3.Distance(transform.position, hit.transform.position) <= interactDistance)
                        {
                            hit.transform.GetComponent<Door>().OpenClose(keys);
                            anmCtrl.Play("Open_door");
                        }
                        else
                        {
                            Debug.Log(Vector3.Distance(transform.position, hit.transform.position));
                        }

                    } else if (hit.transform.CompareTag("Key"))
                    {
                        keys++;
                        hit.transform.gameObject.SetActive(false);
                        Destroy(hit.transform.gameObject);
                        amountOfKeys.text = keys.ToString();
                        Debug.Log(keys);
                    }
                    break;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log(keys);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            equipped.Attack(anmCtrl);
            anmCtrl.SetFloat("Speed_f", 0f);
        }
        if (Input.GetKey(KeyCode.Tab) && !switchOnCd)
        {
            audiosource.Play();
            equipped.gameObject.SetActive(false);
            eqIndex++;
            if (eqIndex == weapons.Length)
                eqIndex = 0;
            SetEquippedWeapon(eqIndex);
            equipped.gameObject.SetActive(true);
            switchTimer = 0f;
            switchOnCd = true;
        }
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
        UpdateHealth();
        isRegenerating = false;
        regenTimer = 0f;
        if (life <= 0)
        {
            anmCtrl.SetBool("Dead", true);
            
        }
    }
    private void UpdateHealth()
    {
        healthSlider.value = life;
    }

    private void SetEquippedWeapon(int index)
    {
        equipped = weapons[index];
        equippedWeaponIcon.GetComponent<Image>().sprite = equipped.GetIcon();
    }

    public void Win()
    {
        anmCtrl.SetBool("Win", true);
        Debug.Log("PLAY");
        win = true;
        cameraHolder.localPosition = new Vector3(0.140000001f, 2.8900001f, 4.96999979f);
        cameraHolder.localRotation = new Quaternion(0,1,0,0);
    }

    public void returnToMenu()
    {
        SceneManager.LoadScene("Scenes/StartMenu");
    }
    
    
    
}
