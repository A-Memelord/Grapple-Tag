using UnityEngine;
using UnityEngine.InputSystem;

public class Dashing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMovement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float maxDashYSpeed;
    public float dashDuration;

    [Header("CameraEffects")]
    public PlayerCam cam;
    public float dashFov;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirections = true;
    public bool disableGravity = false;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;
    public bool canDash;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (dashCdTimer > 0)
            dashCdTimer -= Time.deltaTime;

        if (pm.grounded == true)
        {
            canDash = true;
        }
    }

    public void Dash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canDash)
        {
            canDash = false;
            if (dashCdTimer > 0) return;
            else dashCdTimer = dashCd;

            pm.dashing = true;
            pm.maxYSpeed = maxDashYSpeed;

            cam.DoFov(dashFov);

            Transform forwardT;

            if (useCameraForward)
                forwardT = playerCam; /// where you're looking
            else
                forwardT = orientation; /// where you're facing (no up or down)

            Vector3 direction = GetDirection(forwardT);

            Vector3 forceToApply = direction * dashForce + orientation.up * dashUpwardForce;

            if (disableGravity)
                rb.useGravity = false;

            delayedForceToApply = forceToApply;
            Invoke(nameof(DelayedDashForce), 0.025f);

            Invoke(nameof(ResetDash), dashDuration);
        }
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        if (resetVel)
            rb.linearVelocity = Vector3.zero;

        rb.AddForce(delayedForceToApply, ForceMode.Force);
    }

    private void ResetDash()
    {
        pm.dashing = false;
        pm.maxYSpeed = 0;

        cam.DoFov(85f);

        if (disableGravity)
            rb.useGravity = true;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        Vector3 direction = new Vector3();

        if (allowAllDirections)
            direction = forwardT.forward * GetComponent<PlayerMovement>().verticalInput + forwardT.right * GetComponent<PlayerMovement>().horizontalInput;
        else
            direction = forwardT.forward;

        if (GetComponent<PlayerMovement>().verticalInput == 0 && GetComponent<PlayerMovement>().horizontalInput == 0)
            direction = forwardT.forward;

        return direction.normalized;
    }
}
