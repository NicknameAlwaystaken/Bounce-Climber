using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformClimber : GameModeManager
{
    public PlatformClimber()
    {
        this.gamemodeID = 1;
        this.gamemodeName = "Platform Climber";
        this.objectName = "Platform";
        this.prefabsPath = "Prefabs/";
        this.platformGenerateStartPoint = new Vector3();
        this.platformRangeX = 80f;
        this.platformRangeY = 10f;
        this.platformRangeIncrement = 1.01f;
        this.returnVelocity = 70f;
        this.maxMovementSpeed = 30f;
        this.gravityUpChange = 0.6f;
        this.gravityDownChange = 2.0f;
        this.platformSpawnIntervalMin = 0.7f;
        this.platformSpawnIntervalMax = 1.7f;
        this.falloffHeight = 40f;
        this.gravity = new Vector3(0f, -40f, 0f);
        this.cameraFollowHorizontal = 15f;
        this.cameraPositioningTime = 0.04f;
        this.cameraOffsetZ = -60f;
        this.cameraOffsetY = 10f;
        this.platformSpeedRatioX = 100f;
        this.platformSpeedRatioY = 100f;
        this.platformsOnStart = 1;
        this.platformRouteAmount = 1;
        this.cameraMode = 2;
        this.playerSpawnLocation = new Vector3(0f, 3f, 0f);
        this.cameraStartLocation = new Vector3(0f, 50f, -50f);
    }
}
