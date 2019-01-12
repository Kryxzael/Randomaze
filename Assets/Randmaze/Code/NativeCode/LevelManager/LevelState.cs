using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents the current state of a level
/// </summary>
public class LevelState : ICloneable
{
    /// <summary>
    /// Info about the current map
    /// </summary>
    public LevelMap Map;

    /// <summary>
    /// Info about the current player
    /// </summary>
    public PlayerState Player;

    /// <summary>
    /// Is the current level perfect
    /// </summary>
    public bool Perfect;

    /// <summary>
    /// Has the key been collected?
    /// </summary>
    public bool KeyCollected;

    /// <summary>
    /// How many coins have been collected?
    /// </summary>
    public int CoinsCollected;

    /// <summary>
    /// How many coins are there in total?
    /// </summary>
    public int LevelCoinCount;

    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// Creates an exact duplicate of this level state
    /// </summary>
    /// <returns></returns>
    public LevelState Clone()
    {
        return new LevelState()
        {
            KeyCollected = this.KeyCollected,
            Perfect = this.KeyCollected,
            Player = this.Player.Clone(),
            Map = this.Map.Clone()
        };
    }
}
