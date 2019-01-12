using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class LevelCounter : MonoBehaviour
{
    private void Start()
    {
        GameState.Events.OnLevelStart.AddListener(OnLevelStart);
    }

    private void OnLevelStart()
    {
        GetComponent<Text>().text = "Level " + GetLevelNumber(GameState.History.Level); 
    }

    private string GetLevelNumber(int level)
    {
        if (level == 0)
        {
            return "N";
        }
        else if (level >= 5000)
        {
            return level.ToString();
        }

        string _ = level.ToString("0000");
        string _r = "";

        switch (_[0])
        {
            case '1':
                _r += "M";
                break;
            case '2':
                _r += "MM";
                break;
            case '3':
                _r += "MMM";
                break;
            case '4':
                _r += "MMM";
                break;
        }
        switch (_[1])
        {
            case '1':
                _r += "C";
                break;
            case '2':
                _r += "CC";
                break;
            case '3':
                _r += "CCC";
                break;
            case '4':
                _r += "CD";
                break;
            case '5':
                _r += "D";
                break;
            case '6':
                _r += "DC";
                break;
            case '7':
                _r += "DCC";
                break;
            case '8':
                _r += "DCCC";
                break;
            case '9':
                _r += "CM";
                break;
        }
        switch (_[2])
        {
            case '1':
                _r += "X";
                break;
            case '2':
                _r += "XX";
                break;
            case '3':
                _r += "XXX";
                break;
            case '4':
                _r += "XL";
                break;
            case '5':
                _r += "L";
                break;
            case '6':
                _r += "LX";
                break;
            case '7':
                _r += "LXX";
                break;
            case '8':
                _r += "LXXX";
                break;
            case '9':
                _r += "XC";
                break;
        }
        switch (_[3])
        {
            case '1':
                _r += "I";
                break;
            case '2':
                _r += "II";
                break;
            case '3':
                _r += "III";
                break;
            case '4':
                _r += "IV";
                break;
            case '5':
                _r += "V";
                break;
            case '6':
                _r += "VI";
                break;
            case '7':
                _r += "VII";
                break;
            case '8':
                _r += "VIII";
                break;
            case '9':
                _r += "IX";
                break;
        }

        return _r;
    }
}