using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumping : PlayerState
{
    public DoubleJumping(PlayerController controller, Player player) : base(controller, player)
    {
    }
    public override IEnumerator Start()
    {
        Player.DoubleJumpingAllowed = true;
        yield break;
    }
    public override IEnumerator DoubleJump()
    {
        if (Player.DoubleJumping)
        {
            JumpWithEffects("DoubleJumping", Player.BounceVelocity * Player.DoubleJumpIncrement);
            Player.DoubleJumpingConditions = false;
            Player.JumpingDone = false;
            yield break;
        }
    }
    private void JumpWithEffects(string stateName, float bounceVelocity)
    {
        Player.currentStateName = stateName;
        Player.AudioSource.Play();
        Object.Instantiate(Player.particles, Player.transform.position, new Quaternion());
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * bounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        Player.Jumping = false;
        Player.Bouncing = false;
        Player.DoubleJumping = false;
    }
}