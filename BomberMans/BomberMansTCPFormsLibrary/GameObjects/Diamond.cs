namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Diamond : GameObject, IReadable<Diamond>
    {
        public const int Score = 10;

        public Diamond(string str) : base(str)
        {
        }

        public Diamond(int i, int j) : base(i, j)
        {
        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => false;
        public override bool BlockExplosion => false;
        public static Diamond Read(string s)
        {
            return new Diamond(s);
        }
    }
}
