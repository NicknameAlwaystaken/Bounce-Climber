using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    public ParticleSystem particles;
    public string currentStateName;

    private float bounceVelocity,
        returnVelocity,
        returnHeight,
        maxMovementSpeed,
        gravityUpChange,
        gravityDownChange,
        maxDropSpeed,
        diveCooldownCounter,
        diveCooldown;
    public bool movingAllowed;
    private bool moving;
    public bool jumpingAllowed;
    private bool jumping;
    private bool jumpingDone;


    private Rigidbody rb;

    public bool MovingAllowed { get => movingAllowed; set => movingAllowed = value; }
    public bool JumpingAllowed { get => jumpingAllowed; set => jumpingAllowed = value; }
    public bool Jumping { get => jumping; set => jumping = value; }
    public float BounceVelocity { get => bounceVelocity; set => bounceVelocity = value; }
    public float ReturnVelocity { get => returnVelocity; set => returnVelocity = value; }
    public float ReturnHeight { get => returnHeight; set => returnHeight = value; }
    public float MaxMovementSpeed { get => maxMovementSpeed; set => maxMovementSpeed = value; }
    public float GravityUpChange { get => gravityUpChange; set => gravityUpChange = value; }
    public float GravityDownChange { get => gravityDownChange; set => gravityDownChange = value; }
    public float MaxDropSpeed { get => maxDropSpeed; set => maxDropSpeed = value; }
    public float DiveCooldownCounter { get => diveCooldownCounter; set => diveCooldownCounter = value; }
    public float DiveCooldown { get => diveCooldown; set => diveCooldown = value; }
    public AudioSource AudioSource { get => audioSource; set => audioSource = value; }
    public bool Moving { get => moving; set => moving = value; }
    public bool JumpingDone { get => jumpingDone; set => jumpingDone = value; }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        /*
        movingAllowed = false;
        moving = false;
        jumpingAllowed = false;
        jumping = false;
        */
    }

    void Update()
    {
        if(movingAllowed)
        {
            float inputHorizontal = Input.GetAxis("Horizontal");
            float inputVertical = Input.GetAxis("Vertical");

            if (inputHorizontal != 0)
            {
                Moving = true;
                Vector3 movement = inputHorizontal * MaxMovementSpeed * transform.right;
                rb.velocity = new Vector3(movement.x, rb.velocity.y);
            }
            else
            {
                Moving = false;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
        {
            if (JumpingAllowed)
            {
                Jumping = true;
            }
        }
    }
    public string GetPlayerState()
    {
        return currentStateName;
    }
    public void SetPlayerState(string playerState)
    {
        currentStateName = playerState;
    }
}