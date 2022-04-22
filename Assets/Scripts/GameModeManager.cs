using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;

/*
 * Gamemodemanager checks if file exists or creates it with default values of gamemodes.
 * GameModeManager loads from txt gamemode settings and returns it to caller;
 */
public class GameModeManager
{
    public PlatformSmasher platformSmasher;
    public PlatformClimber platformClimber;

    private readonly List<GameModeManager> gamemodeList = new List<GameModeManager>();

    public int gamemodeID;
    public string gamemodeName,
        objectName,
        prefabsPath;
    public Vector3 platformGenerateStartPoint,
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
        platformSpawnY,
        cameraAccelerationSpeed,
        cameraAccumulatingSpeed,
        cameraSpeedToDistanceRatioZ,
        startingBounceVelocity,
        bounceSpeedRatio;
    public int platformsOnStart,
        platformRouteAmount,
        cameraMode;

    public bool CheckIfFileExists()
    {
        if(!File.Exists(Application.dataPath + "/GameModeSettings.txt"))
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
            Debug.Log("File written.");
            return true;
        }
        else
        {
            return true;
        }
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
                    Debug.Log("Successful load: " + gamemode);
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
        Debug.Log("JsonString: " + JsonString);
        //newList = JsonUtility.FromJson<List<List<GameModeManager>>>(JsonString);
        platformSmasher = JsonUtility.FromJson<PlatformSmasher>(JsonString);
        platformClimber = JsonUtility.FromJson<PlatformClimber>(JsonString);
        Debug.Log("platformSmasher.gamemodeName: " + platformSmasher.gamemodeName);
        Debug.Log("platformClimber.gamemodeName: " + platformClimber.gamemodeName);
        gamemodeList.Add(platformSmasher);
        gamemodeList.Add(platformClimber);
    }
}
