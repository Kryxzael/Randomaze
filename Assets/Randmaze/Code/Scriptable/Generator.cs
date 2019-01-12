using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Holds settings for generating a level
/// </summary>
public abstract class Generator : ScriptableObject, ICloneable
{
    /// <summary>
    /// Retrives the displayname of this generator
    /// </summary>
    /// <returns></returns>
    public abstract string GetName();

    /// <summary>
    /// Generates a new level with this generator
    /// </summary>
    /// <returns></returns>
    public abstract LevelState GenerateLevel();

    /// <summary>
    /// Creates an exact duplicate of this generator
    /// </summary>
    /// <returns></returns>
    public abstract Generator Clone();

    object ICloneable.Clone()
    {
        return Clone();
    }
}
