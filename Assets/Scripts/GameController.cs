using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int platformsOnStart, platformRouteStartAmount;
    public float falloffHeight = 40f,
        gravity = -10f,
        cameraFollowHorizontal = 15f,
        cameraPositioningTime = 0.04f,
        cameraAccelerationSpeed = 200f,
        cameraAccumulatingSpeed = 0.05f,
        cameraOffsetZ = -60f,
        cameraOffsetY = 10f,
        cameraSpeedOffsetRatioY = 100f,
        platformSpeedRatioX = 100f,
        platformSpeedRatioY = 100f,
        startingBounceVelocity = 30,
        bounceSpeedRatio = 100;
    private float t;
    private PlayerControls playerControls;
    public PlatformRouteSpawner platformRouteSpawner;
    public PlayerSpawner playerSpawner;
    private GameObject player;
    public GameObject platform;

    public static GameController instance;
    private Camera mainCamera;
    private CameraType cameraState;

    enum CameraType
    {
        None = 0,
        Following = 1,
        Accelerating = 2
    }
    enum GameMode
    {
        None = 0,
        No_Breaks = 1
    }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
        mainCamera = FindObjectOfType<Camera>();
        player = playerSpawner.SpawnPlayer();
        playerControls = player.GetComponent<PlayerControls>();
        int cameraMode = 1;
        StartGame(cameraMode);
    }
    private void StartGame(int cameraMode = 1)
    {
        SetCameraMode(2);
        SetGameModeSettings(platform.name, (int)GameMode.No_Breaks);
        SpawnRoutes(platform.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y < mainCamera.transform.position.y - falloffHeight)
            {
                Debug.Log("Player died.");
                Debug.Log(mainCamera.transform.position.y);
                Destroy(player);
            }
        }
    }

    private void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 playerPosition = player.transform.position;
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 desiredPosition;
            t = cameraPositioningTime;
            t -= Time.deltaTime;

            if (cameraState == CameraType.Following)
            {
                Debug.Log("Camera is set to following");
                float distanceFromBallHorizontal;

                // Camera follow y axis
                if (playerPosition.y > cameraPosition.y - cameraOffsetY)
                {
                    mainCamera.transform.position = new Vector3(cameraPosition.x, playerPosition.y + cameraOffsetY, cameraPosition.z);
                }

                distanceFromBallHorizontal = Mathf.Abs(playerPosition.x - cameraPosition.x);

                // Camera follow x axis
                if (distanceFromBallHorizontal > cameraFollowHorizontal)
                {
                    if (t > 0)
                    {
                        desiredPosition = new Vector3(playerPosition.x, cameraPosition.y, cameraPosition.z);
                        if (playerPosition.x > cameraPosition.x)
                        {
                            desiredPosition.x -= cameraFollowHorizontal;
                        }
                        else
                        {
                            desiredPosition.x += cameraFollowHorizontal;

                        }

                        Vector3 smootherPosition = Vector3.Lerp(cameraPosition, desiredPosition, t);
                        mainCamera.transform.position = smootherPosition;
                    }
                }
            }
            if (cameraState == CameraType.Accelerating)
            {
                playerControls.SetBounceVelocity(startingBounceVelocity + cameraAccelerationSpeed / bounceSpeedRatio);
                float distanceFromPlayerHorizontal;
                float newCameraXPosition = cameraPosition.x;
                float newCameraYPosition = cameraPosition.y + Time.deltaTime * (cameraAccelerationSpeed += cameraAccumulatingSpeed);
                float newCameraZPosition = cameraOffsetZ - cameraAccelerationSpeed / cameraSpeedOffsetRatioY;

                distanceFromPlayerHorizontal = Mathf.Abs(playerPosition.x - cameraPosition.x);

                // Camera follow x axis
                if (distanceFromPlayerHorizontal > cameraFollowHorizontal)
                {
                    if (playerPosition.x < cameraPosition.x)
                    {
                        distanceFromPlayerHorizontal *= -1;
                    }
                    newCameraXPosition = cameraPosition.x + distanceFromPlayerHorizontal;
                }
                desiredPosition = new Vector3(newCameraXPosition, newCameraYPosition, newCameraZPosition);
                Vector3 smootherPosition = Vector3.Lerp(cameraPosition, desiredPosition, t);
                mainCamera.transform.position = smootherPosition;
            }
            /*
            if (cameraMoving && desiredPosition == cameraPosition)
            {
                cameraMoving = false;
            }
            */
        }
    }

    private void SpawnRoutes(string objectName)
    {
        platformRouteSpawner.SpawnRoutes();
    }
    private void SpawnRoute(string objectName)
    {
        platformRouteSpawner.SpawnRoute();
    }
    private void SetCameraMode(int mode)
    {
        cameraState = (CameraType)mode;
    }
    private void SetSettingsForRoutes(float newPlatformRangeX, float newPlatformRangeY)
    {
        platformRouteSpawner.SetPlatformRanges(newPlatformRangeX, newPlatformRangeY);
    }
    private void SetGameModeSettings(string objectName, int gameMode)
    {
        if(gameMode == (int)GameMode.No_Breaks)
        {
            float platformRangeX = 20f, 
                platformRangeY = 10f,
                platformRangeIncrement = 1.01f,
                bounceVelocity = 30,
                maxMovementSpeed = 25,
                gravityUpChange = 0.6f,
                gravityDownChange = 1.4f;

            playerControls.SetGravityChange(gravityUpChange, gravityDownChange);
            playerControls.SetBounceVelocity(bounceVelocity);
            playerControls.SetMaxVelocity(maxMovementSpeed);

            platformRouteSpawner.Setup(objectName, player.transform.position,
                platformRangeX, platformRangeY, platformsOnStart,
                platformRouteStartAmount);
            platformRouteSpawner.SpecialSetup(platformRangeIncrement);
        }
    }
}
