using UnityEngine;

public class DynamicPivotController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform marble;       // Assign your Marble here
    [SerializeField] private Transform dynamicPivot; // Assign the Dynamic Pivot empty object here
    [SerializeField] private Transform mazeGeometry; // Assign the Maze Art/Geometry object here

    [Header("Control Settings")]
    [SerializeField] private float tiltSpeed = 25f;
    [SerializeField] private float maxTiltAngle = 15f;

    private float rotationX = 0f;
    private float rotationZ = 0f;

    void Update()
    {
        // 1. Shift the pivot to the marble's X/Z position before tilting
        UpdatePivotPosition();

        // 2. Get player input
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        // 3. Calculate and clamp the tilt
        rotationX += inputZ * tiltSpeed * Time.deltaTime;
        rotationZ -= inputX * tiltSpeed * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -maxTiltAngle, maxTiltAngle);
        rotationZ = Mathf.Clamp(rotationZ, -maxTiltAngle, maxTiltAngle);

        // 4. Rotate the pivot point
        dynamicPivot.localRotation = Quaternion.Euler(rotationX, 0f, rotationZ);
    }

    void UpdatePivotPosition()
    {
        // Get the marble's position relative to this local system
        Vector3 marbleLocalPos = transform.InverseTransformPoint(marble.position);
        
        // We only care about moving the pivot on the flat X/Z plane
        Vector3 targetPivotPos = new Vector3(marbleLocalPos.x, dynamicPivot.localPosition.y, marbleLocalPos.z);

        // Calculate how much the pivot is moving this frame
        Vector3 delta = targetPivotPos - dynamicPivot.localPosition;

        // Move the pivot forward to the ball
        dynamicPivot.localPosition = targetPivotPos;

        // Counter-move the maze geometry backward by the exact same amount.
        // This keeps the maze visually perfectly still while shifting the underlying pivot!
        mazeGeometry.localPosition -= delta;
    }
}
