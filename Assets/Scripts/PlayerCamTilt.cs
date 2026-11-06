using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamTilt : MonoBehaviour
{

    [SerializeField] private InputActionReference moveActionReference;

    [Header("Camera Tilt")]
    [SerializeField] private float tiltSmoothness;
    [SerializeField] private float tiltAngle;

    private float currentTilt = 0f;
    private float targetTilt = 0f;

    // Update is called once per frame
    void Update()
    {
        Vector2 moveInput = moveActionReference.action.ReadValue<Vector2>();


        targetTilt = -moveInput.x * tiltAngle;


        currentTilt = Mathf.Lerp(currentTilt, targetTilt, tiltSmoothness * Time.deltaTime);

        Vector3 currentAngles = transform.localRotation.eulerAngles;
        transform.localRotation = quaternion.Euler(currentAngles.x, currentAngles.y, currentTilt);
    }
}
