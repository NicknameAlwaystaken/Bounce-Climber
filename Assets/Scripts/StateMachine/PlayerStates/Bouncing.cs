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
        yield break;
    }
    public override IEnumerator Jump()
    {
        Player.JumpingDone = false;
        Player.AudioSource.Play();
        Object.Instantiate(Player.particles, Player.transform.position, new Quaternion());
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * Player.TempBounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        Player.Jumping = false;
        Player.JumpingDone = true;
        yield break;
    }
    public override IEnumerator Move()
    {
        Vector3 movement = Player.HorizontalInput * Player.MaxMovementSpeed * Player.transform.right;
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(movement.x, rb.velocity.y);
        yield break;
    }
    public override IEnumerator Update()
    {
        yield break;
    }
}
