using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMapper 
{
    public static float GetMoveHorizontal(int playerNum = 0)
    {
        float keyb = (Input.GetKey(KeyCode.A) ? -1f : 0f) + (Input.GetKey(KeyCode.D) ? 1f : 0f);
        return Input.GetAxis("Horizontal") + keyb;
    }

    public static float GetMoveVertical(int playerNum = 0)
    {
        float keyb = (Input.GetKey(KeyCode.W) ? 1f : 0f) + (Input.GetKey(KeyCode.S) ? -1f : 0f);
        return Input.GetAxis("Vertical") + keyb;
    }


}
