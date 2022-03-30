using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speed = 200;
    [SerializeField] private float turnSpeed = 3.0f;
    public GameObject player;
    private GameObject Camera;
    public float polarDeltaY;
    public float maxRotation = 90f;
    public float MinRotation = -90f;
    public Vector3 cameraUpDirection = Vector3.up;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        float deltaX = Input.GetAxis("Mouse X") * turnSpeed;
        float deltaY = Input.GetAxis("Mouse Y") * turnSpeed;
        transform.Rotate(cameraUpDirection, deltaX * speed * Time.deltaTime);
        Camera.transform.RotateAround(transform.position, -transform.right, deltaY);
        //if ((Camera.transform.rotation.y < maxRotation || deltaY > 0) && (Camera.transform.rotation.y > MinRotation || deltaY < 0))
        transform.position = player.transform.position;
    }

    private void UpdateCameraRotation(Vector3 gravityDir)
    {
        //var right = Vector3.Cross(gravityDir.normalized, transform.right.normalized);
        //transform.rotation = new Quaternion(right.x, right.y, right.z, transform.rotation.w);
        cameraUpDirection = -gravityDir;
    }
}
