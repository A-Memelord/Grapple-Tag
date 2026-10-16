using Unity.Entities;
using UnityEngine;

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
    public Rigidbody bullet;

    [Header("Input")]
    public KeyCode shootKey = KeyCode.Mouse1;

    void Start()
    {
        pm = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(shootKey))
        {
            Fire();
            
        }

        if (reloadCdTimer > 0)
        {
            reloadCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {

    }

    private void Fire()
    {
        if (reloadCdTimer > 0) return;

        RaycastHit hit;

        if (Physics.Raycast(cam.position, cam.forward, out hit, maxFireDistance, whatIsShootable))
        {
            if (hit.collider.GetComponent<GettingShotManager>()) {
                hit.collider.GetComponent<GettingShotManager>().BroadcastMessage("Hit");
            }
        }
        Rigidbody b = Instantiate(bullet, new Vector3 (gunTip.position.x, gunTip.position.y, gunTip.position.z), new Quaternion (gun.rotation.x, gun.rotation.y+90, gun.rotation.z, gun.rotation.w));
        b.AddForce(gun.transform.forward * 10000);
        reloadCdTimer = reloadCd;

        Debug.Log("fired");
    }
}
    


        
    