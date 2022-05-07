using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawningPlayer : GameState
{

    public SpawningPlayer(GameController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        GameController.SpawnPlayer();
        yield break;
    }
}