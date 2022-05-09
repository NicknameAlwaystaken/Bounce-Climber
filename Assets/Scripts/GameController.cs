using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameController : StateMachine
{
    public PlayerController playerController;

    public static GameController instance;

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

    // Start is called before the first frame update
    void Start()
    {
        SetGameState(new StartGame(this));
    }

    public void SpawnPlayer()
    {
        playerController.SpawnPlayer();
    }
    public void Update()
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
    }
}
