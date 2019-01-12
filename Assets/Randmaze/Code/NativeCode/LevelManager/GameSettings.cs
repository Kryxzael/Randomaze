using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Holds the current settings
/// </summary>
public class GameSettings
{
    private ResourceSet _rs = new ResourceSet("Modern", null);
    private bool _key, _blind, _inf, _challange, _genPortals, _coin, _randomized;
    private bool _seedRandom = true;
    private Vector2Int _size = new Vector2Int(10, 10);
    private int _seed;
    private Generator _gen;

    /// <summary>
    /// The set of sprites and audio that will be used.
    /// </summary>
    public ResourceSet ResourceSet
    {
        get
        {
            return _rs;
        }
        set
        {
            if (_rs == null)
            {
                throw new NullReferenceException();
            }
            _rs = value;
        }
    }

    /// <summary>
    /// Is key-mode enabled
    /// </summary>
    public bool KeyMode
    {
        get
        {
            return _key;
        }
        set
        {
            _key = value;
        }
    }

    /// <summary>
    /// Is coin mode enabled
    /// </summary>
    public bool CoinMode
    {
        get
        {
            return _coin;
        }
        set
        {
            _coin = value;
        }
    }

    /// <summary>
    /// Is blind mode enabled
    /// </summary>
    public bool BlindMode
    {
        get
        {
            return _blind;
        }
        set
        {
            _blind = value;
        }
    }

    /// <summary>
    /// Is infinite run enabled
    /// </summary>
    public bool InfiniteRun
    {
        get
        {
            return _inf;
        }
        set
        {
            _inf = value;
        }
    }

    /// <summary>
    /// Is the 99-door challange mode enabled
    /// </summary>
    public bool ChallangeMode
    {
        get
        {
            return _challange;
        }
        set
        {
            _challange = value;
        }
    }

    /// <summary>
    /// Will portals be generated?
    /// </summary>
    public bool GeneratePortals
    {
        get
        {
            return _genPortals;
        }
        set
        {
            _genPortals = value;
        }
    }

    /// <summary>
    /// Will the settings be randomized every level?
    /// </summary>
    public bool RandomizedGamemode
    {
        get
        {
            return _randomized;
        }
        set
        {
            _randomized = value;
        }
    }

    /// <summary>
    /// The size the level will have
    /// </summary>
    public Vector2Int Size
    {
        get
        {
            return _size;
        }
        set
        {
            _size = value;
        }
    }

    /// <summary>
    /// The seed that will be used to seed the random number generator
    /// </summary>
    public int Seed
    {
        get
        {
            return _seed;
        }
        set
        {
            _seed = value;
        }
    }

    /// <summary>
    /// If true, a new seed will be generated at random when a new game session starts
    /// </summary>
    public bool GenerateRandomSeed
    {
        get
        {
            return _seedRandom;
        }
        set
        {
            _seedRandom = value;
        }
    }

    /// <summary>
    /// The settins that will be used to generate the tile map
    /// </summary>
    public Generator Generator
    {
        get
        {
            return _gen;
        }
        set
        {
            if (value == null)
            {
                throw new NullReferenceException();
            }

            _gen = value;
        }
    }


}