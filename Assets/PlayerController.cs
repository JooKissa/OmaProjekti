using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private GameObject gameCamera;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float speed = 500;
    private Vector3 gravityDirection = new Vector3(0, -1, 0);
    public float gravityMultiplier = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, -10.0F, 0);
        playerRb = GetComponent<Rigidbody>();
        playerRb.useGravity = false;
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        playerRb.AddForce(gravityDirection * 9.81f * gravityMultiplier, ForceMode.Acceleration);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) playerRb.AddForce(-gravityDirection * 1000 / gravityMultiplier);
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);
    }

    void LateUpdate()
    {
        //float delta = Input.GetAxis("Mouse X") * turnSpeed;
        //gameCamera.transform.RotateAround(transform.position, Vector3.up, delta);
        if (Input.GetMouseButton(1))
        {
            //float delta = Input.GetAxis("Mouse X") * turnSpeed;
            //gameCamera.transform.RotateAround(transform.position, Vector3.up, delta);
        }
        //transform.rotation.z = 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Vector3 avg = new Vector3(0, 0, 0);
            int i = 0;
            foreach (ContactPoint contact in collision.contacts)
            {
                avg += contact.point;
                i++;
            }
            avg /= i;
            gravityDirection = avg - transform.position;
            focalPoint.SendMessage("UpdateCameraRotation", gravityDirection);
        }
    }
}
