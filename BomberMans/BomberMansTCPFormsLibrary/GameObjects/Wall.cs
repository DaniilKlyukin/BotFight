namespace BomberMansTCPFormsLibrary.GameObjects
{
    [Serializable]
    public class Wall : GameObject, IReadable<Wall>
    {
        public Wall(string str) : base(str) { }

        public Wall(int i, int j) : base(i, j) { }

        public static Wall Parse(string s)
        {
            return new Wall(s);
        }
    }
}
