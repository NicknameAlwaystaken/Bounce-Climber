using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [HideInInspector] public PlayerInput playerInput;
    private BounceClimber playerInputActions;


    #region Variables

    private AudioSource audioSource;
    public ParticleSystem particles;
    public Rigidbody rb;
    public string currentStateName;

    private Vector2 inputVector;

    private float bounceVelocity,
        tempBounceVelocity,
        returnVelocity,
        returnHeight,
        maxMovementSpeed,
        movementSpeed,
        slowdownSpeed,
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
    private Vector3 dashDesiredPosition;
    private Vector3 dashSmootherPosition;
    private float dashTimer;

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
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float SlowdownSpeed { get => slowdownSpeed; set => slowdownSpeed = value; }

    #endregion


    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        playerInputActions = new BounceClimber();
        playerInputActions.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (Dashing)
        {
            dashTimer += Time.deltaTime;

            dashSmootherPosition.x = Vector3.Lerp(transform.position, dashDesiredPosition, dashTimer / dashingHorizontalTimer).x;
            dashSmootherPosition.y = Vector3.Lerp(transform.position, dashDesiredPosition, dashTimer / dashingVerticalTimer).y;

            transform.position = dashSmootherPosition;
            if (Mathf.Abs(transform.position.x - dashDesiredPosition.x) < DashFreeingDistance)
            {
                StopDash();
            }
            return;
        }
        //if (Bounce())
        if(!BouncingDone && Landed && Bouncing)
        {
            JumpWithoutEffects();
            BouncingDone = true;
        }
        if (MovingAllowed)
        {
            Vector3 movement = inputVector.x * MovementSpeed * transform.right;

            if (inputVector.x == 0)
            {
                if (rb.velocity.x > 0)
                {
                    rb.AddForce(transform.right * -1 * SlowdownSpeed);
                }
                else
                {
                    rb.AddForce(transform.right * SlowdownSpeed);
                }
            }

            if (rb.velocity.x < MaxMovementSpeed && movement.x > 0)
                rb.AddForce(movement);

            else if (rb.velocity.x > -MaxMovementSpeed && movement.x < 0)
                rb.AddForce(movement);
        }
    }
    private void StartDash()
    {
        Dashing = true;
        DashingDone = true;
        rb.velocity = Vector3.zero;
        transform.GetComponent<Collider>().enabled = false;
        float distance = DashingDistance;
        if (inputVector.x < 0) distance *= -1;
        dashDesiredPosition = new Vector3
        {
            x = transform.position.x + distance,
            y = transform.position.y + DashingLift,
            z = transform.position.z,
        };
        dashTimer = 0f;
    }
    private void StopDash()
    {
        Dashing = false;
        DashingConditions = false;
        transform.GetComponent<Collider>().enabled = true;
        rb.velocity = Vector3.zero;
    }

    private void Update()
    {
        inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (DoubleJumpingConditions && !DoubleJumpingDone)
            {
                if (Dashing)
                    StopDash();
                JumpWithEffects(BounceVelocity * DoubleJumpIncrement);
                DoubleJumpingDone = true;
            }
            if (NormalJump())
            {
                if (Dashing)
                    StopDash();
                Landed = false;
                JumpWithEffects(BounceVelocity * FirstJumpIncrement);
                Jumping = false;
                JumpingDone = true;
                return;
            }
        }
        if (context.canceled && JumpingDone)
        {
            if (DoubleJumpingAllowed && JumpingDone)
            {
                DoubleJumpingConditions = true;
            }
        }
    }

    private bool Bounce()
    {
        return Bouncing && !BouncingDone && BouncingAllowed && Landed;
    }

    public void BounceToggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("madafakin bounce" + Bouncing);
            Bouncing = !Bouncing;
        }
    }
    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed)
            if (DashingAllowed && !DashingDone && inputVector.x != 0)
                StartDash();
    }
    private bool NormalJump()
    {
        return Landed && JumpingAllowed;
    }

    private void JumpWithEffects(float bounceVelocity)
    {
        AudioSource.Play();
        Instantiate(particles, transform.position, new Quaternion());
        Vector3 upVelocity = Vector3.up * bounceVelocity;
        rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        Jumping = false;
    }
    private void JumpWithoutEffects()
    {
        Vector3 upVelocity = Vector3.up * BounceVelocity * AutoJumpBounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (collidedObject.CompareTag("Platform") || collidedObject.CompareTag("Ground"))
        {
            Landed = true;
            DashingDone = false;
            DoubleJumpingDone = false;
            DoubleJumpingConditions = false;
            BouncingDone = false;
            DashingConditions = true;

            if (collidedObject.CompareTag("Platform"))
            {
                LastContact = collidedObject;
                HasContact = true;
            }
        }
    }
}
