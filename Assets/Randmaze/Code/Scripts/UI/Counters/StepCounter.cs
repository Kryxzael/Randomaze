using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Counters
{
    [RequireComponent(typeof(Text))]
    public class StepCounter : MonoBehaviour
    {
        private void Start()
        {
            GameState.Events.OnLevelStart.AddListener(() => OnMoved(null, default(Vector2Int)));
            GameState.Events.OnPlayerMoved.AddListener(OnMoved);
        }

        private void OnMoved(PlayerState pl, Vector2Int obj)
        {
            GetComponent<Text>().text = GameState.CurrentLevel.Player.Steps.Count.ToString();
        }
    }
}
