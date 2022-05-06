using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpawningPlayer : PlayerState
{

    public SpawningPlayer(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {

        GameModeController.SpawnPlayer();

        yield return new WaitForSeconds(2f);

        GameModeController.SetPlayerState(new Moving(GameModeController));
    }
}