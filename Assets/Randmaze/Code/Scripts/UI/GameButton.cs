using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class GameButton : MonoBehaviour
{
    private void Start()
    {
        
    }

    /// <summary>
    /// Event handler: Fired when the skip button on clicked
    /// </summary>
    public void EvSkip()
    {
        GameState.CurrentLevel.Player.Kill(CauseOfDeath.GiveUp);
        GameState.NextLevel();
    }

    /// <summary>
    /// Event handler: Fired when the restart button is clicked
    /// </summary>
    public void EvRestart()
    {
        GameState.RestartLevel();
    }

    /// <summary>
    /// Event handler: Fired when the menu button is clicked
    /// </summary>
    public void EvMenu()
    {
        GameState.EndGame();
    }
}
