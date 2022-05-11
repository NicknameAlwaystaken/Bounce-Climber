using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : PlayerState
{
    public Bouncing(PlayerController controller, Player player) : base(controller, player)
    {
        UserInputSystem.currentStateName = "Bouncing";
        UserInputSystem.BouncingDone = false;
        JumpWithoutEffects();
        UserInputSystem.Bouncing = false;
        UserInputSystem.BouncingDone = true;
    }
    private void JumpWithoutEffects()
    {
        Rigidbody rb = UserInputSystem.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * UserInputSystem.BounceVelocity * UserInputSystem.AutoJumpBounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
