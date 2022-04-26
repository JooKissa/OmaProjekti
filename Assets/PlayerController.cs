using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float turnSpeed = 3.0f;
    [SerializeField] private GameObject gameCamera;
    [SerializeField] private bool onGround = true;
    [SerializeField] private int ballMode = 1;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool jumped = false;
    private float speed = 5;
    private float m2Speed = 10;
    private float jumpPower = 3;
    private float m2JumpPower = 15;
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    private void setBallMode(int mode)
    {
        ballMode = mode;
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButton("Jump") && onGround == true)
        {
            Vector3 vel = playerRb.velocity;
            if (ballMode == 1) playerRb.velocity = new Vector3(vel.x, vel.y + jumpPower, vel.z);
            if (ballMode == 2) playerRb.velocity = new Vector3(vel.x, vel.y + m2JumpPower, vel.z);
            onGround = false;
            jumped = true;
        }
        if (ballMode == 1 && onGround)
        {
            playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * 500 * Time.deltaTime);
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * 500 * Time.deltaTime);
        }
        else if (ballMode == 2)
        {
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
            playerRb.MovePosition(playerRb.position + focalPoint.transform.right * horizontalInput * m2Speed * Time.deltaTime);
            playerRb.MovePosition(playerRb.position + focalPoint.transform.forward * verticalInput * m2Speed * Time.deltaTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (onGround == true) onGround = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (onGround == false) onGround = true;
            if (jumped) jumped = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !jumped)
        {
            if (onGround == false) onGround = true;
        }
    }
}