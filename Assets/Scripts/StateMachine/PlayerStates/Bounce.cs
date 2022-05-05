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
        yield break;
    }

    public override void Update()
    {
        audioSource.Play();
        Instantiate(particles, transform.position, new Quaternion());
        bounce = false;
        Vector3 upVelocity = Vector3.up * bounceVelocity;
        rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
    }
}
