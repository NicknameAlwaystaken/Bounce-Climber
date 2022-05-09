using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    private float currentScore;

    public PlayerController playerController;
    public UIController UIController;
    public Player player;
    public ScoreType scoreType;

    public static GameController instance;

    #region Events

    public event EventHandler<ScoreIncreaseEventArgs> ScoreIncrease;
    public event EventHandler<ScoreResetEventArgs> ScoreReset;
    public event EventHandler<PlayerDiedEventArgs> PlayerDied;

    #endregion

    public enum ScoreType
    {
        Height = 0,
    }

    private void Awake()
    {
        instance = this;
    }

    public PlayerController GetPlayerController()
    {
        return playerController;
    }
    public void DestroyBreakable(GameObject platformToDestroy)
    {
        platformToDestroy.GetComponent<Platform>().DestroyBreakable();
    }
    public void PlayerDead()
    {
        PlayerDying(0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(new StartGame(this));
    }

    private void GetPlayer()
    {
        player = playerController.GetPlayer();
    }

    public void SpawnPlayer()
    {
        player = playerController.SpawnPlayer();
        ScoreIncrease += (sender, args) => UIController.SetScore(args.Amount);
        PlayerDied += (sender, args) => this.ResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.ResetScore(args.Amount);
        PlayerDied += (sender, args) => this.SpawnPlayer();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
        if (GameState != null) StartCoroutine(GameState.Update());
        if(scoreType == ScoreType.Height && currentScore < player.transform.position.y)
        {
            AddScore(player.transform.position.y - currentScore);
        }
    }
    public void PlayerDying(float amount)
    {
        PlayerDied?.Invoke(this, new PlayerDiedEventArgs(amount));

        currentScore = amount;
    }

    public class PlayerDiedEventArgs : EventArgs
    {
        public PlayerDiedEventArgs(float amount)
        {
            Amount = amount;
        }

        public float Amount { get; private set; }
    }
    public void AddScore(float amount)
    {
        ScoreIncrease?.Invoke(this, new ScoreIncreaseEventArgs(amount));

        currentScore += amount;
    }

    public class ScoreIncreaseEventArgs : EventArgs
    {
        public ScoreIncreaseEventArgs(float amount)
        {
            Amount = amount;
        }

        public float Amount { get; private set; }
    }
    public void ResetScore(float amount)
    {
        ScoreReset?.Invoke(this, new ScoreResetEventArgs(amount));
    }

    public class ScoreResetEventArgs : EventArgs
    {
        public ScoreResetEventArgs(float amount)
        {
            Amount = amount;
        }

        public float Amount { get; private set; }
    }
}
