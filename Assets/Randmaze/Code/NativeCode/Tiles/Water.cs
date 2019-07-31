using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Water : Tile
    {
        public override string GetName()
        {
            return "Water";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileBlue";
        }

        public override void OnStep(PlayerState player)
        {
            if (player.Electric)
            {
                player.Kill(CauseOfDeath.Electricity);
            }
            else
            {
                player.Extinguish();
            }
        }

        public override bool IsDynamicallyConnectedTile()
        {
            return true;
        }
    }
}