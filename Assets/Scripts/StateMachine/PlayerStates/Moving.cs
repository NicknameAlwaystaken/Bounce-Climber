using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : PlayerState
{
    public Moving(PlayerController controller, Player player) : base(controller, player)
    {
        Player.currentStateName = "Moving";
        Vector3 movement = Player.HorizontalInput * Player.MaxMovementSpeed * Player.transform.right;
        Rigidbody rb = Player.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(movement.x, rb.velocity.y);
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}