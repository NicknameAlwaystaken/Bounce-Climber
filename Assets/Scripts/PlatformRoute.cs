using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
 * A specific route to handle platforms in
 * Have information of how many are in route spawned
 * Have information of platforms spawned for this route
 * Will have it's own individual platform settings like spawning different types of platforms based on chance
 * Is called from PlatformRouteSpawner
 * Calls platform to generate a specific platforms based on settings given by PlatformRouteSpawner
 * 
 */
public class PlatformRoute : PlatformRouteSpawner
{
    private GameObject platformSettings;
    private Platform platform;
    public PlatformRoute() 
    {

    }
    private float maxDistance, minDistance, maxHeight, minHeight;

    public GameObject SpawnPlatformToList(string objectName, Vector3 location)
    {
        Debug.Log("SpawnPlatformToList in route");
        GameObject newPlatform = platform.SpawnPlatform(objectName, location);
        Debug.Log("SpawnPlatformToList in route done");
        return newPlatform;
    }

}
