using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    private AudioSource audioSource;
    public ParticleSystem particles;
    public string currentStateName;

    private float bounceVelocity,
        tempBounceVelocity,
        returnVelocity,
        returnHeight,
        maxMovementSpeed,
        gravityUpChange,
        gravityDownChange,
        maxDropSpeed,
        diveCooldownCounter,
        diveCooldown,
        superJumpIncrement,
        lowJumpIncrement,
        horizontalInput,
        verticalInput;
    public bool movingAllowed;
    private bool moving;
    private bool movingLeft;
    private bool movingRight;
    private bool movingUp;
    private bool movingDown;
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
    public bool JumpingDone { get => jumpingDone; set => jumpingDone = value; }
    public bool Moving { get => moving; set => moving = value; }
    public bool MovingLeft { get => movingLeft; set => movingLeft = value; }
    public bool MovingRight { get => movingRight; set => movingRight = value; }
    public float HorizontalInput { get => horizontalInput; set => horizontalInput = value; }
    public float VerticalInput { get => verticalInput; set => verticalInput = value; }
    public bool MovingUp { get => movingUp; set => movingUp = value; }
    public bool MovingDown { get => movingDown; set => movingDown = value; }
    public float SuperJumpIncrement { get => superJumpIncrement; set => superJumpIncrement = value; }
    public float TempBounceVelocity { get => tempBounceVelocity; set => tempBounceVelocity = value; }
    public float LowJumpIncrement { get => lowJumpIncrement; set => lowJumpIncrement = value; }

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
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Moving = false;
            if (horizontalInput != 0)
            {
                Moving = true;
                if (horizontalInput > 0)
                {
                    MovingLeft = true;
                }
                else
                {
                    MovingLeft = true;
                }
            }
            if (verticalInput != 0)
            {
                Moving = true;
                if (verticalInput > 0)
                {
                    MovingUp = true;
                    tempBounceVelocity = bounceVelocity * superJumpIncrement;
                }
                else
                {
                    MovingDown = true;
                    tempBounceVelocity = bounceVelocity * lowJumpIncrement;
                }
            }
            else
            {
                tempBounceVelocity = bounceVelocity;
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