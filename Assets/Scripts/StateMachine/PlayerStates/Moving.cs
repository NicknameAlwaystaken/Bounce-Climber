using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : PlayerState
{
    public Moving(PlayerController controller, Player player) : base(controller, player)
    {
        UserInputSystem.currentStateName = "Moving";
        Vector3 movement = UserInputSystem.HorizontalInput * UserInputSystem.MaxMovementSpeed * UserInputSystem.transform.right;
        Rigidbody rb = UserInputSystem.GetComponent<Rigidbody>();
        rb.velocity = new Vector3(movement.x, rb.velocity.y);
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
