using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject player;

    public GameObject SpawnPlayer(Vector3 newSpawnPoint = new Vector3())
    {
        Vector3 spawnPosition = newSpawnPoint;
        GameObject obj = Instantiate(player, spawnPosition, new Quaternion());
        return obj;
    }
}
