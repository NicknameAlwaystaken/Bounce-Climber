using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Platform
 * -------------------
 * Holds data for platform, example particle system and health
 */
public class Platform : MonoBehaviour
{
    public float health,
        scoreOnBreak;
    private float platformSpeed = 0;
    public GameObject platformBreak;
    public GameObject destructableObject;
    public GameObject fracturedObject;

    public float PlatformSpeed { get => platformSpeed; set => platformSpeed = value; }

    public void DestroyPlatform()
    {
        if(destructableObject != null)
        {
            Instantiate(fracturedObject, gameObject.transform.position, new Quaternion());
            Destroy(destructableObject);
        }
        //Instantiate(platformBreak, gameObject.transform.position, new Quaternion());
    }
    void FixedUpdate()
    {
        if(platformSpeed > 0)
        {
            gameObject.transform.position += Vector3.down * platformSpeed * Time.deltaTime;
            if (gameObject.transform.position.y <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
