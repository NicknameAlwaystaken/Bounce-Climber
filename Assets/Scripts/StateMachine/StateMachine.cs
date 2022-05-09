using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected GameState GameState;
    protected PlayerState PlayerState;

    public void SetGameState(GameState gameState)
    {
        GameState = gameState;
        StartCoroutine(GameState.Start());
    }
    public void SetPlayerState(PlayerState playerState)
    {
        PlayerState = playerState;
        StartCoroutine(PlayerState.Start());
    }
}
