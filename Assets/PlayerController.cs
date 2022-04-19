using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private bool canJump = true;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private float speed = 5;
    [SerializeField] private int jumpStage = 0;
    private Vector3 gravityDirection = new Vector3(0, -1, 0);
    public float gravityMultiplier = 0.1f;
    public float jumpMultiplier = 10;
    [SerializeField] private int[] stagePower;
    // Start is called before the first frame update
    void Start()
    {
        stagePower = new int[6];
        stagePower[0] = 10;
        stagePower[1] = 20;
        stagePower[2] = 40;
        stagePower[3] = 80;
        stagePower[4] = 160;
        stagePower[5] = 320;
        Physics.gravity = new Vector3(0, -10.0F, 0);
        playerRb = GetComponent<Rigidbody>();
        //playerRb.useGravity = false;
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        //playerRb.AddForce(gravityDirection * 10 * gravityMultiplier);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && canJump == true)
        {
            playerRb.AddForce(-gravityDirection * stagePower[jumpStage] * 100 / gravityMultiplier);
            canJump = false;
        }
        playerRb.MovePosition(playerRb.position + focalPoint.transform.right * horizontalInput * speed * Time.deltaTime);
        playerRb.MovePosition(playerRb.position + focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (canJump == false) canJump = true;
        }
    }
}