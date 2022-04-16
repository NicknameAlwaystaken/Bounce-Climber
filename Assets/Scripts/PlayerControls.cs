using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float bounceVelocity = 30f,
        returnVelocity = 30f,
        returnHeight = 75f,
        maxMovementSpeed = 15f,
        gravityUpChange = 0.8f,
        gravityDownChange = 1.2f,
        maxDropSpeed = 75f;
    private int gameMode;

    private bool bounce, playerStarted, playerDiving, playerReturned;
    private Vector3 originalGravity, originalPlayerPosition;


    private Rigidbody rb;

    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //originalGravity = Physics.gravity;
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
        if (gameMode == 2)
        {
            if (inputVertical < 0)
            {
                if (!playerStarted)
                {
                    Physics.gravity = originalGravity;
                    transform.GetComponent<Collider>().enabled = true;
                    playerStarted = true;
                }
                if (!playerDiving)
                {
                    Vector3 movement = Vector3.down * maxDropSpeed;
                    rb.velocity = new Vector3(rb.velocity.x, movement.y);
                    playerDiving = true;
                }
            }
            if (playerStarted)
            {
                // applying velocity upwards, if allowed
                if (bounce)
                {
                    audioSource.Play();
                    playerDiving = false;
                    playerReturned = false;
                    bounce = false;
                }
                if (!playerDiving && !playerReturned && transform.position.y < returnHeight)
                {
                    Vector3 upVelocity = Vector3.up * Mathf.Max(returnVelocity * (1 - (Mathf.Max(transform.position.y, 1f)) / returnHeight), 20f);
                    rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
                }
                if (transform.position.y >= returnHeight && !playerReturned)
                {
                    Vector3 upVelocity = Vector3.up * 15f;
                    rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
                    playerReturned = true;
                }
            }
        }
        else
        {
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!bounce)
        {
            GameObject collidedObject = collision.gameObject;
            if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
            {
                bounce = true;
                if (gameMode == 2)
                {
                    if (playerDiving || playerReturned)
                    {
                        GameController.instance.DestroyPlatform(collidedObject);
                        bounce = true;
                    }
                }
            }
        }
    }
    private bool HasPlayerReturned(float height)
    {
        if(transform.position.y >= height)
        {
            playerReturned = true;
        }
        else
        {
            playerReturned = false;
        }
        return playerReturned;
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
    public void SetMaxMovementSpeed(float newVelocity)
    {
        maxMovementSpeed = newVelocity;
    }
    public void SetReturnVelocity(float newVelocity)
    {
        returnVelocity = newVelocity;
    }
    public void SetGameMode(int newGameMode)
    {
        gameMode = newGameMode;
        if(gameMode == 2)
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
            originalGravity = new Vector3(0f, -40f, 0f);
            originalPlayerPosition = transform.position;
            transform.GetComponent<Collider>().enabled = false;
            playerStarted = false;
        }
    }
}
