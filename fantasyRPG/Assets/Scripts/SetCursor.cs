using UnityEngine;

public class SetCursor : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Texture2D crosshair;
    public Texture2D pauseMenu;

    void Start()
    {
        EnterPauseMenu();
    }

    public void EnterGameMode()
    {
        // set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(crosshair.width/2, crosshair.height/2);
     
        // Sets the cursor to the Crosshair sprite with given offset 
        // and automatic switching to hardware default if necessary
        Cursor.SetCursor(crosshair, cursorOffset, CursorMode.ForceSoftware);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnterPauseMenu()
    {
        Cursor.SetCursor(pauseMenu, new Vector2(crosshair.width/2, crosshair.height/3), CursorMode.Auto);
        Cursor.lockState = CursorLockMode.None;
    }
}
