using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected GameState GameState;
    protected PlayerState PlayerState;

    public void SetGameState(GameState state)
    {
        GameState = state;
        StartCoroutine(GameState.Start());
    }
    public void SetPlayerState(PlayerState state)
    {
        PlayerState = state;
        StartCoroutine(PlayerState.Start());
    }
    public virtual void Update()
    {
        GameState.Update();
        PlayerState.Update();
    }
}
