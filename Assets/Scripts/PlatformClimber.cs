using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformClimber : GameModeManager
{
    public int gamemodeID;
    public string gamemodeName = "Platform Climber",
        objectName = "Platform",
        prefabsPath = "Prefabs/";
    public Vector3 platformGenerateStartPoint;
    public float platformRangeX = 80f,
        platformRangeY = 10f,
        platformRangeIncrement = 1.01f,
        returnVelocity = 70f,
        maxMovementSpeed = 30f,
        gravityUpChange = 0.6f,
        gravityDownChange = 2.0f,
        platformSpawnIntervalMin = 0.7f,
        platformSpawnIntervalMax = 1.7f,
        falloffHeight = 40f,
        gravity = -40f,
        cameraFollowHorizontal = 15f,
        cameraPositioningTime = 0.04f,
        cameraOffsetZ = -60f,
        cameraOffsetY = 10f,
        platformSpeedRatioX = 100f,
        platformSpeedRatioY = 100f;
    public int platformsOnStart = 1,
        platformRouteAmount = 1;
}
