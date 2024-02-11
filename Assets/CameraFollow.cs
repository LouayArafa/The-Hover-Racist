using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Reference to the hover car's transform
    public Vector3 offset = new Vector3(0f, 5f, -10f); // Offset from the car's position
    public float smoothPositionSpeed = 0.5f; // Smoothing factor for camera position

    void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning("Camera target not set!");
            return;
        }

        // Calculate the desired position of the camera, including the offset
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly move the camera towards the desired position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothPositionSpeed);
        transform.position = smoothedPosition;

        // Make the camera look in the direction the car is heading
        transform.LookAt(target.position + target.forward * 10f); // Adjust the multiplier as needed
    }
}
