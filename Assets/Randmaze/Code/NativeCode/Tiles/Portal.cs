namespace Tiles
{
    public class Portal : Tile
    {
        private string _name;

        public Portal(string name)
        {
            _name = name;
        }

        public override string GetName()
        {
            return _name;
        }

        public override string GetSpriteAnimationName()
        {
            throw new System.NotImplementedException();
        }

        public override void OnStep(PlayerState player)
        {
            throw new System.NotImplementedException(); //TODO Implement Portals
        }

        public override bool IsSpecialTile()
        {
            return true;
        }
    }
}