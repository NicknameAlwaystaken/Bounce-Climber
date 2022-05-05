using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartGame : GameState
{
    public StartGame(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        GameModeController.SetPlayerState(new SpawningPlayer(GameModeController));
        yield break;
    }
}
