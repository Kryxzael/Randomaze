using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class PlayerState : ICloneable
{
    private List<Vector2Int> _steps = new List<Vector2Int>();

    /// <summary>
    /// Is the player currently alive?
    /// </summary>
    public bool Alive { get; private set; }

    /// <summary>
    /// The position of the player
    /// </summary>
    public Vector2Int Position { get; private set; }

    /// <summary>
    /// the rotation of the player
    /// </summary>
    public Direction Rotation { get; private set; }

    /// <summary>
    /// Does the player currently have the electric effect on him?
    /// </summary>
    public bool Electric { get; private set; }

    /// <summary>
    /// The current fire status of the player, set to -1 if player is not on fire
    /// </summary>
    public int Fire { get; private set; }

    /// <summary>
    /// The amount of steps this player has gone
    /// </summary>
    public System.Collections.ObjectModel.ReadOnlyCollection<Vector2Int> Steps
    {
        get
        {
            return _steps.AsReadOnly();
        }
    }

    /// <summary>
    /// Is the player currently on fire?
    /// </summary>
    public bool OnFire
    {
        get
        {
            return Fire != -1;
        }
    }

    public PlayerState(Vector2Int position)
    {
        Position = position;
        Alive = true;
        Fire = -1;
    }

    /// <summary>
    /// Kills the player
    /// </summary>
    public void Kill(CauseOfDeath cause)
    {
        Alive = false;


        GameState.Events.OnPlayerKilled.Invoke(this, cause);
        UIManager.SetTitles("You Died!", _deathMessages[cause], Color.red, _deathColor[cause]);
    }

    /// <summary>
    /// Catches the player on fire
    /// </summary>
    public void SetOnFire()
    {
        //Does not reset the fire state if the player is allready on fire
        if (OnFire)
        {
            return;
        }

        Fire = 4;

        GameState.Events.OnPlayerStateChanged.Invoke(this);
    }

    /// <summary>
    /// Puts out the fire on the player if he is on fire
    /// </summary>
    public void Extinguish()
    {
        Fire = -1;

        GameState.Events.OnPlayerStateChanged.Invoke(this);
    }

    /// <summary>
    /// Updates the fire counter if the player is on fire
    /// </summary>
    public void TickFire()
    {
        //Do not tick if the game has been finished
        if (GameState.GameplayStatus != GameplayStaus.InGame) 
        {
            return;
        }

        if (OnFire)
        {
            Fire--;

            //Fire counter has reached 0, the player is dead
            if (Fire == 0)
            {
                Kill(CauseOfDeath.Fire);
            }

            GameState.Events.OnPlayerStateChanged.Invoke(this);
        }

        
    }

    /// <summary>
    /// Sets the player electric or not electric
    /// </summary>
    /// <param name="electric">Will the player be electric</param>
    public void SetElectric(bool electric)
    {
        if (electric == Electric)
        {
            return;
        }

        Electric = electric;

        GameState.Events.OnPlayerStateChanged.Invoke(this);
    }

    private bool _surpressFirstInput = true;
    /// <summary>
    /// Moves the player in a direction, updating position and rotation
    /// </summary>
    /// <param name="d">Direction to move in</param>
    public bool Move(Direction d)
    {
        if (_surpressFirstInput)
        {
            _surpressFirstInput = false;
            return false;
        }

        Vector2Int newPos = Position + d.ToVector();

        Rotation = d;

        //Checks if the new position is invalid
        if (!GameState.CurrentLevel.Map.IsInBounds(newPos.x, newPos.y) || //Target position is out of bounds
            GameState.CurrentLevel.Map.GetTile(newPos.x, newPos.y).IsSolidFrom(d.Reverse()) || //Target position is solid from the outside
            (GameState.CurrentLevel.Map.GetTile(Position.x, Position.y).SolidTwoSided() && GameState.CurrentLevel.Map.GetTile(Position.x, Position.y).IsSolidFrom(d))) //Current postion is solid from the inside
        {
            GameState.Events.OnPlayerMoved.Invoke(this, Position);
            return false;
        }

        //Updates position
        Vector2Int lastPos = Position;
        Position = newPos;


        //If the player has backtracked, remove as tep
        if (_steps.Count >= 2 && _steps[_steps.Count - 2] == Position)
        {
            _steps.RemoveAt(_steps.Count - 1);
        }
        //Adds the step
        else
        {
            _steps.Add(Position);
        }

        GameState.Events.OnPlayerMoved.Invoke(this, lastPos);

        return true;
    }

    /// <summary>
    /// Teleports the player
    /// </summary>
    /// <param name="x">New X-coord</param>
    /// <param name="y">New Y-coord</param>
    public void SetPosition(int x, int y)
    {
        //Checks if the new position is invalid
        if (!GameState.CurrentLevel.Map.IsInBounds(x, y))
        {
            return;
        }

        Vector2Int lastPos = Position;
        Position = new Vector2Int(x, y);

        GameState.Events.OnPlayerMoved.Invoke(this, lastPos);
    }

    object ICloneable.Clone()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Creates an exact duplicate of the player
    /// </summary>
    /// <returns></returns>
    public PlayerState Clone()
    {
        return new PlayerState(Position)
        {
            Alive = this.Alive,
            Electric = this.Electric,
            Fire = this.Fire,
            Rotation = this.Rotation
        };
    }

    private static Dictionary<CauseOfDeath, string> _deathMessages = new Dictionary<CauseOfDeath, string>()
    {
        { CauseOfDeath.Lava, "Don't touch the sauce!" },
        { CauseOfDeath.Fire, "Fire is hot" },
        { CauseOfDeath.Electricity, "ZZZZZap!" },
        { CauseOfDeath.Acid, "Mmmmm... Acid..." },
        { CauseOfDeath.GiveUp, "PLAYER_EXIT_LEVEl" },
    };

    private static Dictionary<CauseOfDeath, Color> _deathColor = new Dictionary<CauseOfDeath, Color>()
    {
        { CauseOfDeath.Lava, Color.red },
        { CauseOfDeath.Fire, new Color(1, 0.5f, 0) },
        { CauseOfDeath.Electricity, Color.yellow },
        { CauseOfDeath.Acid, Color.magenta },
        { CauseOfDeath.GiveUp, Color.white },
    };
}

/// <summary>
/// Represents a way the player died
/// </summary>
public enum CauseOfDeath
{
    Lava,
    Acid,
    Fire,
    Electricity,
    GiveUp
}