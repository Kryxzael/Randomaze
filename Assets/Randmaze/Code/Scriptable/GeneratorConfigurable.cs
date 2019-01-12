using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Represents a generator that can have its spawnrates changed
/// </summary>
public abstract class GeneratorConfigurable : Generator
{
    /// <summary>
    /// The information of spawnrates
    /// </summary>
    public List<TileSpawnRateInfo> Spawnrates = new List<TileSpawnRateInfo>();

    /// <summary>
    /// Sets the spawnrate of a tile
    /// </summary>
    /// <param name="tileName"></param>
    /// <param name="spawnrate"></param>
    /// <returns></returns>
    public GeneratorConfigurable SetSpawnrate(string tileName, float spawnrate)
    {
        if (spawnrate > 1f || spawnrate < 0f)
        {
            throw new ArgumentOutOfRangeException("Expected spawnrate between 0-1");
        }

        Spawnrates.RemoveAll(i => i.TileName == tileName);
        if (spawnrate != 0)
        {
            Spawnrates.Add(new TileSpawnRateInfo(tileName, spawnrate));
        }

        return this;
    }

    /// <summary>
    /// Gets the spawn rate for a given tile, the spawnrate will be a number between 0 and 1 inclusive
    /// </summary>
    /// <param name="tile">Tile to get spawnrate of</param>
    /// <returns></returns>
    public float GetSpawnrate(string tile)
    {
        if (Spawnrates.Sum(i => i.Spawnrate) == 0 && tile == "Blank")
        {
            return 1f;
        }

        if (Spawnrates.Any(i => i.TileName == tile))
        {
            return Spawnrates.Single(i => i.TileName == tile).Spawnrate;
        }

        return 0f;
    }

    public override bool Equals(object other)
    {
        if (other is GeneratorConfigurable)
        {
            return (other as GeneratorConfigurable).Spawnrates.SequenceEqual(Spawnrates);
        }

        return false;
    }

    /// <summary>
    /// Holds serializable info about the spawnrate of a tile
    /// </summary>
    [Serializable]
    public struct TileSpawnRateInfo
    {
        /// <summary>
        /// The name of the tile
        /// </summary>
        public string TileName;

        /// <summary>
        /// The spawnrate of the tile
        /// </summary>
        [Range(0f, 1f)]
        public float Spawnrate;

        public TileSpawnRateInfo(string tileName, float spawnrate)
        {
            TileName = tileName;
            Spawnrate = Mathf.Clamp(spawnrate, 0f, 1f);
        }

        /// <summary>
        /// Gets the associated tile
        /// </summary>
        /// <returns></returns>
        public Tile GetTile()
        {
            return Tile.ByName(TileName);
        }

        public override string ToString()
        {
            return TileName + ": " + (Spawnrate * 100) + "%";
        }
    }
}