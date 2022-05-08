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
        firstJumpIncrement,
        doubleJumpIncrement,
        horizontalInput,
        verticalInput;
    public bool movingAllowed;
    public bool jumpingAllowed;
    public bool bouncingAllowed;
    public bool doubleJumpingAllowed;
    public bool landed;

    private bool moving;
    private bool movingLeft;
    private bool movingRight;
    private bool movingUp;
    private bool movingDown;

    private bool jumping;
    private bool jumpingDone;

    private bool bouncing;
    private bool bouncingDone;

    private bool doubleJumping;
    private bool doubleJumpingDone;
    private bool doubleJumpingConditions;

    private bool inputVerticalZero;

    private float autoJumpBounceVelocity;


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
    public float FirstJumpIncrement { get => firstJumpIncrement; set => firstJumpIncrement = value; }
    public float TempBounceVelocity { get => tempBounceVelocity; set => tempBounceVelocity = value; }
    public float DoubleJumpIncrement { get => doubleJumpIncrement; set => doubleJumpIncrement = value; }
    public bool BouncingAllowed { get => bouncingAllowed; set => bouncingAllowed = value; }
    public bool Bouncing { get => bouncing; set => bouncing = value; }
    public bool BouncingDone { get => bouncingDone; set => bouncingDone = value; }
    public bool DoubleJumpingAllowed { get => doubleJumpingAllowed; set => doubleJumpingAllowed = value; }
    public bool DoubleJumping { get => doubleJumping; set => doubleJumping = value; }
    public bool DoubleJumpingDone { get => doubleJumpingDone; set => doubleJumpingDone = value; }
    public float AutoJumpBounceVelocity { get => autoJumpBounceVelocity; set => autoJumpBounceVelocity = value; }
    public bool InputVerticalZero { get => inputVerticalZero; set => inputVerticalZero = value; }
    public bool DoubleJumpingConditions { get => doubleJumpingConditions; set => doubleJumpingConditions = value; }
    public bool Landed { get => landed; set => landed = value; }

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(movingAllowed)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            Moving = false;
            MovingUp = false;
            MovingDown = false;
            MovingLeft = false;
            MovingRight = false;
            MovingUp = verticalInput > 0;
            MovingDown = verticalInput < 0;
            MovingRight = horizontalInput > 0;
            MovingLeft = horizontalInput < 0;
            InputVerticalZero = verticalInput == 0;
            Moving = MovingUp || MovingDown || MovingLeft || MovingRight;

            if (DoubleJumpingConditions && MovingUp && Input.GetKeyUp(KeyCode.W))
            {
                DoubleJumping = true;
            }
            if (Input.GetKeyUp(KeyCode.W) && DoubleJumpingAllowed && JumpingDone)
            {
                DoubleJumpingConditions = true;
            }
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
        {
            Landed = true;
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