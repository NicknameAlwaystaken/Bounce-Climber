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

    public float PlatformSpeed { get => platformSpeed; set => platformSpeed = value; }

    public void DestroyPlatform()
    {
        Instantiate(platformBreak, gameObject.transform.position, new Quaternion());
        Destroy(gameObject);
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
