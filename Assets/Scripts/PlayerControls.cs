using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float bounceVelocity, maxMovementSpeed, movementAcceleration;

    public string velocity = "";

    private bool bounce;


    private Rigidbody rb;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");

        // going left or right
        if (inputHorizontal != 0)
        {
            Vector3 movement = transform.right * inputHorizontal * maxMovementSpeed;
            rb.velocity = new Vector3(movement.x, rb.velocity.y);
        }
        // applying velocity upwards, if allowed
        if(bounce)
        {
            audioSource.Play();
            bounce = false;
            Vector3 upVelocity = Vector3.up * bounceVelocity;
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
}
