<details>
<summary><h1>Document List</h1></summary>

[**Project Description** *(README.md)*](README.md#bounce-climber-project-description)

[**Game Manual** *(GAMEMANUAL.md)*](GAMEMANUAL.md#game-manual)

[**To-do List** *(TODOLIST.md)*](TODOLIST.md#to-do-list)

[**Project Plans** *(PROJECTPLANS.md)*](PROJECTPLANS.md#project-plans)

</details>

# Project Plans

## Project diagram using Mermaid Diagrams

My project script structure

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
    }
    class SpawningPlayer{
        +SpawningPlayer(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    class StartGame{
        +StartGame(GameModeController) : base(GameModeController)
        +override IEnumerator Start()
    }
    StateMachine <|-- GameModeController  : Inheritance
    PlayerState <|-- SpawningPlayer  : Inheritance
    GameState <|-- StartGame  : Inheritance
```