using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Blank : Tile
    {
        public override string GetName()
        {
            return "Blank";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileWhite";
        }

        public override void OnStep(PlayerState player)
        {
            
        }
    }
}
