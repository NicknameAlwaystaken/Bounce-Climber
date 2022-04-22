using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSmasher : GameModeManager
{
    public PlatformSmasher()
    {
        this.gamemodeID = 2;
        this.gamemodeName = "Platform Smasher";
        this.objectName = "Platform_ThickAndWide";
        this.prefabsPath = "Prefabs/";
        this.platformGenerateStartPoint = new Vector3();
        this.platformRangeX = 90f;
        this.platformRangeY = 0f;
        this.platformRangeIncrement = 1.0f;
        this.returnVelocity = 70f;
        this.maxMovementSpeed = 75f;
        this.gravityUpChange = 0.6f;
        this.gravityDownChange = 2.0f;
        this.platformSpeed = 15f;
        this.platformSpawnY = 85f;
        this.platformSpawnIntervalMin = 0.7f;
        this.platformSpawnIntervalMax = 1.7f;
        this.falloffHeight = 40f;
        this.gravity = new Vector3(0f, -40f, 0f);
        this.cameraFollowHorizontal = 15f;
        this.cameraPositioningTime = 0.04f;
        this.cameraAccelerationSpeed = 200f;
        this.cameraAccumulatingSpeed = 0.05f;
        this.cameraSpeedToDistanceRatioZ = 5f;
        this.startingBounceVelocity = 30f;
        this.bounceSpeedRatio = 35f;
        this.platformsOnStart = 1;
        this.platformRouteAmount = 1;
        this.cameraMode = 0;
        this.playerSpawnLocation = new Vector3(0f, 60f, 0f);
        this.cameraStartLocation = new Vector3(0f, 50f, -50f);
    }
}
