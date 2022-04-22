using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRouteSpawner : MonoBehaviour
{
    private int currentRouteAmount, routeLength, currentGameMode;
    private float startingPointOffsetY, newRouteMaxDistance, platformDespawnDistance, maxDistance, minDistance, maxHeight, minHeight;
    private float spawnTimer,
        platformRangeX,
        platformRangeY,
        platformRangeIncrement,
        platformSpeed,
        platformSpawnIntervalMin,
        platformSpawnIntervalMax;
    private bool setupDone = false;
    private string completePath,
        objectName,
        prefabsPath;
    private Vector3 newRoutePosition, platformSpawnPoint;

    private List<GameObject> platformRoute;
    private List<List<GameObject>> platformRouteList;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void GameSettings(GameModeManager newSettings)
    {
        platformRouteList = new List<List<GameObject>>();
        objectName = newSettings.objectName;
        currentRouteAmount = newSettings.platformRouteAmount;
        routeLength = newSettings.platformsOnStart;
        newRoutePosition = newSettings.platformStartPoint;
        SetPlatformRanges(newSettings.platformRangeX, newSettings.platformRangeY);

        currentGameMode = newSettings.gamemodeID;

        platformRangeIncrement = newSettings.platformRangeIncrement;
        platformSpeed = newSettings.platformSpeed;
        platformSpawnPoint = newRoutePosition;
        platformSpawnIntervalMin = newSettings.platformSpawnIntervalMin;
        platformSpawnIntervalMax = newSettings.platformSpawnIntervalMax;
        spawnTimer = Random.Range(platformSpawnIntervalMin, platformSpawnIntervalMax);


        startingPointOffsetY = newSettings.startingPointOffsetY;
        newRouteMaxDistance = newSettings.newRouteMaxDistance;
        platformDespawnDistance = newSettings.platformDespawnDistance;
        maxDistance = newSettings.maxDistance;
        minDistance = newSettings.minDistance;
        maxHeight = newSettings.maxHeight;
        minHeight = newSettings.minHeight;

        prefabsPath = newSettings.prefabsPath;

        setupDone = true;
    }
    public void SetPlatformRanges(float newPlatformRangeX, float newPlatformRangeY)
    {
        platformRangeX = newPlatformRangeX;
        platformRangeY = newPlatformRangeY;
    }
    public void SpawnRoutes()
    {
        for(int i = 0; i < currentRouteAmount; i++)
        {
            SpawnRoute();
        }
    }
    public void SpawnRoute()
    {
        platformRoute = new List<GameObject>();
        float minX = newRoutePosition.x - newRouteMaxDistance;
        float maxX = newRoutePosition.x + newRouteMaxDistance;
        newRoutePosition.x = Random.Range(minX, maxX);
        Vector3 startingPoint = new Vector3(newRoutePosition.x, newRoutePosition.y - startingPointOffsetY);
        newRoutePosition = startingPoint;

        platformRouteList.Add( new List<GameObject>());
        AddPlatformToRouteList(platformRoute, routeLength, true);
    }

    private Vector3 GetNextSpawnLocation(List<GameObject> fromRoute)
    {
        Vector3 newSpawnPosition = new Vector3();
        Vector3 lastSpawnPosition;
        if (fromRoute.Count > 0)
        {
            lastSpawnPosition = GetHighestPlatform(fromRoute).transform.position;
        }
        else
        {
            lastSpawnPosition = newRoutePosition;
        }

        float minPointX = lastSpawnPosition.x - platformRangeX / 2;
        float maxPointX = lastSpawnPosition.x + platformRangeX / 2;
        float minPointY = lastSpawnPosition.y + platformRangeY / 0.6f;
        float maxPointY = lastSpawnPosition.y + platformRangeY;

        newSpawnPosition.x = Random.Range(minPointX, maxPointX);
        newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        return newSpawnPosition;
    }
    private Vector3 SetNextSpawnLocation(Vector3 newSpawnPoint)
    {
        Vector3 newSpawnPosition = new Vector3();
        float minPointX = newSpawnPoint.x - platformRangeX / 2;
        float maxPointX = newSpawnPoint.x + platformRangeX / 2;
        float minPointY = newSpawnPoint.y + platformRangeY / 0.6f;
        float maxPointY = newSpawnPoint.y + platformRangeY;

        newSpawnPosition.x = Random.Range(minPointX, maxPointX);
        newSpawnPosition.y = Random.Range(minPointY, maxPointY);

        return newSpawnPosition;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(setupDone)
        {
            if (currentGameMode == 2)
            {
                spawnTimer -= Time.deltaTime;
                foreach (List<GameObject> route in platformRouteList.ToArray())
                {
                    var itemRoute = route;
                    if (spawnTimer <= 0)
                    {
                        AddPlatformToRouteList(itemRoute);
                        spawnTimer = Random.Range(platformSpawnIntervalMin, platformSpawnIntervalMax);
                    }
                    foreach(GameObject item in itemRoute)
                    {
                        var platform = item;
                        if(platform != null)
                        {
                            platform.transform.position += Vector3.down * platformSpeed * Time.deltaTime;
                            if(platform.transform.position.y <= 0)
                            {
                                Destroy(platform);
                            }
                        }
                    }
                }
            }
            else
            {

                foreach (List<GameObject> route in platformRouteList.ToArray())
                {
                    var itemRoute = route;
                    GameObject lowestPlatform = GetLowestPlatform(itemRoute);
                    if (lowestPlatform != null)
                    {
                        Vector3 lowestPlatformPosition = lowestPlatform.transform.position;
                        if (lowestPlatformPosition.y < mainCamera.transform.position.y - platformDespawnDistance)
                        {
                            AddPlatformToRouteList(itemRoute);
                            Destroy(lowestPlatform);
                            break;
                        }
                    }
                }
            }
        }
    }
    public GameObject GetLowestPlatform(List<GameObject> fromRoute)
    {
        GameObject lowestPlatform = GetComponent<GameObject>();
        foreach (GameObject obj in fromRoute)
        {
            if (obj == null)
            {
                continue;
            }
            if (lowestPlatform == null)
            {
                lowestPlatform = obj;
            }
            if (obj.transform.position.y < lowestPlatform.transform.position.y)
            {
                lowestPlatform = obj;
            }
        }
        return lowestPlatform;
    }
    public GameObject GetHighestPlatform(List<GameObject> fromRoute)
    {
        GameObject highestPlatform = GetComponent<GameObject>();
        foreach (GameObject obj in fromRoute)
        {
            if (obj == null)
            {
                continue;
            }
            if (highestPlatform == null)
            {
                highestPlatform = obj;
            }
            if (obj.transform.position.y > highestPlatform.transform.position.y)
            {
                highestPlatform = obj;
            }
        }
        return highestPlatform;
    }
    public void AddPlatformToRouteList(List<GameObject> route, int platformAmount = 1, bool newRoute = false)
    {
        // clean up, function doing multiple things
        for(int i = 0; i < platformAmount; i++)
        {
            Vector3 location;
            if(currentGameMode == 2)
            {
                location = SetNextSpawnLocation(platformSpawnPoint);
            }
            else
            {
                location = GetNextSpawnLocation(route);
            }
            GameObject newPlatform = CreatePlatform(objectName, location);
            IncrementPlatformRange();
            route.Add(newPlatform);
        }
        // if making multiple platforms at once, it assumes it is a route.
        if(newRoute)
        {
            platformRouteList.Add(route);
        }
    }
    public GameObject CreatePlatform(string objectName, Vector3 location)
    {
        completePath = prefabsPath + objectName;
        GameObject newplatform = Resources.Load(completePath) as GameObject;
        GameObject platform = Instantiate(newplatform, location, new Quaternion());
        return platform;
    }
    public void DestroyPlatform(GameObject platformToDestroy)
    {
        Destroy(platformToDestroy);
    }
    private void IncrementPlatformRange()
    {
        platformRangeX *= platformRangeIncrement;
        platformRangeY *= platformRangeIncrement;
    }
}
