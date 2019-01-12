using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// Represents a log of level transtions, holding info about their clear state
/// </summary>
public class LevelHistory : IEnumerable<LevelOutcome>
{
    private List<LevelOutcome> _data = new List<LevelOutcome>();

    /// <summary>
    /// Gets the amount of clears, perfect or imperfect
    /// </summary>
    public int Clears
    {
        get
        {
            return _data.Count(i => i != LevelOutcome.Death);
        }
    }

    /// <summary>
    /// Gets the amount of perfect clears
    /// </summary>
    public int Perfects
    {
        get
        {
            return _data.Count(i => i == LevelOutcome.PerfectClear);
        }
    }

    /// <summary>
    /// Gets the amount of deaths or skips
    /// </summary>
    public int Deaths
    {
        get
        {
            return _data.Count(i => i == LevelOutcome.Death);
        }
    }

    /// <summary>
    /// Gets the amount of clears, excluding the clears that were perfect
    /// </summary>
    public int ImperfectClears
    {
        get
        {
            return _data.Count(i => i == LevelOutcome.Clear);
        }
    }

    /// <summary>
    /// Gets the amount of entries in this level history
    /// </summary>
    public int Count
    {
        get
        {
            return _data.Count;
        }
    }

    /// <summary>
    /// Gets the level you are on according to this level history, this is always Clears + 1
    /// </summary>
    public int Level
    {
        get
        {
            return Clears + 1;
        }
    }

    /// <summary>
    /// Registers a new outcome
    /// </summary>
    /// <param name="outcome">Outcome to register</param>
    public void Register(LevelOutcome outcome)
    {
        _data.Add(outcome);
    }

    public IEnumerator<LevelOutcome> GetEnumerator()
    {
        return _data.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return _data.GetEnumerator();
    }
}

/// <summary>
/// Represents the outcome of a level transition, used by level logging
/// </summary>
public enum LevelOutcome
{
    Clear,
    PerfectClear,
    Death
}
