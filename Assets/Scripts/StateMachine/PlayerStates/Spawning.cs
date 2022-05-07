using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : PlayerState
{
    public Spawning(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Spawning";
    }
    public override IEnumerator Start()
    {
        Debug.Log("fourth layer");
        PlayerController.SetPlayerState(new Bouncing(PlayerController, Player));
        yield break;
    }
}
