namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class SuperPowerBonus : GameObject, IReadable<SuperPowerBonus>
    {
        public const int BasicSuperPowerTime = 10;
        public const int MaxPenetration = 1;

        public  SuperPowerBonus(string str) : base(str) { }
        public SuperPowerBonus(int i, int j) : base(i, j) { }

        public static SuperPowerBonus Read(string s)
        {
            return new SuperPowerBonus(s);
        }
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
    }
}
