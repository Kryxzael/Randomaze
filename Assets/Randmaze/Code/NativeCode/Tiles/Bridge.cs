using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class BridgeHorizontal : Tile
    {
        public override string GetName()
        {
            return "Bridge";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileBridgeHorizontal";
        }

        public override void OnStep(PlayerState player) { }

        public override bool IsSolidFrom(Direction side)
        {
            return (int)side % 2 == 0;
        }

        public override bool SolidTwoSided()
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
                return ByName("BridgeVertical");
            }
        }
    }

    public class BridgeVertical : Tile
    {
        public override string GetName()
        {
            return "BridgeVertical";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileBridgeVertical";
        }

        public override void OnStep(PlayerState player) { }

        public override bool IsSolidFrom(Direction side)
        {
            return (int)side % 2 == 1;
        }

        public override bool SolidTwoSided()
        {
            return true;
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }
    }
}

