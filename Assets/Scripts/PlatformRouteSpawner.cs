using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * PlatformRouteSpawner
 * -------------------
 * Set individual settings for each PlatformRoute
 * Keeps track of when route needs to destroy or spawn a platform
 * Calls PlatformRoute to generate platforms to a specific route
 * Gives settings to platformRoute for generating platforms in specific way
 * Calls PlatformRoute to spawn platforms based on player location. Calls platformRoute where next platform spawns, if player is close enough
 * Removes platforms from PlatformRoute if they are too far from the player
 * 
 * GameController withholds data how far platforms are allowed to be
 * GameController calls PlatFormRouteSpawner for lowest platform and highest platform
 * GameController destroys it if it is too far from player due to platformroutespawner being unable to call gameobject.destroy()
 * GameController calls to spawn new Routes to left and right if furthers platform to the left or right is too close.
 * GameController calls to spawn new platforms towards up or down if highest platform is too close to player height
 * 
 */

public class PlatformRouteSpawner
{
    private int currentRouteAmount, routeLength, currentGameMode;
    private float startingPointOffsetY, newRouteMaxDistance, platformDespawnDistance, maxDistance, minDistance, maxHeight, minHeight;
    public float platformRangeX,
        platformRangeY,
        platformRangeIncrement,
        platformSpeed,
        platformSpawnIntervalMin,
        platformSpawnIntervalMax;
    private float spawnTimer;
    private bool setupDone = false;
    public List<string> objectPaths;
    public Vector3 routeStartPosition,
        playerPosition;

    private PlatformRouteSpawner routeSettings;
    private PlatformRoute platformRoute;

    private List<PlatformRoute> platformRouteList;

    public void GenerateRoute(PlatformRouteSpawner newSettings)
    {
        platformRoute = new PlatformRoute(newSettings);
        platformRouteList.Add(platformRoute);
    }
    public void SetSpawnerSettings(PlatformRouteSpawner newSettings)
    {
        routeSettings = newSettings;
    }

    public void GameSettings(GameModeManager newSettings)
    {
        platformRouteList = new List<PlatformRoute>();
        objectPaths = newSettings.objectPaths;
        currentRouteAmount = newSettings.platformRouteAmount;
        routeLength = newSettings.platformsOnStart;
        routeStartPosition = newSettings.platformStartPoint;

        currentGameMode = newSettings.gamemodeID;

        spawnTimer = Random.Range(platformSpawnIntervalMin, platformSpawnIntervalMax);


        startingPointOffsetY = newSettings.startingPointOffsetY;
        newRouteMaxDistance = newSettings.newRouteMaxDistance;
        platformDespawnDistance = newSettings.platformDespawnDistance;
        maxDistance = newSettings.maxDistance;
        minDistance = newSettings.minDistance;
        maxHeight = newSettings.maxHeight;
        minHeight = newSettings.minHeight;

        setupDone = true;
    }
    public void SpawnRoutes()
    {
        for(int i = 0; i < currentRouteAmount; i++)
        {
            GenerateRoute(routeSettings);
        }
    }
    public void SpawnRoute()
    {
        GenerateRoute(routeSettings);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(setupDone)
        {
            if (currentGameMode == 2)
            {
                /*
                spawnTimer -= Time.deltaTime;
                foreach (PlatformRouteSpawner route in platformRouteList.ToArray())
                {
                    var itemRoute = route;
                    if (spawnTimer <= 0)
                    {
                        itemRoute.platformRoute.SpawnPlatformStaticLocation();
                        spawnTimer = Random.Range(platformSpawnIntervalMin, platformSpawnIntervalMax);
                    }
                    itemRoute.
                    platform.transform.position += Vector3.down * platformSpeed * Time.deltaTime;
                        if(platform.transform.position.y <= 0)
                        {
                            Destroy(platform);
                        }
                    }
                }
                */
            }
            else
            {
                foreach (PlatformRoute route in platformRouteList.ToArray())
                {
                    var itemRoute = route;
                    GameObject lowestPlatform = itemRoute.GetLowestPlatform();
                    if (lowestPlatform != null)
                    {
                        Vector3 lowestPlatformPosition = lowestPlatform.transform.position;
                        if (lowestPlatformPosition.y < playerPosition.y - platformDespawnDistance)
                        {
                            route.SpawnPlatform();
                            break;
                        }
                    }
                }
            }
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
        platformToDestroy
    }
    private void IncrementPlatformRange()
    {
        platformRangeX *= platformRangeIncrement;
        platformRangeY *= platformRangeIncrement;
    }
    public void SetPlayerPosition(Vector3 newPosition)
    {
        playerPosition = newPosition;
    }
}
