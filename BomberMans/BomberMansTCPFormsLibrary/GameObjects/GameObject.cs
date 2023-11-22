namespace BomberMansTCPFormsLibrary.GameObjects
{
    public abstract class GameObject
    {
        protected GameObject(string str)
        {
            var data = str.Split(',');
            i = int.Parse(data[0]);
            j = int.Parse(data[1]);
        }

        protected GameObject(int i, int j)
        {
            this.i = i;
            this.j = j;
        }

        public abstract bool PlayerCanStep { get; }
        public abstract bool Destructible { get; }
        public abstract bool BlockExplosion { get; }
        public int i { get; set; }
        public int j { get; set; }
    }
}
