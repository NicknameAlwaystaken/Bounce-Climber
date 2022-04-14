using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlatformRoute
{
    private float maxDistance, minDistance, maxHeight, minHeight;

    private Platform platform;
    /*
    public void Setup(string objectName, int length, float newMaxDistance, float newMinDistance, float newMaxHeight, float newMinHeight, Vector3 startingPoint)
    {
        platform = GetComponent<Platform>();
        maxDistance = newMaxDistance;
        minDistance = newMinDistance;
        maxHeight = newMaxHeight;
        minHeight = newMinHeight;
    }
    */
    public GameObject SpawnPlatformToList(string objectName, Vector3 location)
    {
        Debug.Log("SpawnPlatformToList in route");
        GameObject newPlatform = platform.SpawnPlatform(objectName, location);
        Debug.Log("SpawnPlatformToList in route done");
        return newPlatform;
    }
    // Start is called before the first frame update
    void Start()
    {
        //platformList = new List<GameObject>();
    }
}
