using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformRouteSpawner : MonoBehaviour
{
    private int currentRouteAmount;
    public float startingPointOffsetY, newRouteMaxDistance, platformDespawnDistance, maxDistance, minDistance, maxHeight, minHeight;
    private bool setupDone = false;
    public string prefabsPath = "Prefabs/";
    private string completePath;
    private Vector3 newRoutePosition;

    //private Platform platform;
    //private PlatformRoute platformRoute;
    private List<GameObject> platformRoute;
    private List<List<GameObject>> platformRouteList;
    private Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
    }

    public void SetupRoutes(string objectName, int routeStartLength, int routeAmount, Vector3 playerPosition)
    {
        platformRouteList = new List<List<GameObject>>();
        //platformRoute = GetComponent<PlatformRoute>();
        currentRouteAmount = routeAmount;
        //playerPosition.y += startingPointOffsetY;
        for (int i = 0; i < currentRouteAmount; i++)
        {
            //Debug.Log("Setting up routes");
            SpawnRoute(objectName, routeStartLength, playerPosition);
            //Debug.Log("Done setting up routes");
        }
        setupDone = true;
        /*
        platformRouteListNew = new List<PlatformRoute>();
        platformRouteListNew.Add(GetComponent<PlatformRoute>());
        platformRouteListNew.Add(GetComponent<PlatformRoute>());
        platformRouteListNew[0].Setup(objectName, 2);
        platformRouteListNew[1].Setup(objectName, 2);
        for(int i = 0; i < 50; i++)
        {
            platformRouteListNew[i%2].SpawnPlatform("Platform");
        }
        */
    }
    public void SpawnRoute(string objectName, int routeStartLength, Vector3 playerPosition)
    {
        platformRoute = new List<GameObject>();
        float minX = playerPosition.x - newRouteMaxDistance;
        float maxX = playerPosition.x + newRouteMaxDistance;
        Vector3 startingPoint = new Vector3(Random.Range(minX, maxX), playerPosition.y - startingPointOffsetY);
        newRoutePosition = startingPoint;
        //Debug.Log("list to platformRouteList");
        platformRouteList.Add( new List<GameObject>());
        //Debug.Log("list to platformRouteList done");
        AddPlatformToRouteList(platformRoute, objectName, routeStartLength);
        //Debug.Log("platformRouteList.Count: " + platformRouteList.Count);
        /*
        foreach (List<GameObject> list in platformRouteList)
        {
            Debug.Log("List: " + list);
            foreach(GameObject obj in list)
            {
                Debug.Log("Obj: " + obj);
            }
        }*/
        //platformRouteList.Add(new PlatformRoute());
        //Debug.Log("Spawning Route2");
        //platformRouteList.Add(GetComponent<PlatformRoute>());
        //Debug.Log("Spawning Route3");
        //platformRouteList[platformRouteList.Count-1].Setup(objectName, routeStartLength, maxDistance, minDistance, maxHeight, minHeight, startingPoint);
        //Debug.Log("Spawning Route4");
        /*
        platformRouteListNew = new List<PlatformRoute>();
        platformRouteListNew.Add(GetComponent<PlatformRoute>());
        platformRouteListNew.Add(GetComponent<PlatformRoute>());
        platformRouteListNew[0].Setup(objectName, 2);
        platformRouteListNew[1].Setup(objectName, 2);
        for(int i = 0; i < 50; i++)
        {
            platformRouteListNew[i%2].SpawnPlatform("Platform");
        }
        */
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

        float minPointX = lastSpawnPosition.x - minDistance;
        float maxPointX = lastSpawnPosition.x + maxDistance;
        float minPointY = lastSpawnPosition.y + minHeight;
        float maxPointY = lastSpawnPosition.y + maxHeight;

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
                    Debug.Log("lowestPlatform: " + lowestPlatform.transform.position.y);
                    Vector3 lowestPlatformPosition = lowestPlatform.transform.position;
                    if (lowestPlatformPosition.y < mainCamera.transform.position.y - platformDespawnDistance)
                    {
                        //Debug.Log("Destroying platform");
                        AddPlatformToRouteList(itemList, "Platform");
                        Destroy(lowestPlatform);
                        break;
                    }
                }
            }
            /*
            for (int i = 0; i < platformRouteList.Count; i++)
            {
                Debug.Log("Crash1");
                List<GameObject> itemList = platformRouteList[i];
                Debug.Log("Crash2");
                GameObject lowestPlatform = getLowestPlatform(itemList);
                Debug.Log("Crash3");
                if (lowestPlatform != null)
                {
                    Debug.Log("lowestPlatform: " + lowestPlatform.transform.position.y);
                    Vector3 lowestPlatformPosition = lowestPlatform.transform.position;
                    Debug.Log("Crash4");
                    if (lowestPlatformPosition.y < mainCamera.transform.position.y - platformDespawnDistance)
                    {
                        Debug.Log("Destroying platform");
                        AddPlatformToRouteList(itemList, "Platform", getHighestPlatform(itemList).transform.position);
                        Destroy(lowestPlatform);
                    }
                }
            }
            */
        }
    }
    public GameObject getLowestPlatform(List<GameObject> fromRoute)
    {
        GameObject lowestPlatform = GetComponent<GameObject>();
        //Debug.Log("Lowest Platform: " + lowestPlatform + "Lowest Platform Y: " + lowestPlatform.transform.position.y);
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
    public void AddPlatformToRouteList(List<GameObject> list, string objectName, int platformAmount = 1)
    {
        for(int i = 0; i < platformAmount; i++)
        {
            GameObject newPlatform = CreatePlatform(objectName, GetNextSpawnLocation(list));
            //int index = platformRouteList.IndexOf(list);
            //Debug.Log("Adding platform to list index: " + index);
            //Debug.Log("Count: " + route.Count);
            list.Add(newPlatform);
        }
        if(platformAmount > 1)
        {
            platformRouteList.Add(platformRoute);
        }
        //Debug.Log("Adding platform(s) to list done");
    }
    public GameObject CreatePlatform(string objectName, Vector3 location)
    {
        //Debug.Log("CreatePlatform location: " + location);
        completePath = prefabsPath + objectName;
        GameObject newplatform = Resources.Load(completePath) as GameObject;
        GameObject platform = Instantiate(newplatform, location, new Quaternion());
        //Debug.Log("Adding to list");
        //route.Add(newPlatform);
        //Debug.Log("Adding to list done");
        return platform;
    }
}
