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

        GameModeController.player = Object.Instantiate(GameModeController.player, GameModeController.spawnPoint, new Quaternion());

        yield return new WaitForSeconds(2f);

        GameModeController.SetPlayerState(new Moving(GameModeController));
    }
}