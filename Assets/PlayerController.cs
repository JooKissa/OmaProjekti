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
    private GameObject lastTouchedWall;
    [SerializeField] private ParticleSystem pSystem;
    private bool jumped = false;
    private float speed = 5;
    private float m2Speed = 10;
    private float jumpPower = 15;
    private float m2JumpPower = 25;
    private int maxAirJumps = 1;
    private int airJumps;
    private int maxSpeed = 10;
    private float bounce = 1f;
    private float lastSpeed = 0;
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
        playerRb.AddForce(Vector3.down * 1500 * Time.deltaTime, ForceMode.Acceleration);
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump") && onGround == false && jumped && airJumps > 0)
        {
            Vector3 vel = playerRb.velocity;
            if (verticalInput != 0 && horizontalInput != 0)
            {
                playerRb.AddForce((focalPoint.transform.forward * verticalInput + focalPoint.transform.up) / 4 * jumpPower, ForceMode.VelocityChange);
                playerRb.AddForce((focalPoint.transform.right * horizontalInput + focalPoint.transform.up) / 4 * jumpPower, ForceMode.VelocityChange);
            }
            else if (verticalInput != 0 || horizontalInput != 0)
            {
                playerRb.AddForce((focalPoint.transform.forward * verticalInput + focalPoint.transform.up) / 2 * jumpPower, ForceMode.VelocityChange);
                playerRb.AddForce((focalPoint.transform.right * horizontalInput + focalPoint.transform.up) / 2 * jumpPower, ForceMode.VelocityChange);
            }
            else
            {
                playerRb.velocity = new Vector3(vel.x, vel.y + jumpPower, vel.z);
            }
            //playerRb.AddForce((focalPoint.transform.forward + focalPoint.transform.up) / 2 * 20, ForceMode.VelocityChange);
            airJumps--;
        }
        if (Input.GetButton("Jump") && (onGround == true || !jumped))
        {
            Vector3 vel = playerRb.velocity;
            if (ballMode == 1) playerRb.velocity = new Vector3(vel.x, vel.y + jumpPower, vel.z);
            if (ballMode == 2) playerRb.velocity = new Vector3(vel.x, vel.y + m2JumpPower, vel.z);
            onGround = false;
            jumped = true;
        }

        if (Input.GetButton("Shift"))
        {
            GetComponent<Collider>().material.bounciness = 0;
        }
        else
        {
            GetComponent<Collider>().material.bounciness = bounce;
        }

        if (ballMode == 1)
        {
            playerRb.AddForce(focalPoint.transform.right * horizontalInput * speed * 5 * Time.deltaTime, ForceMode.VelocityChange);
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * 5 * Time.deltaTime, ForceMode.VelocityChange);
        }
        else if (ballMode == 2)
        {
            playerRb.velocity = new Vector3(0, playerRb.velocity.y, 0);
            playerRb.MovePosition(playerRb.position + focalPoint.transform.right * horizontalInput * m2Speed * Time.deltaTime);
            playerRb.MovePosition(playerRb.position + focalPoint.transform.forward * verticalInput * m2Speed * Time.deltaTime);
        }

        if (playerRb.velocity.magnitude > maxSpeed) playerRb.AddForce(-playerRb.velocity / playerRb.velocity.magnitude * playerRb.velocity.magnitude * Time.deltaTime, ForceMode.VelocityChange);
    }

    private void LateUpdate()
    {
        lastSpeed = playerRb.velocity.magnitude;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            if (onGround == true) onGround = false;
            airJumps = maxAirJumps;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (lastSpeed > 20)
        {
            var emitParams = new ParticleSystem.EmitParams();
            pSystem.Emit(emitParams, 10);
        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            lastTouchedWall = collision.gameObject;
            if (onGround == false) onGround = true;
            if (jumped) jumped = false;
        }
        if (collision.gameObject.CompareTag("Walljump") && collision.gameObject != lastTouchedWall)
        {
            lastTouchedWall = collision.gameObject;
            if (onGround == false) onGround = true;
            if (jumped) jumped = false;
            airJumps = maxAirJumps;
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