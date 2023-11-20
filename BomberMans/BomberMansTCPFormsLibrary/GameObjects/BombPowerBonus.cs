namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class BombPowerBonus : GameObject, IReadable<BombPowerBonus>
    {
        public BombPowerBonus(string str) : base(str)
        {
        }

        public BombPowerBonus(int i, int j) : base(i, j)
        {
        }

        public static BombPowerBonus Parse(string s)
        {
            return  new BombPowerBonus(s);
        }
    }
}
