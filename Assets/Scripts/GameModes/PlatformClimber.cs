using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformClimber : GameModeManager
{
    public PlatformClimber()
    {
        this.gamemodeID = 1;
        this.cameraMode = 2;
        this.platformsOnStart = 12;
        this.platformRouteAmount = 3;
        this.gamemodeName = "Platform Climber";
        this.objectPaths = new List<string> {
            "Prefabs/icicle_platform",
            "Prefabs/grass_platform"};
        this.platformStartPoint = new Vector3(0f, 0f, 0f);
        this.playerSpawnLocation = new Vector3(0f, 10f, 0f);
        this.cameraStartLocation = new Vector3(0f, 5f, -50f);
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
        this.cameraAccelerationSpeed = 200f;
        this.cameraAccumulatingSpeed = 0.05f;
        this.cameraSpeedToDistanceRatioZ = 5f;
        this.startingBounceVelocity = 30f;
        this.cameraOffsetZ = -60f;
        this.cameraOffsetY = 10f;
        this.platformSpeedRatioX = 100f;
        this.platformSpeedRatioY = 100f;
        this.returnHeight = 0f;
        this.maxDropSpeed = 0f;
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
