using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Acid : Tile
    {
        public override string GetName()
        {
            return "Acid";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileAcid";
        }

        public override void OnStep(PlayerState player)
        {
            player.Kill(CauseOfDeath.Acid);
        }
    }
}
