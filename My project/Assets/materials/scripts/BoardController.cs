using UnityEngine;

public class BoardController : MonoBehaviour
{
    [Header("Control Settings")]
    [SerializeField] private float tiltSpeed = 25f;    // How fast the board tilts
    [SerializeField] private float maxTiltAngle = 15f; // Max tilt in degrees

    private float rotationX = 0f;
    private float rotationZ = 0f;

    void Update()
    {
        // Get keyboard/controller input (WASD or Arrow keys)
        float inputX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float inputZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Calculate the target tilt angles based on time and input
        // Pressing 'W' tilts the board forward (positive X rotation)
        // Pressing 'A' tilts the board left (positive Z rotation)
        rotationX += inputZ * tiltSpeed * Time.deltaTime;
        rotationZ -= inputX * tiltSpeed * Time.deltaTime;

        // Clamp the angles so the board can't tilt past your limit
        rotationX = Mathf.Clamp(rotationX, -maxTiltAngle, maxTiltAngle);
        rotationZ = Mathf.Clamp(rotationZ, -maxTiltAngle, maxTiltAngle);

        // Apply the rotation directly to the platform
        transform.localRotation = Quaternion.Euler(rotationX, 0f, rotationZ);
    }
}
