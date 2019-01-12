using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public static class UIManager
{
    /// <summary>
    /// Will the UI be visible?
    /// </summary>
    public static bool UIVisible
    {
        get
        {
            return GameState.GameplayStatus != GameplayStaus.MainMenu;
        }
    }

    /// <summary>
    /// Sets the title and subtitle of the UI and shows it
    /// </summary>
    /// <param name="title">Text of the title</param>
    /// <param name="subtitle">Text of the sub title</param>
    /// <param name="titleColor">Color of the title</param>
    /// <param name="subtitleColor">Color of the sub title</param>
    /// <returns></returns>
    public static void SetTitles(string title, string subtitle, Color titleColor, Color subtitleColor)
    {
        if (!UIVisible)
        {
            throw new InvalidOperationException("UI is not visible at this time");
        }

        //Sets the title
        GetTitleComponent().text = title;
        GetTitleComponent().color = titleColor;

        //Sets the subttitle
        GetSubtitleComponent().text = subtitle;
        GetSubtitleComponent().color = subtitleColor;
    }

    /// <summary>
    /// Hides the title UI
    /// </summary>
    public static void HideTitles()
    {
        if (!UIVisible)
        {
            return;
        }

        GetTitleComponent().text = string.Empty;
        GetSubtitleComponent().text = string.Empty;
    }

    /// <summary>
    /// Retrives the Text component of the title
    /// </summary>
    /// <returns></returns>
    private static Text GetTitleComponent()
    {
        return GameObject.Find("UI/Titles/Title").GetComponent<Text>();
    }

    /// <summary>
    /// Retrives the Text component of the sub-title
    /// </summary>
    /// <returns></returns>
    private static Text GetSubtitleComponent()
    {
        return GameObject.Find("UI/Titles/Subtitle").GetComponent<Text>();
    }
}
