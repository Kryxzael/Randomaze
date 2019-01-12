using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Sand : Tile
    {
        public override string GetName()
        {
            return "Sand";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileSand";
        }

        public override void OnStep(PlayerState player)
        {
            GameState.CurrentLevel.Map.SetTile(player.Position.x, player.Position.y, ByName("Quicksand"));
        }
    }

    public class Quicksand : Tile
    {
        public override string GetName()
        {
            return "Quicksand";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileRed";
        }

        public override void OnStep(PlayerState player)
        {
            player.Kill(CauseOfDeath.Lava);
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }
    }
}
