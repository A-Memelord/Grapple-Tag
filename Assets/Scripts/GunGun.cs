using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunGun : MonoBehaviour
{
    [Header("References")]
    private PlayerMovement pm;
    public Transform cam;
    public Transform gunTip;
    public Transform gun;
    public LayerMask whatIsShootable;

    [Header("Firing")]
    public float maxFireDistance;
    public float grappleDelayTime;
    public float overshootYAxis;

    [Header("Cooldown")]
    public float reloadCd;
    private float reloadCdTimer;
    public LineRenderer lr;

    [Header("Input")]
    public KeyCode shootKey = KeyCode.Mouse1;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {

        if (reloadCdTimer > 0)
        {
            reloadCdTimer -= Time.deltaTime;
        }
        if (reloadCdTimer < reloadCd/1.5)
        {
            lr.enabled = false;
        }
    }

    public void Fire(InputAction.CallbackContext ctx)
    {
        if (reloadCdTimer > 0) return;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxFireDistance, whatIsShootable))
        {
            var h = hit.collider.GetComponent<GettingShotManager>();
            Debug.Log(h);
            if (h)
            {
                h.BroadcastMessage("Hit");
            }
        }
        lr.enabled = true;
        lr.SetPosition(0, gunTip.position);
        
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxFireDistance))
        {
            lr.SetPosition(1, hit.point);
        }
        else { lr.SetPosition(1, cam.position + cam.forward * maxFireDistance); }
        gun.GetComponent<AudioSource>().Play();



        reloadCdTimer = reloadCd;
    }
}
    


        
    