using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Fire : Tile
    {
        public override string GetName()
        {
            return "Fire";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileOrange";
        }

        public override void OnStep(PlayerState player)
        {
            player.SetOnFire();
        }

        public override bool IsDynamicallyConnectedTile()
        {
            return true;
        }
    }
}