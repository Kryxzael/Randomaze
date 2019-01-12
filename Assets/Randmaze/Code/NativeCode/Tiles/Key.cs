using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Key : Tile
    {
        public override string GetName()
        {
            return "Key";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileKey";
        }

        public override void OnStep(PlayerState player)
        {
            GameState.CurrentLevel.KeyCollected = true;
            GameState.CurrentLevel.Map.SetTile(player.Position.x, player.Position.y, ByName("KeyCollected"));
            GameState.CurrentLevel.Map.ReplaceTile(ByName("ExitLocked"), ByName("Exit"));

            GameState.Events.OnKeyCollected.Invoke();
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }

    public class KeyLocked : Blank
    {
        public override string GetName()
        {
            return "KeyLocked";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileKeyLocked";
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

    public class KeyCollected : Blank
    {
        public override string GetName()
        {
            return "KeyCollected";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileKeyCollected";
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
