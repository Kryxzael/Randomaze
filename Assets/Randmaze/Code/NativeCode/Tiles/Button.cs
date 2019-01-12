using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Button : Tile
    {
        public override string GetName()
        {
            return "Button";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileButtonOn";
        }

        public override void OnStep(PlayerState player)
        {
            for (int x = 0; x < GameState.CurrentLevel.Map.Width; x++)
            {
                for (int y = 0; y < GameState.CurrentLevel.Map.Height; y++)
                {
                    switch (GameState.CurrentLevel.Map.GetTile(x, y).GetName())
                    {
                        case "Block":
                            GameState.CurrentLevel.Map.SetTile(x, y, ByName("BlockUnsolid"));
                            break;
                        case "BlockUnsolid":
                            GameState.CurrentLevel.Map.SetTile(x, y, ByName("Block"));
                            break;
                        case "Button":
                            GameState.CurrentLevel.Map.SetTile(x, y, ByName("ButtonOff"));
                            break;
                        case "ButtonOff":
                            GameState.CurrentLevel.Map.SetTile(x, y, ByName("Button"));
                            break;
                    }
                }
            }
        }

        public override Tile GetTileForGenerator()
        {
            if (GameState.GeneratorRNG.Next(2) == 0)
            {
                return this;
            }
            else
            {
                return ByName("ButtonOff");
            }
        }
    }

    public class ButtonOff : Tile
    {
        public override string GetName()
        {
            return "ButtonOff";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileButtonOff";
        }

        public override void OnStep(PlayerState player) { }

        public override bool ShowInCustomInspector()
        {
            return false;
        }
    }
}
