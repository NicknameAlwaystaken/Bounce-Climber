using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashing : PlayerState
{
    private Vector3 desiredPosition;
    private Vector3 smootherPosition;
    private float timer;
    public Dashing(PlayerController controller, Player player) : base(controller, player)
    {
    }
    public override IEnumerator Start()
    {
        UserInputSystem.currentStateName = "Dashing";
        StartDash();
        yield break;
    }

    private void StartDash()
    {
        UserInputSystem.Dashing = true;
        UserInputSystem.DashingDone = true;
        UserInputSystem.rb.velocity = Vector3.zero;
        UserInputSystem.transform.GetComponent<Collider>().enabled = false;
        float distance = UserInputSystem.DashingDistance;
        if (UserInputSystem.MovingLeft) distance *= -1;
        desiredPosition = new Vector3
        {
            x = UserInputSystem.transform.position.x + distance,
            y = UserInputSystem.transform.position.y + UserInputSystem.DashingLift,
            z = UserInputSystem.transform.position.z,
        };
        timer = 0f;
    }

    public override IEnumerator Dash()
    {
        UserInputSystem.currentStateName = "Dashing";
        if (UserInputSystem.Dashing)
        {
            timer += Time.deltaTime;
            
            smootherPosition.x = Vector3.Lerp(UserInputSystem.transform.position, desiredPosition, timer / UserInputSystem.dashingHorizontalTimer).x;
            smootherPosition.y = Vector3.Lerp(UserInputSystem.transform.position, desiredPosition, timer / UserInputSystem.dashingVerticalTimer).y;
            
            UserInputSystem.transform.position = smootherPosition;
            if (Mathf.Abs(UserInputSystem.transform.position.x - desiredPosition.x) < UserInputSystem.DashFreeingDistance)
            {
                Stop();
            }
        }
        yield break;
    }

    public override IEnumerator StopDash()
    {
        Stop();
        yield break;
    }

    private void Stop()
    {
        UserInputSystem.Dashing = false;
        UserInputSystem.DashingConditions = false;
        UserInputSystem.transform.GetComponent<Collider>().enabled = true;
        UserInputSystem.rb.velocity = Vector3.zero;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
