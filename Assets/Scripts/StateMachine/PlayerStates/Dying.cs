using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dying : PlayerState
{
    public Dying(PlayerController controller, Player player) : base(controller, player)
    {
    }
    public override IEnumerator Start()
    {
        yield break;
    }
    public override IEnumerator Die()
    {
        UserInputSystem.currentStateName = "Dying";
        yield break;

    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}