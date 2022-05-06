<!DOCTYPE html>
<html>
<head>
<link rel="stylesheet" href="readme.css">
</head>
<body>

# Document list

[**Project Description**](README.md#bounce-climber-project-description)

[**Game Manual**](GAMEMANUAL.md#game-manual)

[**To-do List**](TODOLIST.md#to-do-list)


# Game Manual

<details>
<summary><h1>Game Modes</h1></summary>

## Game Modes

### No Breaks

#### Explanation

In this gamemode you aim to climb upwards while staying in the camera view. Camera keeps accelerating up faster. If you fall behind of the camera/out of view you will lose life/die.

#### Still images

<details>
<summary><font size="10">No Breaks gamemode</font></summary>

![Screenshot](No_Breaks.png "No Breaks gamemode")
</details>

## Objects

### Player

#### Ball

##### Still images
<details>
<summary><font size="10">Ball has a trail and creates dust or wind on contact with a platform</font></summary>

![Screenshot](Ball_Animations.png "Ball Animations")
</details>

### Platforms

##### Still images

<details>
<summary><font size="10">Animation for default platform break</font></summary>

![Screenshot](Platform_Breaking.png "Platform Breaking")
</details>

#### Ice Platform

##### Still images

<details>
<summary><font size="10">Ice Platform</font></summary>

![Screenshot](Ice_platform_and_break.png "Ice Platform")
</details>

##### Animations

<details>
<summary><font size="10">Ice Platform break animation</font></summary>

![GIF](https://j.gifs.com/79z1VG.gif "Ice Platform Breaking")
</details>


#### Grass Platform

##### Still images

<details>
<summary><font size="10">Grass Platform</font></summary>

![Screenshot](Grass_Platform.png "Grass Platform")
</details>

##### Animations

No GIF.

</details>

## Player Mechanics

### Behavior

#### Bouncing

By default player will be always bouncing with a set velocity, to change bounce height player is given vertical movement, but instead of bounce velocity it introduces gravity changes. This way player can levitate a bit or fall down faster.

### Controls

### Abilities

#### Dash ability

Player can dash to a direction by double-tapping left or right (or alternatively tapping shift + directional key). It will reset falling speed on use, and will dash a set distance to left and right and reset horizontal movement at the end of dash. By reset I mean setting it to zero (or maybe some other value in the future).

#### Dive ability

Player is able to dive with great speed downwards to hit into a platform faster than just plainly falling. It is activated by pressing Down-key

#### Charge Jump ability

Player is able to reach greater heights using Charge Jump. It's used by holding down Down-key whlist you are ontop of a surface. It will pause [**Bouncing**](#bouncing) during, and the longer you hold the higher you can jump.


</body>
</html>