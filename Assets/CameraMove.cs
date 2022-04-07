using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speed = 200;
    [SerializeField] private float turnSpeed = 3.0f;
    public GameObject player;
    private GameObject controlPoint;
    private GameObject Camera;
    public float polarDeltaY;
    public float maxRotation = 90f;
    public float MinRotation = -90f;
    public Vector3 cameraUpDirection = Vector3.up;
    public Vector3 yes;
    private GameObject gravShow;

    private void Start()
    {
        controlPoint = GameObject.Find("Control Point");
        Camera = GameObject.Find("Main Camera");
        gravShow = GameObject.Find("GravityRepresentation");
    }
    private void Update()
    {
        float deltaX = Input.GetAxis("Mouse X") * turnSpeed;
        float deltaY = Input.GetAxis("Mouse Y") * turnSpeed;
        transform.Rotate(Vector3.up, deltaX * speed * Time.deltaTime);
        //controlPoint.transform.Rotate(Vector3.left, deltaY * speed * Time.deltaTime);
        Camera.transform.RotateAround(transform.position, -transform.right, deltaY);
        //if ((Camera.transform.rotation.y < maxRotation || deltaY > 0) && (Camera.transform.rotation.y > MinRotation || deltaY < 0))
        transform.position = player.transform.position;
    }

    private void UpdateGravityDirection(Vector3 gravityDir)
    {
        cameraUpDirection = -gravityDir;
        Vector3 gravityAlongLocalZAxis = transform.forward * Vector3.Dot(gravityDir, transform.forward);
        Vector3 gravityInLocalXYPlane = gravityDir - gravityAlongLocalZAxis;
        Vector3 old = transform.forward;
        transform.LookAt(transform.position + Vector3.forward, -gravityInLocalXYPlane);
        gravShow.transform.LookAt(gravityDir);
        yes = transform.forward * Vector3.Dot(transform.forward, old);

    }
}
