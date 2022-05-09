using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : PlayerState
{
    public Bouncing(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Bouncing";
        Player.BouncingDone = false;
        JumpWithoutEffects();
        Player.Bouncing = false;
        Player.BouncingDone = true;
    }
    private void JumpWithoutEffects()
    {
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * Player.BounceVelocity * Player.AutoJumpBounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
