using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/*
 * Gamemodemanager checks if file exists or creates it with default values of gamemodes. --DONE--
 * GameModeManager loads from GameModeSettings.txt and returns it to caller. --DONE--
 */
public class GameModeManager
{
    public PlatformSmasher platformSmasher;
    public PlatformClimber platformClimber;

    private readonly List<GameModeManager> gamemodeList = new List<GameModeManager>();

    public List<string> objectPaths;
    public string gamemodeName;
    public Vector3 platformStartPoint,
        playerSpawnLocation,
        cameraStartLocation,
        gravity;
    public float platformRangeX,
        platformRangeY,
        platformRangeIncrement,
        returnVelocity,
        maxMovementSpeed,
        gravityUpChange,
        gravityDownChange,
        platformSpawnIntervalMin,
        platformSpawnIntervalMax,
        falloffHeight,
        cameraFollowHorizontal,
        cameraPositioningTime,
        cameraOffsetZ,
        cameraOffsetY,
        platformSpeedRatioX,
        platformSpeedRatioY,
        platformSpeed,
        cameraAccelerationSpeed,
        cameraAccumulatingSpeed,
        cameraSpeedToDistanceRatioZ,
        startingBounceVelocity,
        bounceSpeedRatio,
        returnHeight,
        maxDropSpeed,
        diveCooldownCounter,
        diveCooldown,
        startingPointOffsetY,
        newRouteMaxDistance,
        platformDespawnDistance,
        maxDistance,
        minDistance,
        maxHeight,
        minHeight;
    public int gamemodeID,
        platformsOnStart,
        platformRouteAmount,
        cameraMode;

    public bool CheckIfFileValid()
    {
        if(!File.Exists(Application.dataPath + "/GameModeSettings.txt"))
        {
            CreateFile();
            return true;
        }
        else
        {
            return true;
        }
    }

    private void CreateFile()
    {
        string strOutput = "";
        platformSmasher = new PlatformSmasher();
        platformClimber = new PlatformClimber();


        // object name has to be exact with the name of the class
        strOutput += "{";
        strOutput += "\"" + "platformSmasher" + "\"" + ": [" + JsonUtility.ToJson(platformSmasher) + "]," + "\n";
        strOutput += "\"" + "platformClimber" + "\"" + ": [" + JsonUtility.ToJson(platformClimber) + "]\n";
        strOutput += "}";



        File.WriteAllText(Application.dataPath + "/GameModeSettings.txt", strOutput);
        Debug.Log("File didn't exist. File written.");
    }

    public GameModeManager LoadGamemodeSettings(int id)
    {
        Load();
        foreach (GameModeManager gamemode in gamemodeList)
        {
            if(gamemode != null)
            {
                if (gamemode.gamemodeID == id)
                {
                    var gamemodeSettings = gamemode;
                    Debug.Log("Successful gamemode load: " + gamemode);
                    return gamemodeSettings;
                }
            }
        }
        return new GameModeManager();
    }
    private void Load()
    {
        //List<List<GameModeManager>> newList = new List<List<GameModeManager>>();
        string JsonString = File.ReadAllText(Application.dataPath + "/GameModeSettings.txt");
        //newList = JsonUtility.FromJson<List<List<GameModeManager>>>(JsonString);
        platformSmasher = JsonUtility.FromJson<PlatformSmasher>(JsonString);
        platformClimber = JsonUtility.FromJson<PlatformClimber>(JsonString);
        gamemodeList.Add(platformSmasher);
        gamemodeList.Add(platformClimber);
    }
}
