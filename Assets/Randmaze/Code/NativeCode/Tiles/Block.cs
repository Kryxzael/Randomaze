using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Block : Tile
    {
        public override string GetName()
        {
            return "Block";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileBlockSolid";
        }

        public override void OnStep(PlayerState player) { }

        public override bool IsSolidFrom(Direction side)
        {
            return true;
        }

        public override Tile GetTileForGenerator()
        {
            if (GameState.GeneratorRNG.Next(2) == 0)
            {
                return this;
            }
            else
            {
                return ByName("BlockUnsolid");
            }
        }
    }

    public class BlockUnsolid : Tile
    {
        public override string GetName()
        {
            return "BlockUnsolid";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileBlockUnsolid";
        }

        public override void OnStep(PlayerState player) { }

        public override bool ShowInCustomInspector()
        {
            return false;
        }
    }
}
