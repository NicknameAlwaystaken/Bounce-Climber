using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : GameState
{
    public StartGame(GameController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        Debug.Log("second layer");
        GameController.SetGameState(new SpawningPlayer(GameController));
        yield break;
    }
}
