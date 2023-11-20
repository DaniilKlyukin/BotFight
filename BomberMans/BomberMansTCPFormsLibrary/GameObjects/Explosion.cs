namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Explosion : GameObject, IReadable<Explosion>
    {
        public string Owner { get; private set; }

        public Explosion(string str) : base(str)
        {
            Owner = "";
        }

        public Explosion(int i, int j, string owner) : base(i, j)
        {
            Owner = owner;
        }

        public static Explosion Parse(string s)
        {
            return new Explosion(s);
        }
    }
}
