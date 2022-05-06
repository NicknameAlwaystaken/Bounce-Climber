<details>
<summary><h1>Document List</h1></summary>

[**Project Description** *(README.md)*](README.md#bounce-climber-project-description)

[**Game Manual** *(GAMEMANUAL.md)*](GAMEMANUAL.md#game-manual)

[**To-do List** *(TODOLIST.md)*](TODOLIST.md#to-do-list)

[**Project Plans** *(PROJECTPLANS.md)*](PROJECTPLANS.md#project-plans)

</details>

# Project Plans

## Project diagram using Mermaid Diagrams

- ### Class Diagram

    - Diagram where it's showing structure and dependancies of classes.

```mermaid
classDiagram
    class StateMachine ~MonoBehaviour~{
        <<abstract>>
        #GameState GameState
        #PlayerState PlayerState
        +SetGameState()
        +SetPlayerState()
    }
    class GameModeController{
        +GameObject player
        +Vector3 spawnPoint
    }
    class GameState{
        #GameModeController GameModeController

        #GameState(GameModeController)

        +virtual IEnumerator Start()
    }
    class PlayerState{
        #GameModeController GameModeController

        #PlayerState(GameModeController)

        +virtual IEnumerator Start()
        +virtual IEnumerator Exit()
    }
    class StartGame{
        +StartGame(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    class SpawningPlayer{
        +SpawningPlayer(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    class Bouncing{
        +Bouncing(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    class Jumping{
        +Jumping(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    StateMachine <|-- GameModeController  : Inheritance
    PlayerState <|-- SpawningPlayer  : Inheritance
    GameState <|-- StartGame  : Inheritance
    GameModeController <.. StartGame  : Dependency
    GameModeController <.. SpawningPlayer  : Dependency
    GameModeController <.. Bouncing  : Dependency
    GameModeController <.. Jumping  : Dependency
```

- ### Sequence Diagram

    - Diagram where it's showing interactions between classes, it goes from starting the game to spawning ball and at the end showing loop how the ball behaves.

```mermaid
sequenceDiagram
    autonumber
    participant GameModeController
    participant GameState
    participant PlayerState
    GameModeController->>GameState: SetGameState();
    GameState->>StartGame: Start()
    StartGame->>GameModeController: SetPlayerState(new SpawningPlayer(GameModeController))
    GameModeController->>PlayerState: StartCoroutine(new SpawningPlayer())
    PlayerState->>SpawningPlayer: Start()
    SpawningPlayer->>GameModeController: GameModeController.player = Object.Instantiate()
    SpawningPlayer->>PlayerControls: Object.Instantiate()
    SpawningPlayer->>GameModeController: SetPlayerState(new Bouncing())
    GameModeController->>PlayerState: SetPlayerState();
    GameState->>Bouncing: Start()
    %% Aim to end autonumber here
    loop OnCollisionEnter() with tag "Ground"/"Platform"
        PlayerControls->>GameModeController: SetPlayerState(new Jumping())
        GameModeController->>PlayerState: StartCoroutine(new Jump())
        PlayerState->>Jumping: Start()
        Jumping->>GameModeController: SetPlayerState(new Bouncing())
        GameModeController->>PlayerState: SetGameState();
        GameState->>Bouncing: Start()
    end
```