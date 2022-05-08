using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncing : PlayerState
{
    public Bouncing(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Bouncing";
    }
    public override IEnumerator Start()
    {
        Player.MovingAllowed = true;
        Player.JumpingAllowed = true;
        Player.AutoJumpingAllowed = true;
        Player.DoubleJumpingAllowed = true;
        yield break;
    }
    public override IEnumerator Jump()
    {
        if (Player.Jumping)
        {
            JumpWithEffects("Jumping", Player.BounceVelocity * Player.FirstJumpIncrement);
            Player.JumpingDone = true;
            yield break;
        }
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
        Player.AutoJumping = false;
        Player.DoubleJumping = false;
    }

    public override IEnumerator AutoJump()
    {
        if (Player.MovingUp)
        {
            yield break;
        }
        Player.currentStateName = "AutoJumping";
        Player.AutoJumpingDone = false;
        /*
        Player.AudioSource.Play();
        Object.Instantiate(Player.particles, Player.transform.position, new Quaternion());
        */
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * Player.BounceVelocity * Player.AutoJumpBounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        Player.AutoJumping = false;
        Player.AutoJumpingDone = true;
        yield break;
    }
    public override IEnumerator Move()
    {
        Player.currentStateName = "Moving";
        Vector3 movement = Player.HorizontalInput * Player.MaxMovementSpeed * Player.transform.right;
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(movement.x, rb.velocity.y);
        yield break;
    }
}
