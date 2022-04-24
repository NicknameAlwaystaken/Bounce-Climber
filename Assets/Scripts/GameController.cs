using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * GameController calls GameModeManager to check if there is file to load --DONE--
 * GameController calls GameModeManager to give gamesettings from one gamemode on start as object --DONE--
 * GameController intializes PlayerController, PlatformRouteSpawner and PlayerSpawner with loaded settings --DONE--
 * */

public class GameController : MonoBehaviour
{
    private float falloffHeight,
        cameraFollowHorizontal,
        cameraPositioningTime,
        cameraAccelerationSpeed,
        cameraAccumulatingSpeed,
        cameraOffsetZ,
        cameraOffsetY,
        cameraSpeedToDistanceRatioZ,
        startingBounceVelocity,
        bounceSpeedRatio;
    private float t;
    private PlayerControls playerControls;
    private PlatformRouteSpawner platformRouteSpawner;
    private GameModeManager gamemodeSettings;
    private GameModeManager gamemodeManager;
    public PlayerSpawner playerSpawner;
    private GameObject player;
    public GameObject platform;

    public static GameController instance;
    private Camera mainCamera;
    private CameraType cameraState;
    private bool setupDone;

    enum CameraType
    {
        Static = 0,
        Following = 1,
        Accelerating = 2
    }
    enum GameMode
    {
        Freeplay = 0,
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
        mainCamera = FindObjectOfType<Camera>();
        platformRouteSpawner = new PlatformRouteSpawner();
        gamemodeManager = new GameModeManager();
        gamemodeManager.CheckIfFileValid();
        gamemodeSettings = gamemodeManager.LoadGamemodeSettings((int)GameMode.Platform_Smasher);
        StartGame();
    }
    private void StartGame()
    {
        SetGameModeSettings();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if (player.transform.position.y < mainCamera.transform.position.y - falloffHeight)
            {
                Debug.Log("Attempting to destroy player");
                //Destroy(player);
            }
        }
    }

    private void FixedUpdate()
    {
        if(setupDone)
        {
            if (player != null)
            {
                Vector3 playerPosition = player.transform.position;
                Vector3 cameraPosition = mainCamera.transform.position;
                Vector3 desiredPosition;
                t = cameraPositioningTime;
                t -= Time.deltaTime;

                if (gamemodeSettings.cameraMode == (int)CameraType.Following)
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
                if (gamemodeSettings.cameraMode == (int)CameraType.Accelerating)
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
            }
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
    private void SetCamera(Vector3 newCameraPosition)
    {
        mainCamera.transform.position = newCameraPosition;
    }
    private void SetSettingsForRoutes(float newPlatformRangeX, float newPlatformRangeY)
    {
        platformRouteSpawner.SetPlatformRanges(newPlatformRangeX, newPlatformRangeY);
    }
    private void SetGameModeSettings()
    {

        GameControllerSetup();
        CameraSettings();
        player = playerSpawner.SpawnPlayer(gamemodeSettings.playerSpawnLocation);
        playerControls = player.GetComponent<PlayerControls>();

        playerControls.SetPlayerSettings(gamemodeSettings);
        platformRouteSpawner.GameSettings(gamemodeSettings);
        platformRouteSpawner.SpawnRoutes();
        setupDone = true;
    }

    private void GameControllerSetup()
    {
        Physics.gravity = gamemodeSettings.gravity;
        falloffHeight = gamemodeSettings.falloffHeight;
        startingBounceVelocity = gamemodeSettings.startingBounceVelocity;
        bounceSpeedRatio = gamemodeSettings.bounceSpeedRatio;
    }

    private void CameraSettings()
    {
        cameraFollowHorizontal = gamemodeSettings.cameraFollowHorizontal;
        cameraPositioningTime = gamemodeSettings.cameraPositioningTime;
        cameraAccelerationSpeed = gamemodeSettings.cameraAccelerationSpeed;
        cameraAccumulatingSpeed = gamemodeSettings.cameraAccumulatingSpeed;
        cameraOffsetZ = gamemodeSettings.cameraOffsetZ;
        cameraOffsetY = gamemodeSettings.cameraOffsetY;
        cameraSpeedToDistanceRatioZ = gamemodeSettings.cameraSpeedToDistanceRatioZ;
        SetCamera(gamemodeSettings.cameraStartLocation);
    }
}
