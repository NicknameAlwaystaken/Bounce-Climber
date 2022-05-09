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
        Player.currentStateName = "Dashing";
        Player.Dashing = true;
        Player.DashingDone = true;
        Player.rb.velocity = Vector3.zero;
        Player.transform.GetComponent<Collider>().enabled = false;
        float distance = Player.DashingDistance;
        if (Player.MovingLeft) distance *= -1;
        desiredPosition = new Vector3
        {
            x = Player.transform.position.x + distance,
            y = Player.transform.position.y + Player.DashingLift,
            z = Player.transform.position.z,
        };
        timer = 0f;
        yield break;
    }

    public override IEnumerator Dash()
    {
        Player.currentStateName = "Dashing";
        if (Player.Dashing)
        {
            timer += Time.deltaTime;
            
            smootherPosition.x = Vector3.Lerp(Player.transform.position, desiredPosition, timer / Player.dashingHorizontalTimer).x;
            smootherPosition.y = Vector3.Lerp(Player.transform.position, desiredPosition, timer / Player.dashingVerticalTimer).y;
            
            Player.transform.position = smootherPosition;
            if (Mathf.Abs(Player.transform.position.x - desiredPosition.x) < Player.DashFreeingDistance)
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
        Player.Dashing = false;
        Player.DashingConditions = false;
        Player.transform.GetComponent<Collider>().enabled = true;
        Player.rb.velocity = Vector3.zero;
    }
    public override IEnumerator CurrentState()
    {
        yield return this;
    }
}
