using UnityEngine;

public class CursorLockDisableForTitle : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created



    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    
    
}
