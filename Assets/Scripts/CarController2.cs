using UnityEngine;

public class CarController2 : MonoBehaviour
{
    public WheelCollider frontLeft;
    public WheelCollider frontRight;
    public WheelCollider rearLeft;
    public WheelCollider rearRight;

    public Transform frontLeftMesh;
    public Transform frontRightMesh;
    public Transform rearLeftMesh;
    public Transform rearRightMesh;

    public float motorPower = 1500f;
    public float steerAngle = 30f;
    public float brakeForce = 3000f;

    float steerInput;
    float motorInput;
    bool brake;

    void Update()
    {
        steerInput = Input.GetAxis("Horizontal");
        motorInput = Input.GetAxis("Vertical");
        brake = Input.GetKey(KeyCode.Space);
    }

    void FixedUpdate()
    {
        rearLeft.motorTorque = motorInput * motorPower;
        rearRight.motorTorque = motorInput * motorPower;

        float steer = steerAngle * steerInput;
        frontLeft.steerAngle = steer;
        frontRight.steerAngle = steer;

        float brakeTorque = brake ? brakeForce : 0f;
        frontLeft.brakeTorque = brakeTorque;
        frontRight.brakeTorque = brakeTorque;
        rearLeft.brakeTorque = brakeTorque;
        rearRight.brakeTorque = brakeTorque;

        UpdateWheel(frontLeft, frontLeftMesh);
        UpdateWheel(frontRight, frontRightMesh);
        UpdateWheel(rearLeft, rearLeftMesh);
        UpdateWheel(rearRight, rearRightMesh);
    }

    void UpdateWheel(WheelCollider col, Transform mesh)
    {
        Vector3 pos;
        Quaternion rot;
        col.GetWorldPose(out pos, out rot);
        mesh.position = pos;
        mesh.rotation = rot;
    }
}
