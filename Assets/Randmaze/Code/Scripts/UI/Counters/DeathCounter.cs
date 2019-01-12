using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Counters
{
    [RequireComponent(typeof(Text))]
    public class DeathCounter : MonoBehaviour
    {
        private void Start()
        {
            GameState.Events.OnLevelStart.AddListener(OnLevelStart);
        }

        //TODO: Implement 
        private void OnLevelStart()
        {
            GetComponent<Text>().text = GameState.History.Deaths.ToString();
        }
    }
}
