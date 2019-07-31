using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelMap : ICloneable
{
    private Tile[,] _data;

    /// <summary>
    /// Gets the amount of tiles horizontaly of this map
    /// </summary>
    public int Width
    {
        get
        {
            return Size.x;
        }
    }

    /// <summary>
    /// Gets the amount of tiles verticaly of this map
    /// </summary>
    public int Height
    {
        get
        {
            return Size.y;
        }
    }

    /// <summary>
    /// Gets the size of the map in tiles
    /// </summary>
    public readonly Vector2Int Size;

    /// <summary>
    /// Creates a new level
    /// </summary>
    /// <param name="w">Width of map</param>
    /// <param name="h">Height of map</param>
    /// <param name="initializer">Tile that will initialy fill the map</param>
    public LevelMap(int w, int h, Tile initializer)
    {
        Size = new Vector2Int(w, h);
        _data = new Tile[w, h];

        for (int x = 0; x < w; x++)
        {
            for (int y = 0; y < h; y++)
            {
                _data[x, y] = initializer;
            }
        }
    }

    /// <summary>
    /// Gets the tile at the given coordinates
    /// </summary>
    /// <param name="x">X-coord</param>
    /// <param name="y">Y-coord</param>
    /// <returns></returns>
    public Tile GetTile(int x, int y)
    {
        if (!IsInBounds(x, y))
        {
            return null;
        }

        return _data[x, y];
    }

    /// <summary>
    /// Overwrites a tile with a new tile
    /// </summary>
    /// <param name="x">X-coord</param>
    /// <param name="y">Y-coord</param>
    /// <param name="tile">Tile to place</param>
    /// <param name="silent">If true, the OnTileUpdated evnet will not be fired</param>
    public void SetTile(int x, int y, Tile tile, bool silent = false)
    {
        if (!IsInBounds(x, y))
        {
            Debug.LogWarning("Attempted to place tile outside of map bounderies");
            return;
        }

        if (tile == null)
        {
            throw new NullReferenceException("Attempted to palce null tile");
        }

        _data[x, y] = tile;

        if (!silent)
        {
            GameState.Events.OnTileUpdated.Invoke(new Vector2Int(x, y));

            //Updates neighbours. This is used to update dynamic tiles of sand
            if (IsInBounds(x - 1, y)) GameState.Events.OnNeighbourTileUpdated.Invoke(new Vector2Int(x - 1, y));
            if (IsInBounds(x + 1, y)) GameState.Events.OnNeighbourTileUpdated.Invoke(new Vector2Int(x + 1, y));
            if (IsInBounds(x, y - 1)) GameState.Events.OnNeighbourTileUpdated.Invoke(new Vector2Int(x, y - 1));
            if (IsInBounds(x, y + 1)) GameState.Events.OnNeighbourTileUpdated.Invoke(new Vector2Int(x, y + 1));
        }
        

    }

    /// <summary>
    /// Finds the first instance of a tile
    /// </summary>
    /// <returns></returns>
    public Vector2Int FindTile(Tile tile)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                if (_data[x, y] == tile)
                {
                    return new Vector2Int(x, y);
                }
            }
        }

        return new Vector2Int(-1, -1);
    }

    /// <summary>
    /// Replaces the first instance of the source tile with the target tile
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    public void ReplaceTile(Tile source, Tile target)
    {
        Vector2Int pnt = FindTile(source);
        _data[pnt.x, pnt.y] = target;
        GameState.Events.OnTileUpdated.Invoke(pnt);
    }

    /// <summary>
    /// Places a single tile at a random position, not overwriting special tiles. This method is allways silent
    /// </summary>
    /// <param name="tile">Tile to place</param>
    /// <returns></returns>
    public Vector2Int SetAtRandomPoint(Tile tile)
    {
        int x, y;
        do
        {
            x = GameState.GeneratorRNG.Next(Width);
            y = GameState.GeneratorRNG.Next(Height);

        } while (GetTile(x, y).IsSpecialTile());

        SetTile(x, y, tile, silent: true);
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// Are the given coordinates a valid in-bounds position
    /// </summary>
    /// <param name="x">X-coord</param>
    /// <param name="y">Y-coord</param>
    /// <returns></returns>
    public bool IsInBounds(int x, int y)
    {
        return x >= 0 && x < Width && y >= 0 && y < Height;
    }

    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Creates an exact duplicate of this map
    /// </summary>
    /// <returns></returns>
    public LevelMap Clone()
    {
        LevelMap _ = new LevelMap(Width, Height, Tile.ByName("Blank"))
        {
            _data = this._data.Clone() as Tile[,]
        };

        return _;
    }
}