using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public float playerHeightSpawn;

    public GameObject player;

    public GameObject SpawnPlayer()
    {
        Vector3 spawnPosition = new Vector3(0f, playerHeightSpawn, 0f);
        GameObject obj = Instantiate(player, spawnPosition, new Quaternion());
        return obj;
    }
}
