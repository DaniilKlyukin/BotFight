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

        public static BuildBonus Parse(string s)
        {
            return new BuildBonus(s);
        }
    }
}
