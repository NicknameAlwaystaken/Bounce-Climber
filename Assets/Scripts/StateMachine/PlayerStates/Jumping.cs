using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    public Jumping(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Jumping";
        JumpWithEffects("Jumping", Player.BounceVelocity * Player.FirstJumpIncrement);
        Player.JumpingDone = true;
    }
    public override IEnumerator Start()
    {
        yield break;
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