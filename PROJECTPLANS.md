<details>
<summary><h1>Document List</h1></summary>

[**Project Description** *(README.md)*](README.md#bounce-climber-project-description)

[**Game Manual** *(GAMEMANUAL.md)*](GAMEMANUAL.md#game-manual)

[**To-do List** *(TODOLIST.md)*](TODOLIST.md#to-do-list)

[**Project Plans** *(PROJECTPLANS.md)*](PROJECTPLANS.md#to-do-list)

</details>

# Project Plans

## Project diagram using Mermaid Diagrams

My project script structure

```mermaid
 classDiagram
      StateMachine <|-- GameModeController
      class StateMachine
      {
          %% #GameState GameState
          %% #PlayerState PlayerState
          +SetGameState()
          +SetPlayerState()
      }
      class GameModeController
      {
          +GameObject player
          +Vector3 spawnPoint
      }
      class GameState{
          #GameModeController GameModeController

          #GameState(GameModeController)

          +IEnumerator Start()
      }
      class PlayerState{
          #GameModeController GameModeController

          #PlayerState(GameModeController)

          +IEnumerator Start()
      }
```