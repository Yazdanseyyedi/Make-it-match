Certainly! I'll update the README to include information about the `BoardEventHandler` class. Here’s the revised README:

---

# Match-3 Game

## Overview

This project is a basic implementation of a match-3 game developed using Unity. The game involves a grid of tiles where the player can swap adjacent tiles to match three or more of the same type in a row or column. Matching tiles are removed from the board, and new tiles fall into place to fill the gaps. The game tracks the player's score and high score and ends when the timer runs out.

## Features

- **Grid-based Gameplay:** A dynamic grid of tiles where players can make matches.
- **Tile Swapping:** Players can swipe to swap adjacent tiles.
- **Match Detection:** Automatically detects and handles tile matches.
- **Score Tracking:** Displays current score and high score.
- **Timer:** Countdown timer that ends the game when it reaches zero.

## Components

### Board

Manages the overall game board, tile placement, and game state.

- **Setup:** Initializes the board with tiles and manages their positions.
- **Tile Matching:** Detects matches and handles tile removal.
- **Score Management:** Updates and displays the score and high score.
- **Game Over:** Handles the end of the game and updates the high score.

### Gem

Represents individual tiles in the game.

- **Swapping:** Handles tile swapping based on player input.
- **Match Detection:** Checks for matches in the board.
- **Movement:** Moves tiles smoothly to their new positions.

### BackgroundTile

Manages the background appearance of each tile on the board.

### BoardData

A ScriptableObject that holds data about the board configuration.

- **Properties:** Width, Height, and Offset of the board.
- **Fields:** Array for storing all gem objects.

### BoardEventHandler

A ScriptableObject that manages events related to game actions.

- **Events:**
  - `DestroyMatches`: Triggered when matches are detected and need to be destroyed.
  - `OnTimerEnd`: Triggered when the timer ends, indicating the end of the game.
  
- **Methods:**
  - `RaiseDestroyMatchesAction()`: Invokes the `DestroyMatches` event.
  - `RaiseOnTimerEndAction()`: Invokes the `OnTimerEnd` event.

## Getting Started

### Prerequisites

- Unity 2020.3 LTS or newer
- DOTween (for tween animations)

### Installation

1. Clone or download this repository:
   ```bash
   git clone https://github.com/yourusername/your-repository.git
   ```

2. Open the project in Unity.

3. Import the DOTween package via the Unity Asset Store or the DOTween website.

4. Set up the board by configuring `BoardData`, `Board`, and `Gem` components in the Unity Editor.

5. Create and configure a `BoardEventHandler` asset to manage game events.

### Usage

1. **Configure the Board:**
   - Attach the `Board` script to an empty GameObject.
   - Assign the `tilePrefab`, `tiles`, `boardData`, `tilesHolder`, `boardEventHandler`, `gameoverBlock`, `scoreText`, `highScoreText`, and `countDownTimer` fields in the Inspector.

2. **Add Gems:**
   - Create and configure the `Gem` prefab with the `Gem` script attached.
   - Define the `Gem` script parameters in the Inspector.

3. **Set Up Event Handling:**
   - Create and configure the `BoardEventHandler` asset.
   - Assign the `DestroyMatches` and `OnTimerEnd` events to their respective methods in the `Board` script.

4. **Play the Game:**
   - Hit the Play button in Unity to start the game.
   - Use the mouse or touch input to swap tiles and make matches.

### Controls

- **Swiping:** Click and drag to swap adjacent tiles.

## Troubleshooting

- **Tiles Not Moving Smoothly:** Ensure you move your mouse enough =).
- **Game Over Not Triggering:** Check the timer setup and ensure `countDownTimer` is correctly assigned.
- **Event Handling Issues:** Verify that the `BoardEventHandler` events are correctly linked to the `Board` script.


## Acknowledgments

- Unity community for various assets and resources.
