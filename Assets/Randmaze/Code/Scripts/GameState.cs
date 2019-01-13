using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Provides general information about the game's current state
/// </summary>
public class GameState : MonoBehaviour
{
    private static GameState _this;
    private LevelState _currentLevel, _currentLevelReset;
    private GameplayStaus _gameplayStatus;
    private GameSettings _settings;
    private LevelHistory _log = new LevelHistory();
    private System.Random _rng, _rng2;
    private GameEvents _ge = new GameEvents();
    private bool _debug = false;
    private bool _god = false;

    /// <summary>
    /// The current level
    /// </summary>
    public static LevelState CurrentLevel
    {
        get
        {
            return _this._currentLevel;
        }
    }

    /// <summary>
    /// Is the game currently running, in menu or on pause screen
    /// </summary>
    public static GameplayStaus GameplayStatus
    {
        get
        {
            return _this._gameplayStatus;
        }
    }

    /// <summary>
    /// Gets the current settings object for the game session
    /// </summary>
    public static GameSettings Settings
    {
        get
        {
            return _this._settings;
        }
    }

    /// <summary>
    /// The random number generator that will be used for level generation
    /// </summary>
    public static System.Random GeneratorRNG
    {
        get
        {
            return _this._rng;
        }
    }

    /// <summary>
    /// The random number generator that can be used for general purposes
    /// </summary>
    public static System.Random GeneralRNG
    {
        get
        {
            return _this._rng2;
        }
    }

    /// <summary>
    /// Gets the history log of the current game session
    /// </summary>
    public static LevelHistory History
    {
        get
        {
            return _this._log;
        }
    }

    /// <summary>
    /// Has the GameState class been initialized. For this to happen, the InitScene scene must be loaded first.
    /// </summary>
    public static bool Initialized
    {
        get
        {
            return _this != null;
        }
    }

    /// <summary>
    /// The game event sets. Game events are cleared when a game is ended
    /// </summary>
    public static GameEvents Events
    {
        get
        {
            return _this._ge;
        }
    }

    /// <summary>
    /// Is debug mode enabled? Debug mode can be enabled by pressing 'F10'
    /// </summary>
    public static bool DebugMode
    {
        get
        {
            return _this._debug;
        }
        set
        {
            _this._debug = value;
        }
    }

    /// <summary>
    /// Is godmode enabled. Godmode can be toggled by pressing 'G' while debug mode is enabled
    /// </summary>
    public static bool GodMode
    {
        get
        {
            return _this._god;
        }
        set
        {
            _this._god = value;
        }
    }

    protected override void Awake()
    {
        if (_this != null && _this.gameObject != gameObject)
        {
            throw new System.Exception("This level manager is not the first level manager to appear!");
        }

        _this = this;
        _settings = new GameSettings();

        DontDestroyOnLoad(gameObject);
    }


    private void Start()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Starts the game
    /// </summary>
    public static void StartNewGame() //BM Game Starter
    {
        //Ends the current game session
        if (GameplayStatus != GameplayStaus.MainMenu)
        {
            Events.OnGameEnd.Invoke();
        }

        //Sets up RNGs
        if (Settings.GenerateRandomSeed)
        {
            Settings.Seed = UnityEngine.Random.Range(0, int.MaxValue);
        }

        _this._rng = new System.Random(Settings.Seed);
        _this._rng2 = new System.Random(Settings.Seed);

        //Clears the level log
        _this._log = new LevelHistory();

        //Randomizes the gamemode
        if (Settings.RandomizedGamemode)
        {
            Settings.KeyMode = GeneratorRNG.Next(2) == 0;
            Settings.BlindMode = GeneratorRNG.Next(3) == 0;
            Settings.CoinMode = GeneratorRNG.Next(2) == 0;
        }

        //Generates the level
        StartLevel(Settings.Generator.GenerateLevel());

        Events.OnGameStart.Invoke();
    }

    /// <summary>
    /// Ends the game, and returns to the title screen
    /// </summary>
    public static void EndGame()
    {
        Events.OnGameEnd.Invoke();

        //Creates a new game state instance with the same settings
        GameSettings settings = Settings;

        _this.RemoveComponent<GameState>();
        _this.AddComponent<GameState>();

        _this._settings = settings;

        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    /// <summary>
    /// Loads a new level
    /// </summary>
    /// <param name="map">Level to load</param>
    private static void StartLevel(LevelState map)
    {

        if (_this._currentLevel != null)
        {
            Events.OnLevelEnd.Invoke();
        }

        //Set map
        _this._currentLevel = map;
        _this._currentLevelReset = map.Clone();

        //Set game status
        _this._gameplayStatus = GameplayStaus.InGame;

        Events.OnLevelStart.Invoke();
    }

    /// <summary>
    /// Set the game status to ExitingLevel
    /// </summary>
    public static void EndLevelAndAwaitInput()
    {
        _this._gameplayStatus = GameplayStaus.ExitingLevel;
        UIManager.SetTitles("Clear!", "In %n steps".Replace("%n", CurrentLevel.Player.Steps.Count.ToString()), Color.green, Color.red);
    }

    /// <summary>
    /// Exits a level, generating the next one
    /// </summary>
    public static void NextLevel()
    {
        //Randomizes the gamemode
        if (Settings.RandomizedGamemode)
        {
            Settings.KeyMode = GeneratorRNG.Next(2) == 0;
            Settings.BlindMode = GeneratorRNG.Next(3) == 0;
            Settings.CoinMode = GeneratorRNG.Next(2) == 0;
        }

        //The player finsihed by exiting through the door. Register it
        if (CurrentLevel.Player.Alive)
        {
            History.Register(CurrentLevel.Perfect ? LevelOutcome.PerfectClear : LevelOutcome.Clear);
        }
        //TODO Implement skip button
        //The player hit skip
        else
        {            
            //If the player has moved and clicked skip, count a death
            if (CurrentLevel.Player.Steps.Count > 0)
            {
                History.Register(LevelOutcome.Death);
            }
            //If not, do not register the skip as anything
        }
        
        //Start the next level
        StartLevel(Settings.Generator.GenerateLevel());
    }

    /// <summary>
    /// Restarts the current level, without generating a new one
    /// </summary>
    public static void RestartLevel()
    {
        History.Register(LevelOutcome.Death);

        StartLevel(_this._currentLevelReset);
    }
}

public enum GameplayStaus
{
    MainMenu,
    Paused,
    InGame,
    ExitingLevel
}
