using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Lava : Tile
    {
        public override string GetName()
        {
            return "Lava";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileRed";
        }

        public override void OnStep(PlayerState player)
        {
            player.Kill(CauseOfDeath.Lava);
        }

        public override bool IsDynamicallyConnectedTile()
        {
            return true;
        }
    }
}