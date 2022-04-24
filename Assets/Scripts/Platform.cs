using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Platform
 * -------------------
 * Called by PlatformRoute to generate a specific type of platform
 */
public class Platform : MonoBehaviour
{
    private GameObject platform;

    public GameObject SpawnPlatform(string objectPath, Vector3 location)
    {
        GameObject newplatform = Resources.Load(objectPath) as GameObject;
        platform = Instantiate(newplatform, location, new Quaternion());
        return platform;
    }
}
