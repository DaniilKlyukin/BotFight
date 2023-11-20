namespace BomberMansTCPFormsLibrary
{
    public record PlayerInfo(string IP, string Name, int Score, bool IsAlive)
    {
        public override string? ToString()
        {
            var isAliveStr = IsAlive ? "[Жив]" : "[Мертв]";
            return $"{Name}: {Score} {isAliveStr}";
        }
    }
}