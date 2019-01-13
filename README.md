# Randomaze
_A randomly generated tile-maze game_

_Randomaze_ is a puzzle game that generates random tile-based maze-like levels, where the objective is to navigate the player to the exit door. Because of its random nature, every level generated is unique and most likely never-before-seen. As of now, there is no guarantee that a level generated by the game is solvable. Although this might change is the future.

## Tiles
Randomaze is tile-based. Each tile having their own effect on the player. Some tiles are harmful or restrictive, while others might be helpful. The player must therefore plan what path they will use to get to the exit. Randomaze keeps track of your step count, although there is no reward or points system for finding the shortest path. 

### Classic Tiles (All difficulties)
* **Blank tiles/Stone tiles.**
Don't do anything. They are perfectly safe.

* **Lava.**
Instantly kills the player on collision. Do not touch these tiles

* **Fire.**
Sets the player on fire. The player can stay on fire for up to three steps. If the fire is not extinguished by the third step, the player dies. However, water can put fire out.

* **Electrifiers.**
Make the player electric. This effect does not hurt the player by itself, however touching water in this state might be a bad idea.

* **Healers.**
Remove electricity effects, making water touchable again. Although, fire will not be extinguished.

* **Water.**
By default, water does not do anything. However its effect changes based on the player's state. Water can put out fire, making it helpful. However it will kill the player if the player is electrified, making it harmful.

### Extra Tiles (Hard mode and up)
* **Sand.**
Falls and turns into lava when stepped on. Meaning it can only be touched once.

* **Ice.**
Makes the player slide to the next tile. If that tile just so happens to be another ice tile, the player will continue to slide.

* **Acid.**
Acts like lava, killing you on impact. But also has a chance of spreading to adjacent tiles every turn. Spawn rates are usually extremely low.

### Bonus tiles (Custom mode)
* **Blocks (Solid/Non-solid)**
Blocks that acts like walls when active. Their solidity can be toggled by pressing a button

* **Buttons.**
When activated, every block in the level will have its solidity toggled, along with the enabled-state of every button.

* **Conveyors.**
Are solid from one of its sides. Although you are free to drive over it from any other direction, and off of it from any side.

* **Bridges.**
Solid from two sides. Unlike conveyors, these tiles are also solid from the inside, meaning you can't step out of them through their solid sides.


## Difficulties & Presets
Randomaze has four traditional difficulty settings:
* **Easy.**
More blanks and healing spaces. These levels are rarely unsolvable

* **Normal.**
Default difficulty. Has the same spawn rates for every classic tile. These levels are solvable most of the time.

* **Hard.**
Inclusion of ice, sand and acid tiles. Along with more fire and lava. A solution to these levels is in no way guaranteed

* **Extreme.**
No blank spaces. No healing spaces. A solution to these levels is highly improbable

### Ascending difficulty
In _Ascending difficulty_ mode the difficulty of the game increases for every level you complete. The first level is easier that _easy_ difficulty, and the last level is harder that _extreme_ difficulty. There are 30 difficulty levels in total. After this, the same difficulty level is repeated.

### Custom difficulty
The player can choose to customize the spawn rates of each individual tile in _custom difficulty_ mode. There are also presets to choose from that can severely alter the way the game is played. 

Here are just a few of them:
* **Party Mode.**
Enables every tile (except acid), including tiles that are not used in the original difficulties.

* **Electric water.**
Only generates water and electric tiles. You are free to move around these levels, but if you first step on an electric tile, there is no going back into the water.

* **Firestorm.**
Only generates water and fire. You can step through the wall of fire if you know that you can reach a water tile in time. (which is not always the case)

* **Icy.**
Mostly ice. Turning the game into a sliding puzzle.


## Gamemode Options
As of writing this there are three gamemode options that can be enabled. They slightly alter the objective of the game, or the way it's played.

### Key Mode
When key mode is enabled, the level will generate a key, along with the usual exit door. The key must be collected first, before exiting the level.

### Coin Mode
When coin mode is enabled, the level will generate between three and eight key coins. The player must collect all the coins before exiting the level. (Think Mario 64's red coins). Every time a coin is collected, the coin will leave a trail signifying how many coins remain in the level. 

### Key & Coin Mode combined
If both key and coin mode is enabled, the player must collect the coins first, _then_ the key, and finally exit the level.

### Blind Mode
When blind mode is enabled, the player can only see a small portion of the level at a given time. Finishing the level in this state can be challenging

In addition, the player can randomize the settings by enabling the 'Randomized' box. The settings will then be randomized every time a new level is generated.


## Planned features
This is not an active project, so development will be slow and in bursts. However I have some features that I would like to implement at some point

* **Pre-computation** that makes sure that the level is actually solvable before giving it to the player. A lot of other features would be possible as well if this becomes a reality.
* **99-door challenge** is a game mode that was in an earlier version of the game. It would have the player exit 99 doors, all in the same level, in random order. (Every time the player reached an exit, a new one would be spawned, until every spot had been used)
* **Infinite mode** is another game mode from an early version. It would not generate a door, but would insted scroll upwards when the player moved. The point of this mode was to reach as many screen scrolls as possible.