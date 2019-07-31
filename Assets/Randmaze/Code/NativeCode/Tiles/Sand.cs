using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tiles
{
    public class Sand : Tile
    {
        public override string GetName()
        {
            return "Sand";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileSand";
        }

        public override void OnStep(PlayerState player)
        {
            //Listens for the player's next move for then to collapse the tile
            GameState.Events.OnPlayerMoved.AddListener(OnStepOff);
        }

        private void OnStepOff(PlayerState player, Vector2Int origPos)
        {
            //Player attempted to move, but was obstructed. Do not collapse the tile
            if (player.Position == origPos)
            {
                return;
            }

            //Collapses the tile into a lava tile when the player steps off
            GameState.CurrentLevel.Map.SetTile(origPos.x, origPos.y, ByName("Lava"));
            GameState.Events.OnPlayerMoved.RemoveListener(OnStepOff);
        }

        public override bool IsDynamicallyConnectedTile()
        {
            return true;
        }
    }

    public class Quicksand : Tile
    {
        public override string GetName()
        {
            return "Quicksand";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileRed";
        }

        public override void OnStep(PlayerState player)
        {
            player.Kill(CauseOfDeath.Lava);
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }

        public override bool IsDynamicallyConnectedTile()
        {
            return true;
        }
    }
}
