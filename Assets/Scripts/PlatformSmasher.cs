using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSmasher : GameModeManager
{
    public PlatformSmasher()
    {
        this.gamemodeID = 2;
        this.cameraMode = 0;
        this.platformsOnStart = 1;
        this.platformRouteAmount = 1;
        this.gamemodeName = "Platform Smasher";
        this.objectPaths = new List<string> {
            "Prefabs/Platform_ThickAndWide" };
        this.platformStartPoint = new Vector3(0f, 85f, 0f);
        this.playerSpawnLocation = new Vector3(0f, 60f, 0f);
        this.cameraStartLocation = new Vector3(0f, 50f, -50f);
        this.platformRangeX = 90f;
        this.platformRangeY = 0f;
        this.platformRangeIncrement = 1.0f;
        this.returnVelocity = 70f;
        this.maxMovementSpeed = 75f;
        this.gravityUpChange = 0.6f;
        this.gravityDownChange = 2.0f;
        this.platformSpeed = 15f;
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
        this.cameraOffsetZ = -60f;
        this.cameraOffsetY = 10f;
        this.platformSpeedRatioX = 100f;
        this.platformSpeedRatioY = 100f;
        this.returnHeight = 75f;
        this.maxDropSpeed = 75f;
        this.diveCooldownCounter = 0f;
        this.diveCooldown = 0.5f;
        this.startingPointOffsetY = 20f;
        this.newRouteMaxDistance = 20f;
        this.platformDespawnDistance = 50f;
        this.maxDistance = 20f;
        this.minDistance = 10f;
        this.maxHeight = 10f;
        this.minHeight = 8f;
        this.bounceSpeedRatio = 35f;
    }
}
