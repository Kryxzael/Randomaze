using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tiles
{
    /// <summary>
    /// A tile that mimics other tiles
    /// </summary>
    class Roulette : Tile
    {
        /// <summary>
        /// The index of the last tile this tile has mimiced
        /// </summary>
        private static int _mimicIndex = 0;

        /// <summary>
        /// The index of the tile that is being mimiced
        /// </summary>
        private static int _lastMimicIndex = 0;

        /// <summary>
        /// The names of all the tiles that roulette tiles can mimic
        /// </summary>
        private static string[] _mimicableTiles = new string[]
        {
            "Blank",
            "Lava",
            "Water",
            "Fire",
            "Electric",
            "Healer",
            "Water",
            "Ice",

            "Blank",
            "Lava",
            "Water",
            "Fire",
            "Electric",
            "Healer",
            "Water",
            "Ice",

            "Blank",

            "ConveyorUp",
            "ConveyorDown",
            "ConveyorLeft",
            "ConveyorRight",
            "Bridge",
            "BridgeVertical"
        };

        /// <summary>
        /// Gets the tile that is currently being mimiced
        /// </summary>
        public static Tile CurrentRouletteMimic
        {
            get
            {
                return ByName(_mimicableTiles[_mimicIndex]);
            }
        }


        public Roulette()
        {
            //Listens for player movements
            GameState.Events.OnPlayerMoved.AddListener(OnPlayerMoved);
        }

        public override string GetName()
        {
            return "Roulette";
        }

        public override string GetSpriteAnimationName()
        {
            //Ingame the texture will be taken from the mimiced tile
            if (GameState.GameplayStatus == GameplayStaus.InGame)
            {
                return CurrentRouletteMimic.GetSpriteAnimationName();
            }

            //In the menu, the icon will be a question mark
            return "TileRoulette";
        }

        private void OnPlayerMoved(PlayerState player, Vector2Int origPos)
        {
            //Gets the next tile to mimic
            int candidateIndex;

            do
            {
                candidateIndex = GameState.GeneralRNG.Next(_mimicableTiles.Length);

            } while (candidateIndex == _mimicIndex);

            //Updates mimic-indexes
            _lastMimicIndex = _mimicIndex;
            _mimicIndex = candidateIndex;

            //Updates every tile sprite...
            UnityEngine.Object.FindObjectsOfType<TileWrapper>()

                //...that is a roulette tile
                .Where(i => GameState.CurrentLevel.Map.GetTile(i.GridPosition.x, i.GridPosition.y) is Roulette)

                //...that is not the tile the player is moving onto
                .Where(i => i.GridPosition != player.Position)
                .ForEach(i => i.UpdateTile());
        }

        public override void OnStep(PlayerState player)
        {
            //Runs the OnStep behaviour of the last mimiced tile. 
            //This prevents the tile from changing while the player is stepping onto it
            ByName(_mimicableTiles[_lastMimicIndex]).OnStep(player);
        }
    }
}
