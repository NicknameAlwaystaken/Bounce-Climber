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
        cameraStartAccelerationSpeed = 200f,
        cameraAccumulatingSpeed = 0.05f,
        cameraOffsetZ = -60f,
        cameraOffsetY = 10f,
        cameraSpeedToOffsetDistanceY = 100f;
    private float t;
    public PlatformRouteSpawner platformRouteSpawner;
    private Vector3 currentPlayerPosition, startingPosition;
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

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        player = playerSpawner.SpawnPlayer();
        Physics.gravity = new Vector3(0, gravity, 0);
        StartGame(2);
    }
    private void StartGame(int cameraMode = 1)
    {
        SetCameraMode(cameraMode);
        startingPosition = new Vector3();
        currentPlayerPosition = startingPosition;
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
                float distanceFromPlayerHorizontal;
                float newCameraXPosition = cameraPosition.x;
                float newCameraYPosition = cameraPosition.y + Time.deltaTime * (cameraStartAccelerationSpeed += cameraAccumulatingSpeed);
                float newCameraZPosition = cameraOffsetZ - cameraStartAccelerationSpeed / cameraSpeedToOffsetDistanceY;

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
        platformRouteSpawner.SetupRoutes(objectName, platformsOnStart, platformRouteStartAmount, currentPlayerPosition);
        Debug.Log("Routes setup in GameController");
    }
    private void SpawnRoute(string objectName)
    {
        platformRouteSpawner.SpawnRoute(objectName, platformsOnStart, getPlayerCurrentPosition());
    }
    private Vector3 getPlayerCurrentPosition()
    {
        return currentPlayerPosition = player.transform.position;
    }
    private void SetCameraMode(int mode)
    {
        cameraState = (CameraType)mode;
    }
}
