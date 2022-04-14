using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float bounceVelocity, maxMovementSpeed, movementAcceleration,
        cameraFollowHeight, cameraFollowHorizontal, cameraPositioningTime;

    public string velocity = "";

    private float ballMaxHeight, t;
    private bool bounce;


    private Camera mainCamera;
    private Rigidbody rb;
    private GameObject lastBouncedOn;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = FindObjectOfType<Camera>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        float distanceFromBall;

        // Camera follow y axis
        if (transform.position.y > mainCamera.transform.position.y - cameraFollowHeight)
        {
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, transform.position.y + cameraFollowHeight, mainCamera.transform.position.z);
        }

        distanceFromBall = Mathf.Abs(transform.position.x - mainCamera.transform.position.x);

        // Camera follow x axis
        if (distanceFromBall > cameraFollowHorizontal)
        {
            t = cameraPositioningTime;
            t -= Time.deltaTime;
            if(t > 0)
            {
                Vector3 desiredPosition = new Vector3(transform.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
                if (transform.position.x > mainCamera.transform.position.x)
                {
                    desiredPosition.x -= cameraFollowHorizontal;
                }
                else
                {
                    desiredPosition.x += cameraFollowHorizontal;

                }

                Vector3 smootherPosition = Vector3.Lerp(mainCamera.transform.position, desiredPosition, t);
                mainCamera.transform.position = smootherPosition;
            }
         }
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
