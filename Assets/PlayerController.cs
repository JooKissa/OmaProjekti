using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private GameObject gameCamera;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float speed = 10;
    [SerializeField] private int jumpStage = 0;
    private Vector3 gravityDirection = new Vector3(0, -1, 0);
    public float gravityMultiplier = 0.1f;
    public float jumpMultiplier = 1000;
    [SerializeField] private int[] stagePower;
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
        playerRb.AddForce(gravityDirection * 10f * gravityMultiplier);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump")) playerRb.AddForce(-gravityDirection * jumpMultiplier / gravityMultiplier);
        playerRb.MovePosition(playerRb.position + focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);
        playerRb.MovePosition(playerRb.position + focalPoint.transform.up * verticalInput * speed * Time.deltaTime);
        //playerRb.AddForce(focalPoint.transform.up * verticalInput * speed * Time.deltaTime);
        //playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);
        if (Input.GetButton("Control")) playerRb.AddForce(gravityDirection / gravityMultiplier * 100);
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

    private void upgradeStage(int stage)
    {
        if (stage > jumpStage)
        {
            jumpStage = stage;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.6f);
    }
    public Vector3 hitPoss;

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 hitPos;
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.6f, transform.forward, out hit, 10))
        {
            hitPos = hit.point;
            hitPoss = hit.point;
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            //playerRb.constraints = RigidbodyConstraints.FreezeAll;
            Vector3 avg = new Vector3(0, 0, 0);
            int i = 0;
            foreach (ContactPoint contact in collision.contacts)
            {
                avg += contact.point;
                i++;
            }
            avg /= i;
            Vector3 gravityDir = avg - transform.position;
            focalPoint.SendMessage("UpdateGravityDirection", gravityDir);
            gravityDirection = gravityDir;
            //playerRb.constraints = RigidbodyConstraints.None;
        }
    }
    */
}