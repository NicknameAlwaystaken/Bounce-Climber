using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float bounceVelocity = 30,
        maxMovementSpeed = 15,
        gravityUpChange = 0.8f,
        gravityDownChange = 1.2f;

    private bool bounce;
    private Vector3 originalGravity;


    private Rigidbody rb;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        originalGravity = Physics.gravity;
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        // going left or right
        if (inputHorizontal != 0)
        {
            Vector3 movement = transform.right * inputHorizontal * maxMovementSpeed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y);
        }
        // pressing up or down
        if (inputVertical != 0)
        {
            if (inputVertical < 0)
            {
                Physics.gravity = new Vector3(0f, originalGravity.y * gravityDownChange, 0f);
            }
            else
            {
                Physics.gravity = new Vector3(0f, originalGravity.y * gravityUpChange, 0f);
            }
        }
        else
        {
            Physics.gravity = originalGravity;
        }
        // applying velocity upwards, if allowed
        if (bounce)
        {
            audioSource.Play();
            bounce = false;
            Vector3 upVelocity = Vector3.up * bounceVelocity;
            Debug.Log("Bounce Velocity: " + bounceVelocity);
            rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!bounce)
        {
            if(collision.gameObject.CompareTag("Platform") || collision.gameObject.CompareTag("Ground"))
            {
                bounce = true;
            }
        }
    }
    public void SetGravityChange(float newGravityUp, float newGravityDown)
    {
        gravityUpChange = newGravityUp;
        gravityDownChange = newGravityDown;
    }
    public void SetBounceVelocity(float newVelocity)
    {
        bounceVelocity = newVelocity;
    }
    public void SetMaxVelocity(float newVelocity)
    {
        maxMovementSpeed = newVelocity;
    }
}
