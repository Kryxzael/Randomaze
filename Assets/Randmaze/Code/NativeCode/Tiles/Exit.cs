using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Exit : Tile
    {
        public override string GetName()
        {
            return "Exit";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileExit";
        }

        public override void OnStep(PlayerState player)
        {
            GameState.EndLevelAndAwaitInput();
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }

    public class ExitLocked : Blank
    {
        public override string GetName()
        {
            return "ExitLocked";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileExitLocked";
        }

        public override bool IsSpecialTile()
        {
            return true;
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }
    }
}
