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

    private void Start()
    {
        controlPoint = GameObject.Find("Control Point");
        Camera = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.GetAxis("Mouse X") * turnSpeed;
            float deltaY = Input.GetAxis("Mouse Y") * turnSpeed;
            transform.Rotate(Vector3.up, deltaX * speed * Time.deltaTime);
            Camera.transform.RotateAround(transform.position, -transform.right, deltaY);
        }
        //controlPoint.transform.Rotate(Vector3.left, deltaY * speed * Time.deltaTime);
        
        //if ((Camera.transform.rotation.y < maxRotation || deltaY > 0) && (Camera.transform.rotation.y > MinRotation || deltaY < 0))
        transform.position = player.transform.position;
    }
}
