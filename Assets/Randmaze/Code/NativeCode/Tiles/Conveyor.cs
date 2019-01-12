using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiles
{
    public class Conveyor : Tile
    {
        public readonly Direction ConveyorDirection;

        public override string GetName()
        {
            return "Conveyor" + ConveyorDirection.ToString();
        }

        public override string GetSpriteAnimationName()
        {
            return "TileConveyor" + ConveyorDirection.ToString();
        }

        public override void OnStep(PlayerState player) { }

        public Conveyor(Direction dir)
        {
            ConveyorDirection = dir;
        }

        public override bool ShowInCustomInspector()
        {
            return false;
        }

        public override bool IsSolidFrom(Direction side)
        {
            return side == ConveyorDirection;
        }
    }

    public class ConveyorEditor : Tile
    {
        public override string GetName()
        {
            return "Conveyor";
        }

        public override string GetSpriteAnimationName()
        {
            return "TileConveyorLeft";
        }

        public override void OnStep(PlayerState player) { }

        public override Tile GetTileForGenerator()
        {
            return ByName("Conveyor" + ((Direction)GameState.GeneratorRNG.Next(4)).ToString());
        }
    }
}
