using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMapper : MonoBehaviour
{
    public static bool GetMoveLeft()
    {
        return Input.GetKey(KeyCode.A);
    }

    public static bool GetMoveRight()
    {
        return Input.GetKey(KeyCode.D);
    }

    public static bool GetMoveUp()
    {
        return Input.GetKey(KeyCode.W);
    }

    public static bool GetMoveDown()
    {
        return Input.GetKey(KeyCode.S);
    }
}
