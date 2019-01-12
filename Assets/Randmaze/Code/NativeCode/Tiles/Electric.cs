using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Tiles
{
    public class Electric : Tile
    {
        public override string GetName()
        {
            return "Electric";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileYellow";
        }

        public override void OnStep(PlayerState player)
        {
            player.SetElectric(true);
        }
    }
}