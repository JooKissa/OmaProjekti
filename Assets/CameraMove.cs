using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private float speed = 200;
    [SerializeField] private float turnSpeedX = 3.0f;
    [SerializeField] private float turnSpeedY = 3.0f;
    public GameObject player;
    private GameObject Camera;
    public float polarDeltaY;
    public float maxRotation = 90f;
    public float MinRotation = -90f;

    private void Start()
    {
        Camera = GameObject.Find("Main Camera");
    }
    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float deltaX = Input.GetAxis("Mouse X") * turnSpeedX;
            float deltaY = Input.GetAxis("Mouse Y") * turnSpeedY;
            transform.Rotate(Vector3.up, deltaX * speed * Time.deltaTime);
            Camera.transform.RotateAround(transform.position, -transform.right, deltaY);
        }
        transform.position = player.transform.position;
    }
}
