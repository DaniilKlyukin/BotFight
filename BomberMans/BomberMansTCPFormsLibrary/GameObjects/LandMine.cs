namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class LandMine : GameObject, IReadable<LandMine>
    {
        public const int Power = 2;

        public LandMine(string str) : base(str)
        {
        }

        public LandMine(int i, int j) : base(i, j)
        {

        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public static LandMine Read(string s)
        {
            return new LandMine(s);
        }
    }
}
