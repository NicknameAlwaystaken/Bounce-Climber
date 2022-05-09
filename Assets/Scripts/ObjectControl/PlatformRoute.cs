using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlatformRoute : PlatformRouteSpawner
{
    private Vector3 startingPoint;
    private List<GameObject> platformList;
    private static int routeIndex = 0;

    enum GameMode
    {
        Freeplay = 0,
        No_Breaks = 1,
        Platform_Smasher = 2
    }
    public PlatformRoute(GameModeManager routeSettings) 
    {
        routeIndex += 1;
        platformList = new List<GameObject>();
        this.currentGameMode = routeSettings.gamemodeID;
        this.startingPoint = routeSettings.platformStartPoint;
        this.platformRangeX = routeSettings.platformRangeX;
        this.platformRangeY = routeSettings.platformRangeY;
        this.platformRangeIncrement = routeSettings.platformRangeIncrement;
        this.platformSpeed = routeSettings.platformSpeed;
        this.platformSpawnIntervalMin = routeSettings.platformSpawnIntervalMin;
        this.platformSpawnIntervalMax = routeSettings.platformSpawnIntervalMax;
        this.objectPaths = routeSettings.objectPaths;
    }

    public void SpawnPlatform(string direction = "up")
    {
        GameObject loadedPlatform = Resources.Load(PickRandomPlatformType()) as GameObject;
        GameObject newObject = Object.Instantiate(loadedPlatform, GetNextSpawnLocation(direction), new Quaternion());
        if (currentGameMode == (int)GameMode.Platform_Smasher)
        {
            newObject.GetComponent<Platform>().PlatformSpeed = 20f;
        }
        platformList.Add(newObject);
    }
    public void SpawnPlatformStaticLocation()
    {
        GameObject loadedPlatform = Resources.Load(PickRandomPlatformType()) as GameObject;
        GameObject newObject = Object.Instantiate(loadedPlatform, startingPoint, new Quaternion());
        if(currentGameMode == (int)GameMode.Platform_Smasher)
        {
            newObject.GetComponent<Platform>().PlatformSpeed = 5f;
        }
        platformList.Add(newObject);
    }

    private string PickRandomPlatformType()
    {
        int random = Random.Range(0, objectPaths.Count);
        return objectPaths[random];
    }

    public GameObject GetLowestPlatform()
    {
        if (platformList.Count > 0)
        {
            GameObject lowestplatform = platformList[0];
            foreach (var platform in platformList)
            {
                if (lowestplatform == null)
                {
                    lowestplatform = platform;
                }
                else if (platform != null && lowestplatform.transform.position.y > platform.transform.position.y)
                {
                    lowestplatform = platform;
                }
            }
            return lowestplatform;
        }
        else
        {
            return null;
        }
    }
    public GameObject GetHighestPlatform()
    {
        if (platformList.Count > 0)
        {
            GameObject highestPlatform = platformList[0];
            foreach (var platform in platformList)
            {
                if (highestPlatform == null)
                {
                    highestPlatform = platform;
                }
                else if (platform != null && highestPlatform.transform.position.y < platform.transform.position.y)
                {
                    highestPlatform = platform;
                }
            }
            return highestPlatform;
        }
        else
        {
            return null;
        }
    }
    private Vector3 GetNextSpawnLocation(string direction = "up")
    {
        Vector3 newSpawnPosition = new Vector3();
        if(currentGameMode == 2)
        {
            Vector3 previousPlatform = startingPoint;

            float minPointX = previousPlatform.x - platformRangeX / 2;
            float maxPointX = previousPlatform.x + platformRangeX / 2;
            float minPointY = previousPlatform.y + platformRangeY / 0.6f;
            float maxPointY = previousPlatform.y + platformRangeY;

            newSpawnPosition.x = Random.Range(minPointX, maxPointX);
            newSpawnPosition.y = Random.Range(minPointY, maxPointY);

            return newSpawnPosition;
        }
        if(direction == "up")
        {
            Vector3 previousPlatform;
            GameObject newPlatform = GetHighestPlatform();
            if(newPlatform != null)
            {
                previousPlatform = newPlatform.transform.position;
            }
            else
            {
                previousPlatform = new Vector3();
            }

            float minPointX = previousPlatform.x - platformRangeX / 2;
            float maxPointX = previousPlatform.x + platformRangeX / 2;
            float minPointY = previousPlatform.y + platformRangeY / 0.6f;
            float maxPointY = previousPlatform.y + platformRangeY;

            newSpawnPosition.x = Random.Range(minPointX, maxPointX);
            newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        }
        if (direction == "down")
        {
            GameObject newPlatform = GetLowestPlatform();
            Vector3 previousPlatform = newPlatform.transform.position;

            float minPointX = previousPlatform.x - platformRangeX / 2;
            float maxPointX = previousPlatform.x + platformRangeX / 2;
            float minPointY = previousPlatform.y - platformRangeY / 0.6f;
            float maxPointY = previousPlatform.y - platformRangeY;

            newSpawnPosition.x = Random.Range(minPointX, maxPointX);
            newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        }
        return newSpawnPosition;
    }
    public int GetRouteIndex()
    {
        return routeIndex;
    }
    public bool CheckRouteIndex(int index)
    {
        if (routeIndex == index)
        {
            return true;
        }
        return false;
    }
    public bool CheckIfPlatformInRoute(GameObject givenPlatform)
    {
        foreach(GameObject route in platformList.ToArray())
        {
            if(route != null && Equals(givenPlatform, route))
            {
                return true;
            }
        }
        return false;
    }
}
