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
        public override bool PlayerCanStep => true;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public static Explosion Read(string s)
        {
            return new Explosion(s);
        }
    }
}
