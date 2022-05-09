using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    private float currentScore;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIController UIController;
    [SerializeField] private Player player;
    [SerializeField] private ScoreType scoreType;
    [SerializeField] private GameStatus gameStatus;

    public static GameController instance;

    #region Events

    public event EventHandler<ScoreIncreaseEventArgs> ScoreIncrease;
    public event EventHandler<ScoreResetEventArgs> ScoreReset;
    public event EventHandler<PlayerDiedEventArgs> PlayerDied;
    public event EventHandler<GameStartEventArgs> GameStart;
    public event EventHandler<GamePauseEventArgs> GamePause;
    public event EventHandler<GameResumeEventArgs> GameResume;
    public event EventHandler<GameEndEventArgs> GameEnd;

    #endregion

    public enum ScoreType
    {
        Height = 0,
    }

    public enum GameStatus
    {
        Stopped = 0,
        Started = 1,
        Paused = 2,
        Unpaused = 3,
    }

    private void Awake()
    {
        instance = this;
    }

    private void SetGameStatus(int newStatus)
    {
        gameStatus = (GameStatus)newStatus;
    }
    public GameStatus GetGameStatus()
    {
        return gameStatus;
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
        ScoreIncrease += (sender, args) => UIController.SetScore(args.Amount);

        PlayerDied += (sender, args) => this.OnResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.SetResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.GameEnded();

        GameEnd += (sender, args) => SetGameState(new EndGame(this));
        GameEnd += (sender, args) => UIController.GameEnded();
        GameEnd += (sender, args) => SetGameStatus(0);

        GameStart += (sender, args) => SetGameState(new StartGame(this));
        GameStart += (sender, args) => UIController.GameStarted();
        GameStart += (sender, args) => SetGameStatus(1);

        GamePause += (sender, args) => SetGameState(new PauseGame(this));
        GamePause += (sender, args) => UIController.GamePaused();
        GamePause += (sender, args) => SetGameStatus(2);

        GameResume += (sender, args) => SetGameState(new ResumeGame(this));
        GameResume += (sender, args) => UIController.GameResumed();
        GameResume += (sender, args) => SetGameStatus(3);

        SetGameStatus(0);
    }

    private void GetPlayer()
    {
        player = playerController.GetPlayer();
    }

    public void SpawnPlayer()
    {
        player = playerController.SpawnPlayer();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause"))
        {
            if(GameState != null)
            {
                if (gameStatus == GameStatus.Stopped)
                {
                    OnQuitGame();
                }
                if (gameStatus == GameStatus.Started)
                {
                    OnPauseGame();
                }
                if (gameStatus == GameStatus.Paused)
                {
                    OnResumeGame();
                }
                if (gameStatus == GameStatus.Unpaused)
                {
                    OnPauseGame();
                }
            }
        }
        if (gameStatus == GameStatus.Stopped) return;
        if (GameState != null) StartCoroutine(GameState.Update());
        if (GameState != null && Input.GetKeyDown(KeyCode.R)) OnStartGame();
        if (scoreType == ScoreType.Height && player != null && currentScore < player.transform.position.y)
        {
            OnScoreIncrease(player.transform.position.y - currentScore);
        }
    }

    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
    public void OnResumeGame()
    {
        GameResume?.Invoke(this, new GameResumeEventArgs());
    }
    public class GameResumeEventArgs : EventArgs
    {

    }
    public void OnPauseGame()
    {
        GamePause?.Invoke(this, new GamePauseEventArgs());
    }
    public class GamePauseEventArgs : EventArgs
    {

    }
    public void OnGameEnd()
    {
        GameEnd?.Invoke(this, new GameEndEventArgs());
    }
    public class GameEndEventArgs : EventArgs
    {

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
