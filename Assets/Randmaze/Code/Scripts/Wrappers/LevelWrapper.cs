using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Wraps the Levelmap class, and displays it on screen
/// </summary>
public class LevelWrapper : MonoBehaviour
{
    [Tooltip("The prefab that will be used to generate tiles")]
    public TileWrapper TilePrefab;

    [Tooltip("The prefab that will be used to generate tiles")]
    public PlayerWrapper PlayerPrefab;

    private void Start()
    {
        //Loads the init scene if the game has not been initialized
        if (!GameState.Initialized)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            return;
        }

        GameState.Events.OnLevelStart.AddListener(OnLevelEnter);
        GameState.Events.OnLevelEnd.AddListener(OnLevelEnd);
        GameState.Events.OnTileUpdated.AddListener(OnTileUpdated);
        GameState.Events.OnPlayerMoved.AddListener(SpreadAcid);

        //DEBUG
        GameState.StartNewGame();
    }

    /// <summary>
    /// Spreads the acid tiles in the level. This is done every time the player moves
    /// </summary>
    /// <param name="pl"></param>
    /// <param name="oldPos"></param>
    private void SpreadAcid(PlayerState pl, Vector2Int oldPos)
    {
        LevelMap clone = GameState.CurrentLevel.Map.Clone();

        for (int x = 0; x < clone.Width; x++)
        {
            for (int y = 0; y < GameState.CurrentLevel.Map.Height; y++)
            {
                //If an acid tile is found, and the acid is not at the position of the player
                if (clone.GetTile(x, y).GetName() == "Acid")
                {
                    //Chance of acid spreading = 1 / 2^(Steps), per tile
                    if (GameState.GeneralRNG.NextDouble() <= Math.Max(1f / pl.Steps.Count / 2f, 0.1f))
                    {
                        int giveUpCounter = 0;
                        while (giveUpCounter++ < 10) //After ten failed placement attempts, the placement method gives up and doesn't place a tile
                        {
                            //Randomly picks an adjacent coordinate
                            int ax = GameState.GeneralRNG.Next(3) - 1;
                            int ay = GameState.GeneralRNG.Next(3) - 1;

                            //The following conditions must be fufilled for acid to spread
                            if (GameState.CurrentLevel.Map.IsInBounds(x + ax, y + ay) && //The target position must be in bounds
                                GameState.CurrentLevel.Map.GetTile(x + ax, y + ay).GetName() != "Acid" && //the target position must not be another acid tile, this includes trying to overwrite itself
                                !GameState.CurrentLevel.Map.GetTile(x + ax, y + ay).IsSpecialTile() && //The target position must not hold a special tile like a door or key
                                new Vector2Int(x, y) != pl.Position) //The player cannot be standing at the position of the target
                            {
                                GameState.CurrentLevel.Map.SetTile(x + ax, y + ay, Tile.ByName("Acid"));
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnLevelEnter()
    {
        for (int x = 0; x < GameState.CurrentLevel.Map.Width; x++)
        {
            for (int y = 0; y < GameState.CurrentLevel.Map.Height; y++)
            {
                TileWrapper t = Instantiate(TilePrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
                t.GridPosition = new Vector2Int(x, y);
                t.UpdateTile();
            }
        }

        //Instantiates the player
        PlayerWrapper p = Instantiate(PlayerPrefab, new Vector3(GameState.CurrentLevel.Player.Position.x, GameState.CurrentLevel.Player.Position.y, 0), Quaternion.identity, transform);
        p.Player = GameState.CurrentLevel.Player;
        p.PlayerAnimationName = "Player1";

        UIManager.SetTitles("Level %l".Replace("%l", GameState.History.Level.ToString()), "Go!", Color.blue, Color.blue);
    }

    private void OnLevelEnd()
    {
        transform.GetChildren().ForEach(i => Destroy(i.gameObject));
    }

    private void OnTileUpdated(Vector2Int pos)
    {
        GetTileWrapper(pos).UpdateTile();
    }

    /// <summary>
    /// Retrives a TileWrapper component that coresponds with a map position
    /// </summary>
    /// <param name="x">X-coord</param>
    /// <param name="y">Y-coord</param>
    /// <returns></returns>
    public TileWrapper GetTileWrapper(Vector2Int pos)
    {
        return GetComponentsInChildren<TileWrapper>().Single(i => i.GridPosition == pos);
    }
}
