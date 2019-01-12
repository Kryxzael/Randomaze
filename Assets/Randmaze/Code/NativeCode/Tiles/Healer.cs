using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Healer : Tile
    {
        public override string GetName()
        {
            return "Healer";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileGreen";
        }

        public override void OnStep(PlayerState player)
        {
            player.SetElectric(false);
        }
    }
}
