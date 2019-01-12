using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Spawn : Tile
    {
        public override string GetName()
        {
            return "Spawn";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileWhite";
        }

        public override void OnStep(PlayerState player)
        {
            
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }
}
