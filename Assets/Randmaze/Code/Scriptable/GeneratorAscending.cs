using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// System Level Generator for generation of ascending difficulty levels
/// </summary>
[CreateAssetMenu(menuName = "Generators/Ascending Generator")]
public class GeneratorAscending : Generator
{
    /// <summary>
    /// Lists of presets to use for ascending difficulty
    /// </summary>
    public List<Generator> Levels;

    public override Generator Clone()
    {
        GeneratorAscending _ = CreateInstance<GeneratorAscending>();
        _.Levels = Levels.ToList();
        return _;
    }

    public override LevelState GenerateLevel()
    {
        int lvlindex = GameState.History.Level - 1;

        //We have reached max level
        if (lvlindex >= Levels.Count)
        {
            return Levels.Last().GenerateLevel();
        }

        return Levels[lvlindex].GenerateLevel();
    }

    public override string GetName()
    {
        return "Ascending";
    }
}