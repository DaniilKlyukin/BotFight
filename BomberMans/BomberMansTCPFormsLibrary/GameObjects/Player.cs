namespace BomberMansTCPFormsLibrary.GameObjects
{
    public class Player : GameObject, IReadable<Player>
    {
        public Player(int i, int j, string iP, string name, int score, int bombPowerModificator) : base(i, j)
        {
            IP = iP;
            Name = name;
            Score = score;
            BombPowerModificator = bombPowerModificator;
        }


        public Player(string str) : base(str)
        {
            IP = string.Empty;
            Score = 0;

            var info = str.Split(',');
            i = int.Parse(info[0]);
            j = int.Parse(info[1]);
            Name = info[2];

            var sptr = int.Parse(info[3]);
            SuperPowerTimeRemain = sptr == 0 ? null : sptr;
        }

        public string IP { get; set; }
        public string Name { get; set; }
        public int Score { get; set; }
        public bool IsAlive { get; set; } = true;
        public int? TimeToRessurect { get; set; } = null;
        public int? SuperPowerTimeRemain { get; set; } = null;
        public int BombPowerModificator { get; set; }

        public static Player Parse(string s)
        {
            return new Player(s);
        }
    }
}
