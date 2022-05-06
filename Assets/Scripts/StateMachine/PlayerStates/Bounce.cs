using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : PlayerState
{
    public Bounce(GameModeController controller) : base(controller)
    {
    }

    public override IEnumerator Start()
    {
        Rigidbody rb = GameModeController.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * 30f;
        if(rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);

        base.Exit();

        yield break;
    }
}
