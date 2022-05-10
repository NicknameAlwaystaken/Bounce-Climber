using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{

    #region Variables

    private AudioSource audioSource;
    public ParticleSystem particles;
    public Rigidbody rb;
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
        verticalInput,
        currentScore;

    public float dashingHorizontalTimer;
    public float dashingVerticalTimer;

    [SerializeField] private bool movingAllowed;
    [SerializeField] private bool jumpingAllowed;
    [SerializeField] private bool bouncingAllowed;
    [SerializeField] private bool doubleJumpingAllowed;
    [SerializeField] private bool dashingAllowed;
    [SerializeField] private bool paused;
    [SerializeField] private bool landed;
    [SerializeField] private float dashingDistance;
    [SerializeField] private float dashingLift;
    [SerializeField] private KeyCode bounceToggleKey;
    [SerializeField] private KeyCode dashKey;
    [SerializeField] private GameObject lastContact;

    private bool moving;
    private bool movingLeft;
    private bool movingRight;
    private bool movingUp;
    private bool movingDown;

    private bool jumping;
    private bool jumpingDone;

    private bool bouncing;
    private bool bouncingDone;
    private bool toggleBounce;

    private bool doubleJumping;
    private bool doubleJumpingDone;
    private bool doubleJumpingConditions;

    private bool dashing;
    private bool dashingDone;
    private bool dashingConditions;
    private float dashFreeingDistance;

    private bool inputVerticalZero;

    private float autoJumpBounceVelocity;

    private bool hasContact;

    #endregion


    #region Get and Set
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
    public bool ToggleBounce { get => toggleBounce; set => toggleBounce = value; }
    public bool DashingAllowed { get => dashingAllowed; set => dashingAllowed = value; }
    public bool Dashing { get => dashing; set => dashing = value; }
    public bool DashingDone { get => dashingDone; set => dashingDone = value; }
    public bool DashingConditions { get => dashingConditions; set => dashingConditions = value; }
    public float DashingDistance { get => dashingDistance; set => dashingDistance = value; }
    public float DashingHorizontalTimer { get => dashingHorizontalTimer; set => dashingHorizontalTimer = value; }
    public float DashingLift { get => dashingLift; set => dashingLift = value; }
    public float DashingVerticalTimer { get => dashingVerticalTimer; set => dashingVerticalTimer = value; }
    public float DashFreeingDistance { get => dashFreeingDistance; set => dashFreeingDistance = value; }
    public GameObject LastContact { get => lastContact; set => lastContact = value; }
    public bool HasContact { get => hasContact; set => hasContact = value; }
    public float CurrentScore { get => currentScore; set => currentScore = value; }
    public bool Paused { get => paused; set => paused = value; }

    #endregion

    public void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Paused) return;
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(dashKey) && DashingAllowed && !DashingDone && HorizontalMovement()) // dashing
        {
            DashingConditions = true;
        }
        if (!DoubleJumpingDone && Input.GetKeyDown(KeyCode.W) && Dashing) // Jump to cancel dash
        {
            DoubleJumping = true;
        }
        if (movingAllowed)
        {
            if (Input.GetKeyDown(bounceToggleKey)) ToggleBounce = true;


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

            if (DoubleJumpingConditions && Input.GetKeyDown(KeyCode.W))
            {
                DoubleJumping = true;
            }
            if (Input.GetKeyUp(KeyCode.W) && DoubleJumpingAllowed && JumpingDone)
            {
                DoubleJumpingConditions = true;
            }
        }
    }

    private bool HorizontalMovement()
    {
        return MovingLeft || MovingRight;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
        {
            Landed = true;
            DashingDone = false;
            DoubleJumpingDone = false;
            BouncingDone = false;

            if(collidedObject.CompareTag("Platform"))
            {
                LastContact = collidedObject;
                HasContact = true;
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