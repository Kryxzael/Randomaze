using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class DebugListener : MonoBehaviour
{
    /// <summary>
    /// The key that will be used to toggle debug mode
    /// </summary>
    private const KeyCode KEY_DEBUG_TOGGLE = KeyCode.F10;

    /// <summary>
    /// The key that will toggle the player's fire state
    /// </summary>
    private const KeyCode KEY_FIRE = KeyCode.F;

    /// <summary>
    /// The key that will clear the area of tiles
    /// </summary>
    private const KeyCode KEY_CLEAR_AREA = KeyCode.C;

    /// <summary>
    /// The key that will give the player the key
    /// </summary>
    private const KeyCode KEY_KEY = KeyCode.K;

    /// <summary>
    /// The key that will toggle the player's electric state
    /// </summary>
    private const KeyCode KEY_ELECTRIC = KeyCode.E;

    /// <summary>
    /// The key that will toggle god mode
    /// </summary>
    private const KeyCode KEY_GOD = KeyCode.G;

    /// <summary>
    /// Writes output to the label
    /// </summary>
    private void WriteLine(string text)
    {
        GetComponent<Text>().text = text;
    }

    private void Update()
    {
        //Enabling or disabling debug mode
        if (Input.GetKeyDown(KEY_DEBUG_TOGGLE))
        {
            GameState.DebugMode = !GameState.DebugMode;
            WriteLine("debug: " + GameState.DebugMode);
        }

        if (GameState.DebugMode)
        {
            PlayerState player = GameState.CurrentLevel.Player;

            //Toggle fire
            if (Input.GetKeyDown(KEY_FIRE))
            {
                if (player.OnFire)
                {
                    player.Extinguish();
                }
                else
                {
                    player.SetOnFire();
                    player.TickFire();
                }

                WriteLine("fire: " + player.Fire);
            }

            //Toggle electricity
            if (Input.GetKeyDown(KEY_ELECTRIC))
            {
                player.SetElectric(!player.Electric);
                WriteLine("elec: " + player.Electric); 
            }

            //Giving key
            if (Input.GetKeyDown(KEY_KEY))
            {
                GameState.CurrentLevel.Map.ReplaceTile(Tile.ByName("ExitLocked"), Tile.ByName("Exit"));

                GameState.Events.OnKeyCollected.Invoke();
                WriteLine("key");
            }

            //Clear area
            if (Input.GetKeyDown(KEY_CLEAR_AREA))
            {
                //Clear a 3x3 area around the player's position
                for (int x = player.Position.x - 1; x <= player.Position.x + 1; x++)
                {
                    for (int y = player.Position.y - 1; y <= player.Position.y + 1; y++)
                    {
                        //If the tile is in bounds and is not a special tile, replace it with a blank tile
                        if (GameState.CurrentLevel.Map.IsInBounds(x, y) && !GameState.CurrentLevel.Map.GetTile(x, y).IsSpecialTile())
                        {
                            GameState.CurrentLevel.Map.SetTile(x, y, Tile.ByName("Blank"));
                        }
                    }
                }

                WriteLine("clear " + (player.Position - Vector2Int.one) + ", " + (player.Position + Vector2Int.one));
            }

            //God mode
            if (Input.GetKeyDown(KEY_GOD))
            {
                GameState.GodMode = !GameState.GodMode;
                WriteLine("god: " + GameState.GodMode);
            }
        }
    }
}