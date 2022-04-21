using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameModeManager
{
    public PlatformSmasher platformSmasher;
    public PlatformClimber platformClimber;

    private List<GameModeManager> gamemodeList = new List<GameModeManager>();


    public void OutputJSON()
    {
        string strOutput = "";
        platformSmasher = new PlatformSmasher();
        platformClimber = new PlatformClimber();
        platformSmasher.gamemodeID = 0;
        platformClimber.gamemodeID = 1;
        gamemodeList.Add(platformSmasher);
        gamemodeList.Add(platformClimber);

        strOutput += "\"" + platformSmasher.gamemodeName + "\"" + " : " + JsonUtility.ToJson(platformSmasher) + "\n";
        strOutput += "\"" + platformClimber.gamemodeName + "\"" + " : " + JsonUtility.ToJson(platformClimber) + "\n";
        if(!File.Exists(Application.dataPath + "/GameModeSettings.txt"))
        {
            File.WriteAllText(Application.dataPath + "/GameModeSettings.txt", strOutput);
            Debug.Log("File written.");
        }
        else
        {

        }

    }
}
