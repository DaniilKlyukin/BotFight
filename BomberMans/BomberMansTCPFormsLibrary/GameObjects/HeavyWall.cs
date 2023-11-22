namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class HeavyWall : GameObject, IReadable<HeavyWall>
    {
        public HeavyWall(string str) : base(str)
        {
        }

        public HeavyWall(int i, int j) : base(i, j)
        {
        }
        public override bool PlayerCanStep => false;
        public override bool Destructible => false;
        public override bool BlockExplosion => true;
        public static HeavyWall Read(string s)
        {
            return new HeavyWall(s);
        }
    }
}
