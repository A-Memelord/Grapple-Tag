using UnityEngine;
using UnityEngine.InputSystem;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerObj;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = playerObj.localScale.y;
    }

    private void FixedUpdate()
    {
        if(pm.sliding)
        {
            SlidingMovemnet();
        }
    }

    public void Slide(InputAction.CallbackContext ctx)
    {
        if (ctx.started)
            StartSlide();
        else if (ctx.canceled)
            StopSliding();
    }

    private void StartSlide()
    {
        if ((GetComponent<PlayerMovement>().horizontalInput != 0 || GetComponent<PlayerMovement>().verticalInput != 0))
        {
            pm.sliding = true;

            playerObj.localScale = new Vector3(playerObj.localScale.x, slideYScale, playerObj.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            slideTimer = maxSlideTime;
        }
    }

    private void StopSliding()
    {
        if (pm.sliding)
        {
            pm.sliding = false;

            playerObj.localScale = new Vector3(playerObj.localScale.x, startYScale, playerObj.localScale.z);
        }
    }

    public void SlidingMovemnet()
    {
        if(!pm.OnSlope() || rb.linearVelocity.y > -0.1f)
        {
            Vector3 inputDirection = orientation.forward * GetComponent<PlayerMovement>().verticalInput + orientation.right * GetComponent<PlayerMovement>().horizontalInput;

            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);
            slideTimer -= Time.deltaTime;
        }

        else
        {
            Vector3 inputDirection = orientation.forward * GetComponent<PlayerMovement>().verticalInput + orientation.right * GetComponent<PlayerMovement>().horizontalInput;
            rb.AddForce(pm.GetSlopeMoveDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0)
        {
            StopSliding();
        }
    }
}
