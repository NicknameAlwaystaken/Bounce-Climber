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
    public bool colliderSet = true;
    public Vector3 nextColliderSize;
    public Vector3 nextColliderCenter;
    public int protectiveLayers = 0;
    public bool destructable = false;
    public GameObject platformBreak;
    public GameObject destructableObject;
    public GameObject fracturedObject;

    public float PlatformSpeed { get => platformSpeed; set => platformSpeed = value; }

    public void DestroyPlatform()
    {
        if(protectiveLayers > 0)
        {
            colliderSet = false;
            DestroyBreakable();
        }
        else
        {
            Instantiate(platformBreak, gameObject.transform.position, new Quaternion());
            Destroy(gameObject);
        }
    }

    public void DestroyBreakable()
    {
        Debug.Log("Destroying breakables");
        if(destructableObject != null)
        {
            Instantiate(fracturedObject, gameObject.transform.position, new Quaternion());
            Destroy(destructableObject);
            protectiveLayers--;
        }
        if(protectiveLayers == 0 && !colliderSet)
        {
            BoxCollider collider = (BoxCollider)gameObject.GetComponent<Collider>();

            collider.center = nextColliderCenter;
            collider.size = nextColliderSize;
        }
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
