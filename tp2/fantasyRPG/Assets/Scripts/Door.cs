using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float cooldown = 1f;
    public float timer;
    public bool onCooldown = false;
    public bool isJailDoor;
    public int keysToOpen;
    public List<EnemyManager> enemiesToActivate;
    
    // Start is called before the first frame update
    void Update()
    {
        if (onCooldown)
        {
            timer += Time.deltaTime;
            if(timer >= cooldown)
            {
                onCooldown = false;
            }
        }
    }

    // Update is called once per frame
    public void OpenClose(int keys)
    {
        if (!onCooldown) {
            if (!isOpen)
            {
                Open(keys);
            }
            else
            {
                Close();
                isOpen = !isOpen;
            }
            onCooldown = true;
            timer = 0f;
        }

    }

    public void Open(int keys)
    {
        if (isJailDoor)
        {
            if(keys >= keysToOpen){
                gameObject.transform.GetChild(0).transform.Rotate(0f, -90f, 0f);
                isOpen = !isOpen;
            }
        }
        else
        {
            transform.Rotate(0f, -90f, 0f);
            isOpen = !isOpen;
        }

        foreach (var enemy in enemiesToActivate)
        {
            enemy.Activate();
        }

    }

    public void Close()
    {
        //if (isJailDoor)
        //{
          //  gameObject.transform.GetChild(0).transform.Rotate(0f, 90f, 0f);
        //}
        //else
        //{
          //  transform.Rotate(0f, 90f, 0f);
        //}
    }
}
