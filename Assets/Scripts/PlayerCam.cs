using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.ProBuilder.MeshOperations;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;
    public float sensXCon;
    public float sensYCon;

    public Transform orientation;

    float xRotation;
    float yRotation;

    private float xTurn, yTurn;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        yRotation += xTurn;

        xRotation -= yTurn;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void Turn(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.control);
        if (ctx.control != InputSystem.FindControl("<Gamepad>/rightStick"))
        {
            xTurn = ctx.ReadValue<Vector2>().x * Time.deltaTime * sensX;
            yTurn = ctx.ReadValue<Vector2>().y * Time.deltaTime * sensY;
        }
        else 
        {
            xTurn = ctx.ReadValue<Vector2>().x * Time.deltaTime * sensXCon;
            yTurn = ctx.ReadValue<Vector2>().y * Time.deltaTime * sensYCon;
        }
    
    }

    public void DoFov(float endValue)
    {

    }

    public void DoTilt(float zTilt)
    {

    }
}
