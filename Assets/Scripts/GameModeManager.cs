using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameModeManager : MonoBehaviour
{

    [System.Serializable]
    public class PlatformSmasher
    {
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

    [System.Serializable]
    public class PlatformClimber
    {

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

    public PlatformSmasher platformSmasher = new PlatformSmasher();
    public PlatformClimber platformClimber = new PlatformClimber();


    public void OutputJSON()
    {
        string strOutput = "";
        strOutput += JsonUtility.ToJson(platformSmasher);
        strOutput += JsonUtility.ToJson(platformClimber);
        File.WriteAllText(Application.dataPath + "/GameModeSettings.txt", strOutput);
        Debug.Log("File written.");

    }
}
