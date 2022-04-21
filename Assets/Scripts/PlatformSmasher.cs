using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSmasher : GameModeManager
{
    public int gamemodeID;
    public string gamemodeName = "Platform Smasher",
        objectName = "Platform_ThickAndWide",
        prefabsPath = "Prefabs/";
    public Vector3 platformGenerateStartPoint;
    public float platformRangeX = 90f,
        platformRangeY = 0f,
        platformRangeIncrement = 1.0f,
        returnVelocity = 70f,
        maxMovementSpeed = 75f,
        gravityUpChange = 0.6f,
        gravityDownChange = 2.0f,
        platformSpeed = 15f,
        platformSpawnY = 85f,
        platformSpawnIntervalMin = 0.7f,
        platformSpawnIntervalMax = 1.7f,
        falloffHeight = 40f,
        gravity = -40f,
        cameraFollowHorizontal = 15f,
        cameraPositioningTime = 0.04f,
        cameraAccelerationSpeed = 200f,
        cameraAccumulatingSpeed = 0.05f,
        cameraSpeedToDistanceRatioZ = 5f,
        startingBounceVelocity = 30f,
        bounceSpeedRatio = 35f;
    public int platformsOnStart = 1,
        platformRouteAmount = 1;
}
