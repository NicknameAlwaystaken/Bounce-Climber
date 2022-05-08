using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    public Jumping(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Jumping";
    }
    public override IEnumerator Start()
    {
        Player.JumpingAllowed = true;
        yield break;
    }
    public override IEnumerator Jump()
    {
        if (Player.Jumping)
        {
            Player.currentStateName = "Jumping";
            Player.JumpingDone = false;
            Player.AudioSource.Play();
            Object.Instantiate(Player.particles, Player.transform.position, new Quaternion());
            Rigidbody rb = Player.GetComponent<Rigidbody>();
            Vector3 upVelocity = Vector3.up * Player.BounceVelocity;
            if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
            Player.Jumping = false;
            Player.Bouncing = false;
            Player.JumpingDone = true;
            yield break;
        }
    }
}