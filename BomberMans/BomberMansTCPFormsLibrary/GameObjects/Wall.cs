namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Wall : GameObject, IReadable<Wall>
    {
        public Wall(string str) : base(str) { }

        public Wall(int i, int j) : base(i, j) { }

        public override bool PlayerCanStep => false;
        public override bool Destructible => true;
        public override bool BlockExplosion => true;

        public static Wall Read(string s)
        {
            return new Wall(s);
        }
    }
}
