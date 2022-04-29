using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    private float bounceVelocity,
        returnVelocity,
        returnHeight,
        maxMovementSpeed,
        gravityUpChange,
        gravityDownChange,
        maxDropSpeed,
        diveCooldownCounter,
        diveCooldown;
    private int gameMode;
    private PlayerState playerState;

    private bool bounce, playerStarted, playerDiving, playerReturned;
    private Vector3 originalGravity, originalPlayerPosition;


    private Rigidbody rb;

    private AudioSource audioSource;
    public ParticleSystem particles;

    enum PlayerState
    {
        None = 0,
        Started = 1,
        Returned = 2,
        Diving = 3
    }

    public void SetPlayerSettings(GameModeManager playerSettings)
    {
        gravityUpChange = playerSettings.gravityUpChange;
        gravityDownChange = playerSettings.gravityDownChange;
        bounceVelocity = playerSettings.startingBounceVelocity;
        maxMovementSpeed = playerSettings.maxMovementSpeed;
        returnVelocity = playerSettings.returnVelocity;
        returnHeight = playerSettings.returnHeight;
        maxDropSpeed = playerSettings.maxDropSpeed;
        diveCooldownCounter = playerSettings.diveCooldownCounter;
        diveCooldown = playerSettings.diveCooldown;
        SetGameMode(playerSettings);
    }

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
        diveCooldownCounter -= Time.deltaTime;

        // going left or right
        if (inputHorizontal != 0)
        {
            Vector3 movement = inputHorizontal * maxMovementSpeed * transform.right;
            rb.velocity = new Vector3(movement.x, rb.velocity.y);
        }
        if (gameMode == 1)
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
                Instantiate(particles, transform.position, new Quaternion());
                bounce = false;
                Vector3 upVelocity = Vector3.up * bounceVelocity;
                rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
            }
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
                if (!playerDiving && diveCooldownCounter <= 0)
                {
                    Vector3 movement = Vector3.down * maxDropSpeed;
                    rb.velocity = new Vector3(rb.velocity.x, movement.y);
                    diveCooldownCounter = diveCooldown;
                    playerDiving = true;
                }
            }
            if (playerStarted)
            {
                if (bounce)
                {
                    audioSource.Play();
                    Instantiate(particles, transform.position, new Quaternion());
                    playerDiving = false;
                    playerReturned = false;
                    bounce = false;
                }
                // applying velocity upwards, if allowed
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(!bounce)
        {
            GameObject collidedObject = collision.gameObject;
            if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
            {
                if(collidedObject.CompareTag("Platform"))
                {
                    GameController.instance.DestroyBreakable(collidedObject);
                    if (gameMode == 2)
                    {
                        if (playerDiving || playerReturned)
                        {
                            GameController.instance.DestroyPlatform(collidedObject);
                        }
                    }
                }
                bounce = true;
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
    public void SetGameMode(GameModeManager newSettings)
    {
        originalPlayerPosition = transform.position;
        originalGravity = newSettings.gravity;
        gameMode = newSettings.gamemodeID;
        if(gameMode == 2)
        {
            Physics.gravity = new Vector3(0f, 0f, 0f);
            transform.GetComponent<Collider>().enabled = false;
            playerStarted = false;
        }
    }
}
