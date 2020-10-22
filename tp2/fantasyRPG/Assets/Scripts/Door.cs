using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool isOpen = false;
    public float cooldown = 1f;
    public float timer;
    public bool onCooldown = false;
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
    public void OpenClose()
    {
        if (!onCooldown) {
            if (!isOpen)
            {
                transform.Rotate(0f, -90f, 0f);
            }
            else
            {
                transform.Rotate(0f, 90f, 0f);
            }
            isOpen = !isOpen;
            onCooldown = true;
            timer = 0f;
        }
       
    }
}
