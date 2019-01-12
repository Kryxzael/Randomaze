using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Ice : Tile
    {
        public override string GetName()
        {
            return "Ice";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileIce";
        }

        public override void OnStep(PlayerState player)
        {
            player.Move(player.Rotation);
        }
    }
}
