using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX;
    public float sensY;

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
        xTurn = ctx.ReadValue<Vector2>().x * Time.deltaTime * sensX;
        yTurn = ctx.ReadValue<Vector2>().y * Time.deltaTime * sensY;
    }

    public void DoFov(float endValue)
    {

    }

    public void DoTilt(float zTilt)
    {

    }
}
