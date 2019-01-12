using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Randmaze.Code.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class MovementButton : MonoBehaviour
    {
        [Tooltip("The direction to move the player when clicked")]
        public Direction Direction;

        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(() => GameState.CurrentLevel.Player.Move(Direction));
        }
    }
}
