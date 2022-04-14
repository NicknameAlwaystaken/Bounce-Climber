using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public int platformsOnStart, platformRouteStartAmount;
    public float falloffHeight, gravity;
    public PlatformRouteSpawner platformRouteSpawner;
    private Vector3 currentPlayerPosition, startingPosition;
    //private Spawner spawner;
    //private List<Spawner> spawnerList;
    public PlayerSpawner playerSpawner;
    private GameObject player;
    public GameObject platform;

    public static GameController instance;
    private Camera mainCamera;

    private bool newRoute = true;
    private bool newRoute1 = true;
    private bool newRoute2 = true;


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
        StartGame();
    }
    private void StartGame()
    {
        startingPosition = new Vector3();
        currentPlayerPosition = startingPosition;
        SpawnRoutes(platform.name);
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {
            if(player.transform.position.y < mainCamera.transform.position.y - falloffHeight)
            {
                Debug.Log("Player died.");
                Debug.Log(mainCamera.transform.position.y);
                Destroy(player);
            }
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
}
