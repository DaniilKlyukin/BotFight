namespace BomberMansTCPFormsLibrary
{
    public record PlayerInfo(string IP, string Name, int Score, bool IsAlive)
    {
        public override string? ToString()
        {
            return Name;
        }
    }
}