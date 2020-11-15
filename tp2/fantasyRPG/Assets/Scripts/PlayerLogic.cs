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
    private readonly Weapon[] _weapons = new Weapon[2];
    private Weapon _equipped;
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
    void Start()
    {
        cc = GetComponent<CharacterController>();
        audiosource = GetComponent<AudioSource>();
        anmCtrl = GetComponent<Animator>();
        SetWeaponArray();
        SetEquippedWeapon(0);
        UpdateHealth();
    }
    
    // Update is called once per frame
    void Update()
    {
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
                        var objectToInteract = hit.transform.gameObject;
                        keys++;
                        objectToInteract.SetActive(false);
                        Destroy(objectToInteract);
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
            _equipped.Attack(anmCtrl);
            anmCtrl.SetFloat("Speed_f", 0f);
        }
        if (Input.GetKey(KeyCode.Tab) && !switchOnCd)
        {
            audiosource.Play();
            _equipped.gameObject.SetActive(false);
            eqIndex++;
            if (eqIndex == _weapons.Length)
                eqIndex = 0;
            SetEquippedWeapon(eqIndex);
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

        var t = transform;
        Vector3 move = t.forward * verticalMove + t.right * horizontalMove;
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
        _equipped = _weapons[index];
        equippedWeaponIcon.GetComponent<Image>().sprite = _equipped.GetIcon();
        _equipped.gameObject.SetActive(true);
    }

    private void SetWeaponArray()
    {
        var typeChosen = GameManager.Instance.GetTypeChosen();
        switch (typeChosen)
        {
            case 0:    // magic & melee
                _weapons[0] = gameObject.GetComponentInChildren<Staff>(true);
                _weapons[1] = gameObject.GetComponentInChildren<Sword>(true);
                break;
            case 1:    // melee & ranged
                _weapons[0] = gameObject.GetComponentInChildren<Sword>(true);
                _weapons[1] = gameObject.GetComponentInChildren<Bow>(true);
                break;
            case 2:    // ranged & magic
                _weapons[0] = gameObject.GetComponentInChildren<Bow>(true);
                _weapons[1] = gameObject.GetComponentInChildren<Staff>(true);
                break;
            default:
                Debug.Log("No weapon types selected!");
                SceneManager.LoadScene("Scenes/MainMenu");
                break;
        }
    }
}
