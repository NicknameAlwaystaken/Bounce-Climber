using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    private float currentScore;

    public string GameTitle;
    public string GameVersion;

    [SerializeField] private PlayerController playerController;
    [SerializeField] private UIController UIController;
    [SerializeField] private Game Game;
    [SerializeField] private Player Player;
    [SerializeField] private ScoreType scoreType;
    [SerializeField] private GameStatus gameStatus;
    [SerializeField] private float CurrentTimeScale;

    public static GameController instance;

    #region Events

    public event EventHandler<ScoreIncreaseEventArgs> ScoreIncrease;
    public event EventHandler<ScoreResetEventArgs> ScoreReset;
    public event EventHandler<PlayerDiedEventArgs> PlayerDied;
    public event EventHandler<ToMainMenuEventArgs> ToMainMenu;
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
        Resumed = 3,
        MainMenu = 4,
    }

    private void Awake()
    {
        instance = this;
    }

    private void SetGameStatus(GameStatus newStatus)
    {
        gameStatus = newStatus;
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
    public void PlayerDead(GameObject _player)
    {
        DestroyPlayer(_player);
        OnPlayerDying(0f);
    }

    // Start is called before the first frame update
    void Start()
    {
        UIController.SetGameTitle(GameTitle, GameVersion);

        ScoreIncrease += (sender, args) => UIController.SetScore(args.Amount);

        PlayerDied += (sender, args) => OnResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.SetResetScore(args.Amount);
        PlayerDied += (sender, args) => UIController.PlayerDied();
        PlayerDied += (sender, args) => SetGameStatus(GameStatus.Stopped);
        PlayerDied += (sender, args) => SetTimeScale(0f);

        GameEnd += (sender, args) => SetGameStatus(GameStatus.Stopped);
        GameEnd += (sender, args) => SetGameState(new EndGame(this));
        GameEnd += (sender, args) => DestroyAllPlatforms();
        GameEnd += (sender, args) => DestroyIfPlayerFound();


        ToMainMenu += (sender, args) => OnGameEnd();
        ToMainMenu += (sender, args) => SetGameStatus(GameStatus.MainMenu);
        ToMainMenu += (sender, args) => SetGameState(new MainMenu(this));
        ToMainMenu += (sender, args) => UIController.BackToMainMenu();


        GameStart += (sender, args) => SetGameStatus(GameStatus.Started);
        GameStart += (sender, args) => SetTimeScale(1f);
        GameStart += (sender, args) => Game.StartGame();
        GameStart += (sender, args) => SetGameState(new StartGame(this));
        GameStart += (sender, args) => UIController.GameStarted();


        GamePause += (sender, args) => SetGameStatus(GameStatus.Paused);
        GamePause += (sender, args) => SetTimeScale(0f);
        GamePause += (sender, args) => SetGameState(new PauseGame(this));
        GamePause += (sender, args) => UIController.GamePaused();


        GameResume += (sender, args) => SetGameStatus(GameStatus.Resumed);
        GameResume += (sender, args) => SetTimeScale(1f);
        GameResume += (sender, args) => SetGameState(new ResumeGame(this));
        GameResume += (sender, args) => UIController.GameResumed();

        OnToMainMenu();
    }

    private void DestroyIfPlayerFound()
    {
        if (Player != null) Destroy(Player);
    }

    private void DestroyPlayer(GameObject _player)
    {
        if (_player != null) Destroy(_player);
    }

    private void SetTimeScale(float amount)
    {
        CurrentTimeScale = amount;
        Time.timeScale = amount;
    }

    private void GetPlayer()
    {
        Player = playerController.GetPlayer();
    }

    public void SpawnPlayer()
    {
        Player = playerController.SpawnPlayer();
    }
    private void DestroyAllPlatforms()
    {
        var objects = GameObject.FindGameObjectsWithTag("Platform");
        foreach (var obj in objects)
        {
            if (obj != null) Destroy(obj);
        }
    }

    private void Update()
    {
        if(GameState == null) return;
        if (Input.GetKeyDown(KeyCode.R) && gameStatus == GameStatus.Stopped) RestartGame();
        if (Input.GetButtonDown("Cancel") || Input.GetButtonDown("Pause"))
        {
            if (gameStatus == GameStatus.Stopped)
            {
                OnQuitGame();
            }
            else if (gameStatus == GameStatus.Started)
            {
                OnPauseGame();
            }
            else if (gameStatus == GameStatus.Paused)
            {
                OnResumeGame();
            }
            else if (gameStatus == GameStatus.Resumed)
            {
                OnPauseGame();
            }
        }
        if (gameStatus == GameStatus.Stopped) return;

        StartCoroutine(GameState.Update());

        if (scoreType == ScoreType.Height && Player != null && currentScore < Player.transform.position.y)
        {
            OnScoreIncrease(Player.transform.position.y - currentScore);
        }
    }

    public void RestartGame()
    {
        OnGameEnd();
        OnStartGame();
    }

    public void OnQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    #region Eventhandlers

    public void OnToMainMenu()
    {
        ToMainMenu?.Invoke(this, new ToMainMenuEventArgs());
    }
    public class ToMainMenuEventArgs : EventArgs
    {

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

    #endregion

}
