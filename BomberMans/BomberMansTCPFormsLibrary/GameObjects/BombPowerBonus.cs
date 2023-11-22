namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class BombPowerBonus : GameObject, IReadable<BombPowerBonus>
    {
        public const int Score = 1;

        public BombPowerBonus(string str) : base(str)
        {
        }

        public BombPowerBonus(int i, int j) : base(i, j)
        {
        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public static BombPowerBonus Read(string s)
        {
            return  new BombPowerBonus(s);
        }
    }
}
