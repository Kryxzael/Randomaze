using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Coin : Tile
    {
        public override string GetName()
        {
            return "Coin";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileCoin";
        }

        public override void OnStep(PlayerState player)
        {
            GameState.CurrentLevel.CoinsCollected++;
            GameState.CurrentLevel.Map.SetTile(player.Position.x, player.Position.y, ByName("CoinCollected" + (GameState.CurrentLevel.LevelCoinCount - GameState.CurrentLevel.CoinsCollected)));

            if (GameState.CurrentLevel.CoinsCollected >= GameState.CurrentLevel.LevelCoinCount)
            {
                if (GameState.Settings.KeyMode)
                {
                    GameState.CurrentLevel.Map.ReplaceTile(ByName("KeyLocked"), ByName("Key"));
                }
                else
                {
                    GameState.CurrentLevel.Map.ReplaceTile(ByName("ExitLocked"), ByName("Exit"));
                }
                
            }

            GameState.Events.OnCoinCollected.Invoke();
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }

    public class CoinCollected : Blank
    {
        public int Index;

        public CoinCollected(int index)
        {
            Index = index;
        }

        public override string GetName()
        {
            return "CoinCollected" + Index;
        }

        public override string GetSpriteAnimationName()
        {
            return "TileCoinCollected" + Index;
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }
}
