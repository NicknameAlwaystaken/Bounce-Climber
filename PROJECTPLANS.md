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
graph TD;
    GameModeController-->StateMachine;
    PlayerState-->SpawningPlayer;
    GameState-->StartGame;
```