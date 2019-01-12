using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Darkness : MonoBehaviour
{
    private void Start()
    {
        GameState.Events.OnLevelStart.AddListener(OnLevelStart);   
    }

    private void OnLevelStart()
    {
        if (GameState.Settings.BlindMode)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color();
        }
    }
}