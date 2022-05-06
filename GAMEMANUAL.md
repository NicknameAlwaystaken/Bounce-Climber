<details>
<summary><h1>Document List</h1></summary>

[**Project Description** *(README.md)*](README.md#bounce-climber-project-description)

[**Game Manual** *(GAMEMANUAL.md)*](GAMEMANUAL.md#game-manual)

[**To-do List** *(TODOLIST.md)*](TODOLIST.md#to-do-list)

</details>

# Game Manual

<details>
<summary><h2>Game Modes</h2></summary>

### No Breaks

-   #### Explanation
    - In this gamemode you aim to climb upwards while staying in the camera view. Camera keeps accelerating up faster. If you fall behind of the camera/out of view you will lose life/die.

-   #### Still images

    - <details><summary>No Breaks gamemode</summary><img src="No_Breaks.png" alt="No Breaks gamemode"></details>

## Objects

### Player

-   #### Ball

    - ##### Still images
        - <details><summary><h6>Ball has a trail and creates dust or wind on contact with a platform</h6></summary><img src="Ball_Animations.png" alt="Ball Animations"></details>

### Platforms

-   #### Still images
    - <details><summary><h5>Animation for default platform break</h5></summary><img src="Platform_Breaking.png" alt="Platform Breaking"></details>

-   #### Ice Platform

    - ##### Still images
        - <details><summary><h6>Ball has a trail and creates dust or wind on contact with a platform</h6></summary><img src="Ice_platform_and_break.png" alt="Ice Platform"></details>

    - ##### Animations
        - <details><summary><h6>Ball has a trail and creates dust or wind on contact with a platform</h6></summary><img src="https://j.gifs.com/79z1VG.gif" alt="Ice Platform Breaking"></details>

-   #### Grass Platform

    - ##### Still images
        - <details><summary><h6>Ball has a trail and creates dust or wind on contact with a platform</h6></summary><img src="Grass_Platform.png" alt="Grass Platform"></details>

    - ##### Animations

        - No GIF yet.

</details>


<details>
<summary><h2>Player Mechanics</h2></summary>

### Behavior

-   #### Bouncing

    - By default player will be always bouncing with a set velocity, to change bounce height player is given vertical movement, but instead of bounce velocity it introduces gravity changes. This way player can levitate a bit or fall down faster.

### Controls

-   None documented yet.

### Abilities

-   #### Dash ability

    - Player can dash to a direction by double-tapping left or right (or alternatively tapping shift + directional key). It will reset falling speed on use, and will dash a set distance to left and right and reset horizontal movement at the end of dash. By reset I mean setting it to zero (or maybe some other value in the future).

-   #### Dive ability

    - Player is able to dive with great speed downwards to hit into a platform faster than just plainly falling. It is activated by pressing Down-key

-   #### Charge Jump ability

    - Player is able to reach greater heights using Charge Jump. It's used by holding down Down-key whlist you are ontop of a surface. It will pause [**Bouncing**](#bouncing) during, and the longer you hold the higher you can jump.

</details>