namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Powder : GameObject, IReadable<Powder>
    {
        public const int Score = 1;

        public Powder(string str) : base(str)
        {
        }

        public Powder(int i, int j) : base(i, j)
        {
        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public static Powder Read(string s)
        {
            return  new Powder(s);
        }
    }
}
