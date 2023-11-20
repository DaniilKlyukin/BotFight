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

        public static LandMine Parse(string s)
        {
            return new LandMine(s);
        }
    }
}
