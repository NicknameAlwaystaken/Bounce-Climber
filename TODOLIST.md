# Document list

[**Project Description**](README.md#bounce-climber-project-description)
[**Game Manual**](GAMEMANUAL.md)
[**To-do List**](TODOLIST.md)


# To-do list


## Game Structure

I will gather here things that are mostly done in the side of creating a skeleton for the game to work with.

- [ ] State machines
    - [ ] **Player**
        - [ ] Basic States
            - [ ] Playing
            - [ ] In Main Menu
            - [ ] Movement
                - [ ] Bouncing
                - [ ] Falling
                - [ ] Jumping

        - [ ] Special States
            - [ ] Static Toggle
            - [ ] Collision Toggle
            - [ ] Showcase for menus
                - [ ] Idle
                - [ ] Bouncing

    - [ ] **Game**
        - [ ] Main Menu
        - [ ] Paused
        - [ ] Playing

- [ ] **Platform changes**
    - [x] Give platforms breakable layers
        - [ ] As a list
        - [ ] Have set their own health

- [ ] **Player changes**
    - [ ] Inflict damage to platforms on contact
        - [ ] Static amount
            - [ ] Eventually upgradeable (lower damage inflicted)
        - [ ] Based on speed on contact

## Game Mechanics

Here I am aiming to use the skeleton of the game

### Game menus

- [ ] **Main Menu**
    - [ ] Play Game Menu
        - [ ] Start Game
        - [ ] Change Gamemode
        - [ ] Back to Main Menu
- [ ] **Pause Menu**
    - [ ] Continue
    - [ ] Settings Menu
    - [ ] To Main Menu

- [ ] **Gameover Menu**
    - [ ] Restart
    - [ ] To Main Menu

- [ ] **Settings Menu**
    - [ ] Game Settings
        - [ ] Ball color change

    - [ ] Sound Settings
        - [ ] Main Volume Slider 0 to 100 (Default 50)

### Minimap

- [ ] Intuitive to read
- [ ] Useful for micro-movements

### Gameplay

#### Player Specific

- [ ] [**Charge jump ability**](#charge-jump-ability)
- [ ] [**Dash ability**](#dash-ability)
- [ ] [**Dive ability**](#dive-ability)

- [ ] Player/Ball deformation
    - [ ] On bounce
    - [ ] On high speeds
    - [ ] Back to original on zero speed
    - [ ] Jump delay based on Falling speed
    - [ ] Deformation amount based on falling speed
    - [ ] Add extra special effects on higher velocities

- [ ] Double-jump
- [x] Icy platform break on contact (from above)

#### Game Specific

- [ ] Give platforms "health" for when they break
    - [ ] Some platforms to never break
    - [ ] Some platforms to have layers, which have their own health
- [ ] Close game loop (Start game --> play --> lose/win --> start game)
- [ ] Cycle of Seasons with distance

## Art

I aim to create very pleasing looking and sounding game with these effects

- [ ] Polishing game with graphics and sounds
    - [ ] Ball deformation
    - [ ] Better splash animation on landing
    - [ ] Better default platform break animation

- [ ] Add more platform types
    - [x] **Winter**
        - [x] Winter platform animations (Ice break and melt)
        - [ ] Unique sound on ice and the platform
        - [x] Unique animation on land (Ice breaking, none for platform inside)

    - [ ] **Spring**

    - [x] **Summer**
        - [ ] Summer platform animations (maybe flowers growing, bees flying)
        - [ ] Unique sound
        - [ ] Unique animation on land

    - [ ] **Autumn**

