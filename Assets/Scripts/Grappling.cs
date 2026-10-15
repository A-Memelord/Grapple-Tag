using UnityEngine;
using UnityEngine.InputSystem;

public class Grappling : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask whatIsGrappleable;
    public LineRenderer lr;

    [Header("Grappling")]
    public float maxGrappleDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    private Vector3 grapplePoint;

    [Header("Cooldown")]
    public float grapplingCd;
    private float grapplingCdTimer;

    private bool grappling;

    public Animation anim;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (grappling)
        {

            RaycastHit hit;

            if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, whatIsGrappleable))
            {
                grapplePoint = hit.point;
                Invoke(nameof(ExecuteGrapple), grappleDelayTime);
            }

            else
            {
                grapplePoint = cam.position + cam.forward * maxGrappleDistance;
                Invoke(nameof(StopGrapple), grappleDelayTime);
            }

            lr.enabled = true;

            Debug.Log("2");
            lr.positionCount = 2;

            lr.SetPosition(0, gunTip.position);
            lr.SetPosition(1, grapplePoint);

            //anim.Play();
        }

        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if(grappling)
        {
            lr.SetPosition(0, gunTip.position);
        }
    }

    public void StartGrapple(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (grapplingCdTimer > 0)
                return;

            grappling = true;
        }
    }

    private void ExecuteGrapple()
    {
        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0)
        {
            highestPointOnArc = overshootYAxis;
        }

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        grappling = false;

        grapplingCdTimer = grapplingCd;
        lr.enabled = false;

        lr.positionCount = 0;
    }
}
