using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int platformsOnStart, platformRouteStartAmount;
    public float falloffHeight = 40f,
        gravity = -40f,
        cameraFollowHorizontal = 15f,
        cameraPositioningTime = 0.04f,
        cameraAccelerationSpeed = 200f,
        cameraAccumulatingSpeed = 0.05f,
        cameraOffsetZ = -60f,
        cameraOffsetY = 10f,
        cameraSpeedToDistanceRatioZ = 5f,
        startingBounceVelocity = 30f,
        bounceSpeedRatio = 35f;
    private float t;
    private PlayerControls playerControls;
    public PlatformRouteSpawner platformRouteSpawner;
    private GameModeManager gameModeManager;
    public PlayerSpawner playerSpawner;
    private GameObject player;
    public GameObject platform;

    public static GameController instance;
    private Camera mainCamera;
    private CameraType cameraState;

    enum CameraType
    {
        Static = 0,
        Following = 1,
        Accelerating = 2
    }
    enum GameMode
    {
        None = 0,
        No_Breaks = 1,
        Platform_Smasher = 2

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
        int cameraMode = 1;
        StartGame(cameraMode);
        gameModeManager = new GameModeManager();
        gameModeManager.OutputJSON();
    }
    private void StartGame(int cameraMode = 1)
    {
        SetGameModeSettings(platform.name, (int)GameMode.Platform_Smasher);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y < mainCamera.transform.position.y - falloffHeight)
            {
                //Destroy(player);
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
                cameraAccelerationSpeed += cameraAccumulatingSpeed;
                float distanceFromPlayerHorizontal;
                float newCameraXPosition = cameraPosition.x;
                float newCameraYPosition = cameraPosition.y + Time.deltaTime * cameraAccelerationSpeed;
                float newCameraZPosition = cameraOffsetZ - cameraAccelerationSpeed / cameraSpeedToDistanceRatioZ;

                distanceFromPlayerHorizontal = Mathf.Abs(playerPosition.x - cameraPosition.x);

                // Camera follow x axis
                if (distanceFromPlayerHorizontal > cameraFollowHorizontal)
                {
                    //check direction player is from the camera
                    if (playerPosition.x < cameraPosition.x)
                    {
                        distanceFromPlayerHorizontal *= -1; //swap distance to negative if the player is in -X axis of camera position
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

    private void SpawnRoutes()
    {
        platformRouteSpawner.SpawnRoutes();
    }
    private void SpawnRoute(string objectName)
    {
        platformRouteSpawner.SpawnRoute();
    }
    public void DestroyPlatform(GameObject platformToDestroy)
    {
        platformRouteSpawner.DestroyPlatform(platformToDestroy);
    }
    private void SetCameraMode(int mode)
    {
        cameraState = (CameraType)mode;
        if(cameraState == CameraType.Static)
        {
            float newCameraPosX = 0f;
            float newCameraPosY = 50f;
            float newCameraPosZ = -50f;
            mainCamera.transform.position = new Vector3(newCameraPosX, newCameraPosY, newCameraPosZ);
        }
    }
    private void SetSettingsForRoutes(float newPlatformRangeX, float newPlatformRangeY)
    {
        platformRouteSpawner.SetPlatformRanges(newPlatformRangeX, newPlatformRangeY);
    }
    private void SetGameModeSettings(string objectName, int gameMode)
    {
        if (gameMode == (int)GameMode.No_Breaks)
        {
            float platformRangeX = 80f,
                platformRangeY = 10f,
                platformRangeIncrement = 1.01f,
                bounceVelocity = 30,
                maxMovementSpeed = 25,
                gravityUpChange = 0.6f,
                gravityDownChange = 2.0f;

            player = playerSpawner.SpawnPlayer();
            playerControls = player.GetComponent<PlayerControls>();
            SetCameraMode(2);

            playerControls.SetGravityChange(gravityUpChange, gravityDownChange);
            playerControls.SetBounceVelocity(bounceVelocity);
            playerControls.SetMaxMovementSpeed(maxMovementSpeed);

            platformRouteSpawner.Setup(objectName, player.transform.position,
                platformRangeX, platformRangeY, platformsOnStart,
                platformRouteStartAmount);
            platformRouteSpawner.SpecialSetup(gameMode, platformRangeIncrement);
            SpawnRoutes();
        }

        if (gameMode == (int)GameMode.Platform_Smasher)
        {
            float platformRangeX = 90f,
                platformRangeY = 0f,
                platformRangeIncrement = 1.0f,
                returnVelocity = 70f,
                maxMovementSpeed = 75f,
                gravityUpChange = 0.6f,
                gravityDownChange = 2.0f,
                platformSpeed = 15f,
                platformSpawnY = 85f,
                platformSpawnIntervalMin = 0.7f,
                platformSpawnIntervalMax = 1.7f;

            platformsOnStart = 1;
            SetCameraMode(0);
            player = playerSpawner.SpawnPlayer(new Vector3(0f, 60f, 0f));
            playerControls = player.GetComponent<PlayerControls>();
            playerControls.SetGravityChange(gravityUpChange, gravityDownChange);
            playerControls.SetReturnVelocity(returnVelocity);
            playerControls.SetMaxMovementSpeed(maxMovementSpeed);

            platformRouteSpawner.Setup("Platform_ThickAndWide", player.transform.position,
                platformRangeX, platformRangeY, platformsOnStart,
                platformRouteStartAmount);
            platformRouteSpawner.SpecialSetup(gameMode, platformSpeed, platformSpawnY, platformRangeIncrement, platformSpawnIntervalMin, platformSpawnIntervalMax);
            playerControls.SetGameMode(gameMode);
            SpawnRoutes();
        }
    }
}
