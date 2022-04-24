using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
 * PlatformRoute child of PlatformRouteSpawner
 * -------------------
 * A specific route to handle platforms in
 * Have information of how many are in route spawned
 * Have information of platforms spawned for this route
 * Will have it's own individual platform settings like spawning different types of platforms based on chance
 * Is called from PlatformRouteSpawner
 * Calls platform to generate a specific platforms based on settings given by PlatformRouteSpawner
 * Stores next platform spawnlocation so PlatformRouteSpawner knows when to spawn next one
 */

/*
 * Startingpoint, rangeof Y and X, objectlist, 
 * 
 */
public class PlatformRoute : PlatformRouteSpawner
{
    private Vector3 startingPoint;
    private PlatformRoute platformSettings;
    private List<GameObject> platformList;
    private Platform platform;
    public PlatformRoute(PlatformRouteSpawner routeSettings) 
    {
        this.startingPoint = routeSettings.routeStartPosition;
        this.platformRangeX = routeSettings.platformRangeX;
        this.platformRangeY = routeSettings.platformRangeY;
        this.platformRangeIncrement = routeSettings.platformRangeIncrement;
        this.platformSpeed = routeSettings.platformSpeed;
        this.platformSpawnIntervalMin = routeSettings.platformSpawnIntervalMin;
        this.platformSpawnIntervalMax = routeSettings.platformSpawnIntervalMax;
        this.objectPaths = routeSettings.objectPaths;
        platformList = new List<GameObject>();
    }

    public void SpawnPlatform(string direction = "up")
    {
        GameObject newPlatform = platform.SpawnPlatform(PickRandomPlatformType(), GetNextSpawnLocation(direction));
        platformList.Add(newPlatform);
    }
    public void SpawnPlatformStaticLocation()
    {
        GameObject newPlatform = platform.SpawnPlatform(PickRandomPlatformType(), startingPoint);
        platformList.Add(newPlatform);
    }

    private string PickRandomPlatformType()
    {
        int random = Random.Range(0, objectPaths.Count - 1);
        return objectPaths[random];
    }

    public GameObject GetLowestPlatform()
    {
        GameObject lowestplatform = new GameObject();
        if (platformList.Count > 0)
        {
            foreach (var platform in platformList)
            {
                if (lowestplatform == null)
                {
                    lowestplatform = platform;
                }
                else
                {
                    if (lowestplatform.transform.position.y > platform.transform.position.y)
                    {
                        lowestplatform = platform;
                    }
                }
            }
        }
        else
        {
            lowestplatform.transform.position = startingPoint;
        }
        return lowestplatform;
    }
    public GameObject GetHighestPlatform()
    {
        GameObject highestPlatform = new GameObject();
        if(platformList.Count > 0)
        {
            foreach (var platform in platformList)
            {
                if(highestPlatform == null)
                {
                    highestPlatform = platform;
                }
                else
                {
                    if(highestPlatform.transform.position.y < platform.transform.position.y)
                    {
                        highestPlatform = platform;
                    }
                }
            }
        }
        else
        {
            highestPlatform.transform.position = startingPoint;
        }
        return highestPlatform;
    }
    private Vector3 GetNextSpawnLocation(string direction = "up")
    {
        Vector3 newSpawnPosition = new Vector3();
        if(direction == "up")
        {
            Vector3 previousPlatform = HighestPlatform().transform.position;

            float minPointX = previousPlatform.x - platformRangeX / 2;
            float maxPointX = previousPlatform.x + platformRangeX / 2;
            float minPointY = previousPlatform.y + platformRangeY / 0.6f;
            float maxPointY = previousPlatform.y + platformRangeY;

            newSpawnPosition.x = Random.Range(minPointX, maxPointX);
            newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        }
        if (direction == "down")
        {
            Vector3 previousPlatform = LowestPlatform().transform.position;

            float minPointX = previousPlatform.x - platformRangeX / 2;
            float maxPointX = previousPlatform.x + platformRangeX / 2;
            float minPointY = previousPlatform.y - platformRangeY / 0.6f;
            float maxPointY = previousPlatform.y - platformRangeY;

            newSpawnPosition.x = Random.Range(minPointX, maxPointX);
            newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        }
        return newSpawnPosition;
    }
}
