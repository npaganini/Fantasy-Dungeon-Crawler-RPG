using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float cooldown = 1f;
    public float timer;
    public bool onCooldown = false;
    private bool alreadyOpen = false;
    public bool isJailDoor;
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
    public int OpenClose(int keys)
    {
        if (!onCooldown) {
            if (!isOpen)
            {
                keys = Open(keys);
            }
            else
            {
                Close();
                isOpen = !isOpen;
            }
            onCooldown = true;
            timer = 0f;
        }

        return keys;
    }

    public int Open(int keys)
    {
        if (isJailDoor)
        {
            Debug.Log(keys);
            if (keys > 0 || alreadyOpen)
            {
                gameObject.transform.GetChild(0).transform.Rotate(0f, -90f, 0f);
                if (!alreadyOpen)
                {
                    alreadyOpen = true;
                    keys--;
                }
                isOpen = !isOpen;
            }
        }
        else
        {
            transform.Rotate(0f, -90f, 0f);
            isOpen = !isOpen;
        }

        return keys;
    }

    public void Close()
    {
        if (isJailDoor)
        {
            gameObject.transform.GetChild(0).transform.Rotate(0f, 90f, 0f);
        }
        else
        {
            transform.Rotate(0f, 90f, 0f);
        }
        
    }
}
