using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StatusIcon : MonoBehaviour
{
    [AutoGetComponent(typeof(Image))]
    private Image _img;

    /// <summary>
    /// Is the status icon lit up?
    /// </summary>
    public bool EnabledIcon
    {
        get
        {
            return _img.color.a == 1f;
        }
        set
        {
            _img.color = new Color(_img.color.r, _img.color.g, _img.color.b, value ? 1f : 0.25f);
        }
    }

   //TODO Implement

    private void UpdateState()
    {
        switch (name)
        {
            case "FxElectric":
                EnabledIcon = GameState.CurrentLevel.Player.Electric;
                break;
            case "FxFire1":
                EnabledIcon = GameState.CurrentLevel.Player.OnFire && GameState.CurrentLevel.Player.Fire <= 3;
                break;
            case "FxFire2":
                EnabledIcon = GameState.CurrentLevel.Player.OnFire && GameState.CurrentLevel.Player.Fire <= 2;
                break;
            case "FxFire3":
                EnabledIcon = GameState.CurrentLevel.Player.OnFire && GameState.CurrentLevel.Player.Fire <= 1;
                break;
            case "FxKey":
                EnabledIcon = GameState.CurrentLevel.KeyCollected;
                break;
            default:
                break;
        }
    }
}
