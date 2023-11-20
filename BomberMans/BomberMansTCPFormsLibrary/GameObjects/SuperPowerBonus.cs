namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class SuperPowerBonus : GameObject, IReadable<SuperPowerBonus>
    {
        public const int BasicSuperPowerTime = 10;
        public const int MaxBrokenWalls = 2;

        public  SuperPowerBonus(string str) : base(str) { }
        public SuperPowerBonus(int i, int j) : base(i, j) { }

        public static SuperPowerBonus Parse(string s)
        {
            return new SuperPowerBonus(s);
        }
    }
}
