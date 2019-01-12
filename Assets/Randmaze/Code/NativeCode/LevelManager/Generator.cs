using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents spawnrates of map
/// </summary>
[Obsolete]
public class GeneratorOld
{
    private Dictionary<Tile, float> _data = new Dictionary<Tile, float>();

    /// <summary>
    /// Sets the spawnrate of a tile in this generator
    /// </summary>
    /// <param name="tile">Tile to set spawnrate of </param>
    /// <param name="spawnrate">Spawnrate of the tile</param>
    /// <returns></returns>
    public GeneratorOld SetSpawnrate(string tileName, float spawnrate)
    {
        Tile tile = Tile.ByName(tileName);

        if (spawnrate > 1f || spawnrate < 0f)
        {
            throw new ArgumentOutOfRangeException("Expected spawnrate between 0-1");
        }

        if (tile == null)
        {
            throw new ArgumentNullException("Null tile not expected");
        }

        if (spawnrate == 0)
        {
            _data.Remove(tile);
        }
        else
        {
            _data[tile] = spawnrate;
        }

        return this;
    }

    /// <summary>
    /// Gets the spawn rate for a given tile, the spawnrate will be a number between 0 and 1
    /// </summary>
    /// <param name="tile">Tile to get spawnrate of</param>
    /// <returns></returns>
    public float GetSpawnrate(Tile tile)
    {
        if (_data.Values.Sum() == 0)
        {
            throw new InvalidOperationException("This generator configuration is invalid because all spawnrates are set to 0 or undefined");
        }

        if (_data.ContainsKey(tile))
        {
            return _data[tile];
        }

        return 0f;
    }

    /// <summary>
    /// Retrives a random tile with the current generator settings
    /// </summary>
    /// <returns></returns>
    public Tile GetRandomTile()
    {
        while (true)
        {
            KeyValuePair<Tile, float> gen = _data.ElementAt(GameState.GeneratorRNG.Next(_data.Count));

            if (GameState.GeneratorRNG.NextDouble() <= gen.Value)
            {
                return gen.Key;
            }

        }
    }
}