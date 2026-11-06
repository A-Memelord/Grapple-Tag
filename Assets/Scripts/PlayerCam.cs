using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{

    [Header("Camera Tilt")]
    [SerializeField] private float tiltSmoothness;
    [SerializeField] private float tiltAngle;

    private float currentTilt = 0f;
    private float targetTilt = 0f;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;


    public Transform orientation;

    float xRotation;
    float yRotation;

   
    private float xTurn, yTurn;

    private Vector2 moveInput;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        targetTilt = -moveInput.x * tiltAngle;


        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmoothness * Time.deltaTime);



        yRotation += xTurn;

        xRotation -= yTurn;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);


        Vector3 currentAngles = transform.localRotation.eulerAngles;
        transform.localRotation = Quaternion.Euler(currentAngles.x, currentAngles.y, currentTilt);
        Debug.Log(currentTilt);
        
    }

    public void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
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
