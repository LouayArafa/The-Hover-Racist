using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float moveDistance = 1f; // Set the distance the object should move up and down
    public float moveSpeed = 1f;    // Set the speed of the vertical movement
    public float rotateSpeed = 30f; // Set the speed of rotation

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        // Vertical movement
        float newY = initialPosition.y + Mathf.Sin(Time.time * moveSpeed) * moveDistance;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);

        // Rotation
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }
}
