using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    private Vector3 platformlocation;
    private GameObject platform;
    public string prefabsPath = "Prefabs/";
    private string completePath;
    /*
    public void setPlatformObject(Platform obj)
    {
        platform = obj;
    }
    public Platform getPlatformObject()
    {
        return platform;
    }*/
    public GameObject SpawnPlatform(string objectName, Vector3 location)
    {
        platformlocation = location;
        completePath = prefabsPath + objectName;
        GameObject newplatform = Resources.Load(completePath) as GameObject;
        //Debug.Log(completePath);
        //Debug.Log(platform);
        platform = Instantiate(newplatform, platformlocation, new Quaternion());
        //platform = obj;
        //Debug.Log("done spawning object");
        return platform;
    }
}
