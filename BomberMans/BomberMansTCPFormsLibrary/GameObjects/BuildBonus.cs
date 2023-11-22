namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class BuildBonus : GameObject, IReadable<BuildBonus>
    {
        public const int HalfSize = 2;

        public BuildBonus(string str) : base(str)
        {
        }

        public BuildBonus(int i, int j) : base(i, j)
        {
        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public static BuildBonus Read(string s)
        {
            return new BuildBonus(s);
        }
    }
}
