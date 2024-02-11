using System.Collections;
using UnityEngine;

public class hover : MonoBehaviour
{
    Rigidbody rb;
    public LayerMask groundLayer;
    public float HoverMultiplier = 2.4f;
    public float maxHoverForce = 10f;
    public float maxHoverDistance = 5f;

    public float baseGravity = 9.8f; // Adjust this value as needed
    public float maxGravity = 20f; // Adjust this value as needed

    public float acceleration = 10f;
    public float deceleration = 5f; // Added deceleration factor
    public float maxSpeed = 20f;
    public float backwardSpeed = 5f;
    public float turnSpeed = 5f;
    public float torque = 200f;

    [SerializeField] private float currentSpeed = 0f;
    [SerializeField] private float horizontalInput;
    [SerializeField] private float verticalInput;
    [SerializeField] private float currentGravity = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, 0f, 0); // Adjust the center of mass for better stability
    }

    public Transform[] anchors = new Transform[4];
    public RaycastHit[] hits = new RaycastHit[5];
    public float hoverForceDamping = 0.5f; // Adjust this value for damping at the edges
    public float pushPullDamping = 0.5f;
    public float fallingDamping = 0.5f; // Adjust this value for falling damping

    void ApplyF(Transform anchor, RaycastHit hit)
    {
        Vector3 raycastDirection = -anchor.up;

        // Draw the ray for visualization
        Debug.DrawRay(anchor.position, raycastDirection * maxHoverDistance, Color.blue);

        if (Physics.Raycast(anchor.position, raycastDirection, out hit, maxHoverDistance, groundLayer))
        {
            if (hit.collider != null)
            {
                float force = Mathf.Abs(1 / (hit.point.y - anchor.position.y));
                float cappedForce = Mathf.Clamp(force * HoverMultiplier, 0f, maxHoverForce);

                // Apply damped force
                float dampedForce = Mathf.Lerp(rb.GetRelativePointVelocity(anchor.position).y, cappedForce, hoverForceDamping);
                rb.AddForceAtPosition(transform.up * dampedForce, anchor.position, ForceMode.Acceleration);

                // Reset custom gravity when the raycast hits the ground
                currentGravity = baseGravity;

                // Implement the push-pull system with damping
                float distance = Vector3.Distance(anchor.position, hit.point);
                Vector3 pushPullDirection = (anchor.position - hit.point).normalized;
                float pushPullForce = Mathf.Clamp((maxHoverDistance - distance) * 10f, -5f, 5f);
                float dampedPushPullForce = Mathf.Lerp(rb.GetRelativePointVelocity(anchor.position).y, pushPullForce, pushPullDamping);
                rb.AddForceAtPosition(pushPullDirection * dampedPushPullForce, anchor.position, ForceMode.Acceleration);
            }
        }
        else
        {
            // Apply custom gravity if the raycast doesn't hit the ground
            if (rb.useGravity)
            {
                // Gradually increase custom gravity
                currentGravity = Mathf.Min(currentGravity + Time.fixedDeltaTime, maxGravity);
                rb.AddForce(Vector3.down * currentGravity, ForceMode.Acceleration);

                // Damping for falling too fast
                float verticalVelocity = Mathf.Max(0f, -rb.velocity.y); // Get positive vertical velocity
                float dampingForce = Mathf.Lerp(0f, verticalVelocity * fallingDamping, Mathf.Clamp01(verticalVelocity ));
                rb.AddForce(Vector3.up * dampingForce, ForceMode.Acceleration);
            }
        }
    }

    void Update()
    {
        // Get player input
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        HandleHover();
        HandleMovement();
    }

    void HandleHover()
    {
        for (int i = 0; i < anchors.Length; i++)
            ApplyF(anchors[i], hits[i]);
    }

    void HandleMovement()
    {
        // Calculate movement
        MoveCar();
        TurnCar();
    }

    void MoveCar()
    {
        // Calculate acceleration
        currentSpeed += verticalInput * acceleration * Time.fixedDeltaTime;

        // Apply deceleration when there is no input
        if (Mathf.Approximately(verticalInput, 0f))
        {
            Decelerate();
        }

        // Limit overall speed
        currentSpeed = Mathf.Clamp(currentSpeed, -backwardSpeed, maxSpeed);

        // Calculate movement in the forward direction
        Vector3 moveDirection = transform.forward * currentSpeed;

        // Apply force to move the car
        rb.AddForce(moveDirection, ForceMode.Acceleration);
    }

    void TurnCar()
    {
        // Calculate rotation
        float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;

        // Create rotation quaternion
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

        // Apply rotation using Rigidbody.MoveRotation
        rb.MoveRotation(rb.rotation * turnRotation);

        // Apply torque for additional rotation realism
        rb.AddTorque(Vector3.up * horizontalInput * torque * Time.fixedDeltaTime);
    }

    void Decelerate()
    {
        // Apply deceleration to gradually slow down the car
        float decelerationAmount = deceleration * Time.fixedDeltaTime;
        currentSpeed = Mathf.MoveTowards(currentSpeed, 0f, decelerationAmount);
    }
}
