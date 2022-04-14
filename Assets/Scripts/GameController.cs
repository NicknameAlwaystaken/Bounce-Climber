using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int platformsOnStart, platformRouteStartAmount;
    public float falloffHeight, gravity,
        cameraFollowHeight, cameraFollowHorizontal, cameraPositioningTime, cameraStartAccelerationSpeed, cameraAccumulatingSpeed;
    private float t;
    public PlatformRouteSpawner platformRouteSpawner;
    private Vector3 currentPlayerPosition, startingPosition;
    //private Spawner spawner;
    //private List<Spawner> spawnerList;
    public PlayerSpawner playerSpawner;
    private GameObject player;
    public GameObject platform;

    public static GameController instance;
    private Camera mainCamera;
    private CameraType cameraState;

    private bool cameraMoving = false;

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
            /*
            if(getPlayerCurrentPosition().y > 50 && newRoute)
            {
                SpawnRoute(platform.name);
                newRoute = false;
            }
            if (getPlayerCurrentPosition().y > 100 && newRoute1)
            {
                SpawnRoute(platform.name);
                newRoute1 = false;
            }
            if (getPlayerCurrentPosition().y > 150 && newRoute2)
            {
                SpawnRoute(platform.name);
                newRoute2 = false;
            }
            */
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
                if (playerPosition.y > cameraPosition.y - cameraFollowHeight)
                {
                    mainCamera.transform.position = new Vector3(cameraPosition.x, playerPosition.y + cameraFollowHeight, cameraPosition.z);
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
                Debug.Log("Camera is set to Accelerating");
                float distanceFromPlayerHorizontal;
                float newCameraXPosition = cameraPosition.x;
                float newCameraYPosition = Time.deltaTime * (cameraStartAccelerationSpeed += cameraAccumulatingSpeed);

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
                desiredPosition = new Vector3(newCameraXPosition, cameraPosition.y + newCameraYPosition, cameraPosition.z);
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
