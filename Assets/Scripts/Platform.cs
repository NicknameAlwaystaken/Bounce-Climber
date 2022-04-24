using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Called by PlatformRoute to generate a specific type of platform
 */
public class Platform : MonoBehaviour
{
    private GameObject platform;

    public GameObject SpawnPlatform(GameObject newPlatform)
    {
        platform = Instantiate(newPlatform);
        return platform;
    }
}
