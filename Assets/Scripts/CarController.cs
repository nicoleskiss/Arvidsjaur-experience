using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public float accel = 12f;
    public float maxSpeed = 25f;
    public float turnSpeed = 120f;
    public float brake = 20f;

    Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");    // W / S
        float h = Input.GetAxisRaw("Horizontal");  // A / D

        Vector3 vel = rb.linearVelocity;
        Vector3 forward = transform.forward;

        float forwardSpeed = Vector3.Dot(vel, forward);

        float targetAccel =
            Input.GetKey(KeyCode.Space)
            ? -Mathf.Sign(forwardSpeed) * brake
            : v * accel;

        forwardSpeed = Mathf.Clamp(
            forwardSpeed + targetAccel * Time.fixedDeltaTime,
            -maxSpeed * 0.5f,
            maxSpeed
        );

        Vector3 lateral = vel - forward * Vector3.Dot(vel, forward);
        lateral *= 0.85f;

        rb.linearVelocity = forward * forwardSpeed + lateral;

        float speedFactor = Mathf.Clamp01(Mathf.Abs(forwardSpeed) / 2f);
        float turn = h * turnSpeed * speedFactor * Time.fixedDeltaTime;

        rb.MoveRotation(
            rb.rotation * Quaternion.Euler(0f, turn, 0f)
        );
    }
}
