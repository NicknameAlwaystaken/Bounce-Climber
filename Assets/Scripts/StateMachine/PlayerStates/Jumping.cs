using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : PlayerState
{
    public Jumping(PlayerController controller, Player player) : base(controller, player)
    {
        UserInputSystem.currentStateName = "Jumping";
        UserInputSystem.DoubleJumpingConditions = false;
        JumpWithEffects("Jumping", UserInputSystem.BounceVelocity * UserInputSystem.FirstJumpIncrement);
        UserInputSystem.JumpingDone = true;
    }
    public override IEnumerator Start()
    {
        yield break;
    }
    private void JumpWithEffects(string stateName, float bounceVelocity)
    {
        UserInputSystem.currentStateName = stateName;
        UserInputSystem.AudioSource.Play();
        Object.Instantiate((ParticleSystem)UserInputSystem.particles, UserInputSystem.transform.position, new Quaternion());
        Rigidbody rb = UserInputSystem.GetComponent<Rigidbody>();
        Vector3 upVelocity = Vector3.up * bounceVelocity;
        if (rb != null) rb.velocity = new Vector3(rb.velocity.x, upVelocity.y);
        UserInputSystem.Jumping = false;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}