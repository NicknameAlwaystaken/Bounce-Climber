using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRouteSpawner
{
    private int currentRouteAmount, routeLength;
    public int currentGameMode;
    private float startingPointOffsetY, newRouteMaxDistance, platformDespawnDistance, maxDistance, minDistance, maxHeight, minHeight;
    public float platformRangeX,
        platformRangeY,
        platformRangeIncrement,
        platformSpeed,
        platformSpawnIntervalMin,
        platformSpawnIntervalMax;
    public List<string> objectPaths;
    public Vector3 routeStartPosition;

    private GameModeManager routeSettings;

    private List<PlatformRoute> platformRouteList;

    enum GameMode
    {
        Freeplay = 0,
        No_Breaks = 1,
        Platform_Smasher = 2
    }

    public void GenerateRoute()
    {
        PlatformRoute platformRoute = new PlatformRoute(routeSettings);
        for (int i = 0; i < routeLength; i++)
        {
            platformRoute.SpawnPlatform();
        }
        platformRouteList.Add(platformRoute);
    }

    public void GameSettings(GameModeManager newSettings)
    {
        routeSettings = newSettings;
        platformRouteList = new List<PlatformRoute>();
        objectPaths = routeSettings.objectPaths;
        currentRouteAmount = routeSettings.platformRouteAmount;
        routeLength = routeSettings.platformsOnStart;
        routeStartPosition = routeSettings.platformStartPoint;

        currentGameMode = routeSettings.gamemodeID;



        startingPointOffsetY = routeSettings.startingPointOffsetY;
        newRouteMaxDistance = routeSettings.newRouteMaxDistance;
        platformDespawnDistance = routeSettings.platformDespawnDistance;
        maxDistance = routeSettings.maxDistance;
        minDistance = routeSettings.minDistance;
        maxHeight = routeSettings.maxHeight;
        minHeight = routeSettings.minHeight;

    }


    public void SpawnRoutes()
    {
        for(int i = 0; i < currentRouteAmount; i++)
        {
            GenerateRoute();
        }
    }

    public GameObject GetHighestPlatformInRoutes()
    {
        GameObject highestPlatform = new GameObject();
        foreach(PlatformRoute route in platformRouteList.ToArray())
        {
            var itemRoute = route;
            if(highestPlatform != null)
            {
                Object.Destroy(highestPlatform);
                highestPlatform = itemRoute.GetHighestPlatform();
            }
            if (highestPlatform.transform.position.y < itemRoute.GetHighestPlatform().transform.position.y)
            {
                highestPlatform = itemRoute.GetHighestPlatform();
            }
        }
        return highestPlatform;
    }
    public GameObject GetLowestRoutePlatform() // returns route that has highest platform being lower than other route's highest platforms
    {
        if(platformRouteList.Count > 0)
        {
            List<GameObject> routeList = new List<GameObject>();
            GameObject routePlatform;
            foreach (PlatformRoute route in platformRouteList.ToArray())
            {
                var itemRoute = route;
                routePlatform = itemRoute.GetHighestPlatform();
                if(routePlatform != null)
                {
                    if (routePlatform.transform.position.y < itemRoute.GetHighestPlatform().transform.position.y)
                    {
                        routePlatform = itemRoute.GetHighestPlatform();
                    }
                    routeList.Add(routePlatform);
                }
            }
            if (routeList.Count == 0)
            {
                return null;
            }
            GameObject platformToReturn = routeList[0];
            for (int i = 0; i < routeList.Count; i++)
            {
                var item = routeList[i];

                if(item.transform.position.y < platformToReturn.transform.position.y)
                {
                    platformToReturn = item;
                }
            }
            return platformToReturn;
        }
        return null;
    }
    public GameObject GetLowestPlatformInRoutes()
    {
        if (platformRouteList.Count > 0)
        {
            GameObject lowestPlatform = platformRouteList[0].GetLowestPlatform();
            foreach (PlatformRoute route in platformRouteList)
            {
                if(route != null)
                {
                    var itemRoute = route;
                    if(lowestPlatform != null && lowestPlatform.transform.position.y > itemRoute.GetLowestPlatform().transform.position.y)
                    {
                        lowestPlatform = itemRoute.GetLowestPlatform();
                    }
                }
            }
            return lowestPlatform;
        }
        return null;
    }

    public void CallRouteToSpawn(GameObject newPlatform)
    {
        foreach (PlatformRoute route in platformRouteList.ToArray())
        {
            if (route != null && route.CheckIfPlatformInRoute(newPlatform))
            {
                route.SpawnPlatform();
            }
        }
    }
    public void SpawnInRoute(int index)
    {
        foreach (PlatformRoute route in platformRouteList.ToArray())
        {
            if (route != null && route.CheckRouteIndex(index))
            {
                route.SpawnPlatform();
            }
        }
    }
    public void PlatformSmasherSpawnPlatform()
    {
        foreach (PlatformRoute route in platformRouteList.ToArray())
        {
            if (route != null)
            {
                route.SpawnPlatform();
            }
        }
    }
    private void IncrementPlatformRange()
    {
        platformRangeX *= platformRangeIncrement;
        platformRangeY *= platformRangeIncrement;
    }
    public int GetRouteAmount()
    {
        return platformRouteList.Count;
    }
}
