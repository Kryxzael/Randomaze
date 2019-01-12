using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[CreateAssetMenu(menuName = "Generators/Random", fileName = "New Preset")]
public class RandomGenerator : GeneratorConfigurable
{
    /// <summary>
    /// The name that will be used to display this generator in the preset picker
    /// </summary>
    public string DisplayName;

    public override string GetName()
    {
        return DisplayName;
    }

    public override LevelState GenerateLevel()
    {
        //Creates new leveldata
        LevelState newLevel = new LevelState()
        {
            Map = new LevelMap(GameState.Settings.Size.x, GameState.Settings.Size.y, Tile.ByName("Blank"))
        };

        //Fills the level with random tiles
        for (int x = 0; x < GameState.Settings.Size.x; x++)
        {
            for (int y = 0; y < GameState.Settings.Size.y; y++)
            {
                newLevel.Map.SetTile(x, y, GetRandomTile(), silent: true);
            }
        }

        //Places the exit, unless in infinite run
        if (!GameState.Settings.InfiniteRun)
        {
            if (GameState.Settings.KeyMode || GameState.Settings.CoinMode)
            {
                newLevel.Map.SetAtRandomPoint(Tile.ByName("ExitLocked"));
            }
            else
            {
                newLevel.Map.SetAtRandomPoint(Tile.ByName("Exit"));
            }
            
        }

        //Places the key
        if (GameState.Settings.KeyMode && !GameState.Settings.ChallangeMode)
        {
            if (GameState.Settings.CoinMode)
            {
                newLevel.Map.SetAtRandomPoint(Tile.ByName("KeyLocked"));
            }
            else
            {
                newLevel.Map.SetAtRandomPoint(Tile.ByName("Key"));
            }
        }

        //Places coins
        if (GameState.Settings.CoinMode)
        {
            int coinCount = GameState.GeneratorRNG.Next(3, 9);

            for (int i = 0; i < coinCount; i++)
            {
                newLevel.Map.SetAtRandomPoint(Tile.ByName("Coin"));
            }

            newLevel.LevelCoinCount = coinCount;
        }

        //Places the portals
        if (GameState.Settings.GeneratePortals)
        {
            newLevel.Map.SetAtRandomPoint(Tile.ByName("Portal 1"));
            newLevel.Map.SetAtRandomPoint(Tile.ByName("Portal 2"));
        }

        //Spawns the player
        Vector2Int spawnPoint = newLevel.Map.SetAtRandomPoint(Tile.ByName("Spawn"));
        newLevel.Player = new PlayerState(spawnPoint);


        return newLevel;
    }

    /// <summary>
    /// Retrives a random tile with the current generator settings
    /// </summary>
    /// <returns></returns>
    public Tile GetRandomTile()
    {
        if (Spawnrates.Count == 0)
        {
            return Tile.ByName("Blank");
        }

        while (true)
        {
            TileSpawnRateInfo gen = Spawnrates[GameState.GeneratorRNG.Next(Spawnrates.Count)];

            if (GameState.GeneratorRNG.NextDouble() <= gen.Spawnrate)
            {
                Tile t = gen.GetTile();
                if (t == null)
                {
                    throw new NullReferenceException("No such tile with name " + gen.TileName);
                }
                return t.GetTileForGenerator();
            }

        }
    }

    public override Generator Clone()
    {
        RandomGenerator gen = CreateInstance<RandomGenerator>();
        gen.Spawnrates = Spawnrates.ToList();
        gen.DisplayName = DisplayName;
        return gen;
    }
}