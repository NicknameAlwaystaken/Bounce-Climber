using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRouteSpawner : MonoBehaviour
{
    private int currentRouteAmount, routeLength;
    public float startingPointOffsetY, newRouteMaxDistance, platformDespawnDistance, maxDistance, minDistance, maxHeight, minHeight;
    private float platformRangeX = 20,
        platformRangeY = 10,
        platformRangeIncrement = 1;
    private bool setupDone = false;
    public string prefabsPath = "Prefabs/";
    private string completePath, objectName;
    private Vector3 newRoutePosition;

    private List<GameObject> platformRoute;
    private List<List<GameObject>> platformRouteList;
    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }
    public void Setup(string newObjectName, Vector3 startingPoint, float newPlatformRangeX, float newPlatformRangeY, int newRouteLength, int routeAmount)
    {
        platformRouteList = new List<List<GameObject>>();
        objectName = newObjectName;
        currentRouteAmount = routeAmount;
        routeLength = newRouteLength;
        newRoutePosition = startingPoint;
        SetPlatformRanges(newPlatformRangeX, newPlatformRangeY);
        setupDone = true;
    }
    public void SpecialSetup(float newRangeIncrement = 1)
    {
        platformRangeIncrement = newRangeIncrement;
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
        Vector3 startingPoint = new Vector3(Random.Range(minX, maxX), newRoutePosition.y - startingPointOffsetY);
        newRoutePosition = startingPoint;

        platformRouteList.Add( new List<GameObject>());
        AddPlatformToRouteList(platformRoute, routeLength, true);
    }

    private Vector3 GetNextSpawnLocation(List<GameObject> fromRoute)
    {
        Vector3 lastSpawnPosition = new Vector3();
        if (fromRoute.Count > 0)
        {
            lastSpawnPosition = getHighestPlatform(fromRoute).transform.position;
        }
        else
        {
            lastSpawnPosition = newRoutePosition;
        }
        Vector3 newSpawnPosition = new Vector3();

        float minPointX = lastSpawnPosition.x - platformRangeX / 2;
        float maxPointX = lastSpawnPosition.x + platformRangeX / 2;
        float minPointY = lastSpawnPosition.y + platformRangeY / 0.6f;
        float maxPointY = lastSpawnPosition.y + platformRangeY;

        newSpawnPosition.x = Random.Range(minPointX, maxPointX);
        newSpawnPosition.y = Random.Range(minPointY, maxPointY);
        Debug.Log("Highest platform: " + newSpawnPosition.y);
        return newSpawnPosition;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(setupDone)
        {
            foreach (List<GameObject> list in platformRouteList.ToArray())
            {
                var itemList = list;
                GameObject lowestPlatform = getLowestPlatform(itemList);
                if (lowestPlatform != null)
                {
                    Vector3 lowestPlatformPosition = lowestPlatform.transform.position;
                    if (lowestPlatformPosition.y < mainCamera.transform.position.y - platformDespawnDistance)
                    {
                        AddPlatformToRouteList(itemList);
                        Debug.Log("PlatformRangeX: " + platformRangeX);
                        Debug.Log("PlatformRangeY: " + platformRangeY);
                        Destroy(lowestPlatform);
                        break;
                    }
                }
            }
        }
    }
    public GameObject getLowestPlatform(List<GameObject> fromRoute)
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
    public GameObject getHighestPlatform(List<GameObject> fromRoute)
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
            GameObject newPlatform = CreatePlatform(objectName, GetNextSpawnLocation(route));
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
    private void IncrementPlatformRange()
    {
        platformRangeX *= platformRangeIncrement;
        platformRangeY *= platformRangeIncrement;
    }
}
