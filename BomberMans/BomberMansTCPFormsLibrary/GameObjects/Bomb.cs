namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Bomb : GameObject, IReadable<Bomb>
    {
        public const int BasicPower = 2;
        public const int BasicTimeRemain = 5;
        public override bool PlayerCanStep => false;
        public override bool Destructible => true;
        public override bool BlockExplosion => false;
        public Bomb(string str) : base(str)
        {
            var info = str.Split(',');
            i = int.Parse(info[0]);
            j = int.Parse(info[1]);
            Power = int.Parse(info[2]);
            TimeRemain = int.Parse(info[3]);
            Owner = "";
        }

        public Bomb(int i, int j, int power, int timeRemain, string owner) : base(i, j)
        {
            Power = power;
            TimeRemain = timeRemain;
            Owner = owner;
        }

        public void Tick()
        {
            TimeRemain--;
        }

        public static Bomb Read(string s)
        {
            return new Bomb(s);
        }

        public int TimeRemain { get; private set; }
        public string Owner { get; private set; }
        public int Power { get; private set; }
    }
}
