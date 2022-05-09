using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    private float currentScore;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIController UIController;
    public Player player;
    public ScoreType scoreType;

    public static GameController instance;

    #region Events

    public event EventHandler<ScoreIncreaseEventArgs> ScoreIncrease;
    public event EventHandler<ScoreResetEventArgs> ScoreReset;
    public event EventHandler<PlayerDiedEventArgs> PlayerDied;
    public event EventHandler<GameStartEventArgs> GameStart;

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
        OnPlayerDying(0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameStart += (sender, args) => SetGameState(new StartGame(this));
        GameStart += (sender, args) => UIController.GameStarted();
        OnStartGame();
    }

    private void GetPlayer()
    {
        player = playerController.GetPlayer();
    }

    public void SpawnPlayer()
    {
        player = playerController.SpawnPlayer();
        ScoreIncrease += (sender, args) => UIController.SetScore(args.Amount);
        PlayerDied += (sender, args) => this.OnResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.SetResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.GameEnded();
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
        if (GameState != null && Input.GetKeyDown(KeyCode.R)) OnStartGame();
        if (scoreType == ScoreType.Height && player != null && currentScore < player.transform.position.y)
        {
            OnScoreIncrease(player.transform.position.y - currentScore);
        }
    }
    public void OnStartGame()
    {
        GameStart?.Invoke(this, new GameStartEventArgs());
    }
    public class GameStartEventArgs : EventArgs
    {

    }
    public void OnPlayerDying(float amount)
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
    public void OnScoreIncrease(float amount)
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
    public void OnResetScore(float amount)
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
