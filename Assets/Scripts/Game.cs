using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public float falloffHeight,
        cameraFollowHorizontal,
        cameraPositioningTime,
        cameraAccelerationSpeed,
        cameraAccumulatingSpeed,
        cameraOffsetZ,
        cameraOffsetY,
        cameraSpeedToDistanceRatioZ;
    private float spawnTimer;
    private float platformSpawnIntervalMin;
    private float platformSpawnIntervalMax;
    private PlatformRouteSpawner platformRouteSpawner;
    private GameModeManager gamemodeManager;

    public static GameController instance;
    [SerializeField] private Camera mainCamera;
    public GameMode gameModes;
    public CameraType cameraState;
    private float platformSpawnYDistance = 50f;
    private float cameraTimer;
    private GameObject Player;

    public enum CameraType
    {
        Static = 0,
        Following = 1,
        Accelerating = 2
    }
    public enum GameMode
    {
        Freeplay = 0,
        No_Breaks = 1,
        Platform_Smasher = 2
    }

    private void Awake()
    {
        mainCamera = FindObjectOfType<Camera>();
        platformRouteSpawner = new PlatformRouteSpawner();
        gamemodeManager = new GameModeManager();
        gamemodeManager.CheckIfFileValid();
        gamemodeManager = gamemodeManager.LoadGamemodeSettings((int)gameModes);
        SetGameModeSettings();
    }

    void Start()
    {
    }

    public void StartGame()
    {
        SetGameModeSettings();
        platformRouteSpawner.SpawnRoutes();
    }
    private void SetGameModeSettings()
    {
        GameSetup();
        CameraSettings();
        platformRouteSpawner.GameSettings(gamemodeManager);
    }

    private void GameSetup()
    {
        Physics.gravity = gamemodeManager.gravity;
        falloffHeight = gamemodeManager.falloffHeight;
        platformSpawnIntervalMin = gamemodeManager.platformSpawnIntervalMin;
        platformSpawnIntervalMax = gamemodeManager.platformSpawnIntervalMax;
        spawnTimer = Random.Range(platformSpawnIntervalMin, platformSpawnIntervalMax);
    }

    private void CameraSettings()
    {
        cameraFollowHorizontal = gamemodeManager.cameraFollowHorizontal;
        cameraPositioningTime = gamemodeManager.cameraPositioningTime;
        cameraAccelerationSpeed = gamemodeManager.cameraAccelerationSpeed;
        cameraAccumulatingSpeed = gamemodeManager.cameraAccumulatingSpeed;
        cameraOffsetZ = gamemodeManager.cameraOffsetZ;
        cameraOffsetY = gamemodeManager.cameraOffsetY;
        cameraSpeedToDistanceRatioZ = gamemodeManager.cameraSpeedToDistanceRatioZ;
        SetCamera(gamemodeManager.cameraStartLocation);
    }
    private void SetCamera(Vector3 newCameraPosition)
    {
        mainCamera.transform.position = newCameraPosition;
    }
    private void Update()
    {
        if(mainCamera != null && Player != null && IsGamePlaying())
        {
            if (Player.transform.position.y < mainCamera.transform.position.y - falloffHeight)
            {
                GameController.instance.PlayerDead(Player);
                return;
            }
            if (gamemodeManager.gamemodeID == (int)gameModes)
            {
                if (platformRouteSpawner.GetRouteAmount() > 0)
                {
                    GameObject platform = platformRouteSpawner.GetLowestRoutePlatform();
                    if (platform != null)
                    {
                        float verticalDistance = Mathf.Abs(platform.transform.position.y - mainCamera.transform.position.y);
                        if (verticalDistance < platformSpawnYDistance)
                        {
                            platformRouteSpawner.CallRouteToSpawn(platform);
                        }
                    }
                    platform = platformRouteSpawner.GetLowestPlatformInRoutes();
                    if (platform != null)
                    {
                        float verticalDistance = Mathf.Abs(platform.transform.position.y - mainCamera.transform.position.y);
                        if (verticalDistance > platformSpawnYDistance)
                        {
                            platform.GetComponent<Platform>().DestroyPlatform();
                        }
                    }
                }

            }
        }
    }
    private void FixedUpdate()
    {
        if (Player != null && IsGamePlaying())
        {
            Vector3 playerPosition = Player.transform.position;
            Vector3 cameraPosition = mainCamera.transform.position;
            Vector3 desiredPosition;
            cameraTimer = cameraPositioningTime;
            cameraTimer -= Time.deltaTime;

            if (gamemodeManager.cameraMode == (int)CameraType.Following)
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
                    if (cameraTimer > 0)
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

                        Vector3 smootherPosition = Vector3.Lerp(cameraPosition, desiredPosition, cameraTimer);
                        mainCamera.transform.position = smootherPosition;
                    }
                }
            }
            if (gamemodeManager.cameraMode == (int)CameraType.Accelerating)
            {
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
                Vector3 smootherPosition = Vector3.Lerp(cameraPosition, desiredPosition, cameraTimer);
                mainCamera.transform.position = smootherPosition;
            }
        }
        else
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    private static bool IsGamePlaying()
    {
        return GameController.instance.GetGameStatus() == GameController.GameStatus.Started ||
            GameController.instance.GetGameStatus() == GameController.GameStatus.Resumed;
    }
}
