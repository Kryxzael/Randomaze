using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents a tile
/// </summary>
public abstract class Tile
{
    /// <summary>
    /// Holds a list of all the tiles that can be grabbed by name
    /// </summary>
    public static readonly List<Tile> AllTiles = new List<Tile>()
    {
        new Tiles.Blank(),
        new Tiles.Exit(),
        new Tiles.Fire(),
        new Tiles.Key(),
        new Tiles.Lava(),
        new Tiles.Electric(),
        new Tiles.Healer(),
        new Tiles.Portal("Portal 1"),
        new Tiles.Portal("Portal 2"),
        new Tiles.Spawn(),
        new Tiles.Water(),
        new Tiles.Sand(),
        new Tiles.Quicksand(),
        new Tiles.Ice(),
        new Tiles.Acid(),
        new Tiles.Block(),
        new Tiles.BlockUnsolid(),
        new Tiles.Button(),
        new Tiles.ButtonOff(),
        new Tiles.ConveyorEditor(),
        new Tiles.Conveyor(Direction.Up),
        new Tiles.Conveyor(Direction.Down),
        new Tiles.Conveyor(Direction.Left),
        new Tiles.Conveyor(Direction.Right),
        new Tiles.BridgeHorizontal(),
        new Tiles.BridgeVertical(),
        new Tiles.Coin(),
        new Tiles.CoinCollected(0),
        new Tiles.CoinCollected(1),
        new Tiles.CoinCollected(2),
        new Tiles.CoinCollected(3),
        new Tiles.CoinCollected(4),
        new Tiles.CoinCollected(5),
        new Tiles.CoinCollected(6),
        new Tiles.CoinCollected(7),
        new Tiles.CoinCollected(8),
        new Tiles.KeyLocked(),
        new Tiles.KeyCollected(),
        new Tiles.ExitLocked(),
    };

    /// <summary>
    /// Retrives a tile from AllTiles by name
    /// </summary>
    /// <param name="name">Name of tile to get </param>
    /// <returns></returns>
    public static Tile ByName(string name)
    {
        return AllTiles.SingleOrDefault(i => i.GetName().ToUpper() == name.ToUpper());
    }


    /// <summary>
    /// If this functions returns true, it will not be overwritten durring generation
    /// </summary>
    /// <returns></returns>
    public virtual bool IsSpecialTile()
    {
        return false;
    }

    /// <summary>
    /// Will this tile be shown in a custom difficulty settings pane
    /// </summary>
    /// <returns></returns>
    public virtual bool ShowInCustomInspector()
    {
        return !IsSpecialTile();
    }

    /// <summary>
    /// Returns the tile that the generator will use when placing the tile in the world. By default this just returns 'this'
    /// </summary>
    /// <returns></returns>
    public virtual Tile GetTileForGenerator()
    {
        return this;
    }

    /// <summary>
    /// Is this tile unpassable from the specific side "side"
    /// </summary>
    /// <param name="side">Side to check solidness off</param>
    /// <returns></returns>
    public virtual bool IsSolidFrom(Direction side)
    {
        return false;
    }

    /// <summary>
    /// If this tile is solid, will it also be solid from the inside?
    /// </summary>
    /// <returns></returns>
    public virtual bool SolidTwoSided()
    {
        return false;
    }

    /// <summary>
    /// Gets the name of the tile
    /// </summary>
    /// <returns></returns>
    public abstract string GetName();

    /// <summary>
    /// Gets the name of the tile's sprite animation
    /// </summary>
    /// <returns></returns>
    public abstract string GetSpriteAnimationName();

    /// <summary>
    /// What happens when the player steps on this tile
    /// </summary>
    /// <param name="player"></param>
    public abstract void OnStep(PlayerState player);
}