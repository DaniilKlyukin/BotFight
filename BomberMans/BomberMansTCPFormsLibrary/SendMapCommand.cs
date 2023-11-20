using BomberMansTCPFormsLibrary.GameObjects;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BomberMansTCPFormsLibrary
{
    public class SendMapCommand : Command
    {
        public GameObject[,]? Map { get; }
        public IList<PlayerInfo> PlayerInfos { get; }

        public SendMapCommand(GameObject[,]? map, IList<PlayerInfo> playerInfos)
        {
            Map = map;
            PlayerInfos = playerInfos;
        }

        public override string ConvertToJson()
        {
            var json = new JObject();
            json["Command"] = ((int)Commands.SendMap).ToString();

            var wallsStringBuilder = new StringBuilder();
            var explosionsStringBuilder = new StringBuilder();
            var bombPowerStringBuilder = new StringBuilder();
            var bombsStringBuilder = new StringBuilder();
            var buildsStringBuilder = new StringBuilder();
            var playersStringBuilder = new StringBuilder();
            var minesStringBuilder = new StringBuilder();
            var superPowerStringBuilder = new StringBuilder();

            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    switch (Map[i, j])
                    {
                        case Wall: wallsStringBuilder.Append($"{i},{j};"); break;
                        case Explosion: explosionsStringBuilder.Append($"{i},{j};"); break;
                        case BombPowerBonus: bombPowerStringBuilder.Append($"{i},{j};"); break;
                        case BuildBonus: buildsStringBuilder.Append($"{i},{j};"); break;
                        case SuperPowerBonus: superPowerStringBuilder.Append($"{i},{j};"); break;
                        case LandMine: minesStringBuilder.Append($"{i},{j};"); break;
                        case Bomb b: bombsStringBuilder.Append($"{i},{j},{b.Power},{b.TimeRemain};"); break;
                        case Player p:
                            {
                                var sptr = p.SuperPowerTimeRemain ?? 0;
                                playersStringBuilder.Append($"{i},{j},{p.Name},{sptr};");
                            }
                            break;
                    }
                }
            }

            var playersInfoStringBuilder = new StringBuilder();

            foreach (var info in PlayerInfos)
            {
                var isAlive = info.IsAlive ? 1 : 0;
                playersInfoStringBuilder.Append($"{info.Name},{info.Score},{isAlive}");
            }

            json["Map"] = new JObject();
            json["Map"]["Size"] = Map.GetLength(0);
            json["Map"]["Walls"] = wallsStringBuilder.ToString();
            json["Map"]["Explosions"] = explosionsStringBuilder.ToString();
            json["Map"]["BPowder"] = bombPowerStringBuilder.ToString();
            json["Map"]["Builds"] = buildsStringBuilder.ToString();
            json["Map"]["Mines"] = minesStringBuilder.ToString();
            json["Map"]["Bombs"] = bombsStringBuilder.ToString();
            json["Map"]["SuperPower"] = superPowerStringBuilder.ToString();
            json["Map"]["Players"] = playersStringBuilder.ToString();
            json["PlayersInfo"] = playersInfoStringBuilder.ToString();

            return Regex.Replace(json.ToString(), @"\s", "");
        }

        private static void ReadGameObjectToMap<T>(JObject json, GameObject?[,] map, string field) where T : GameObject, IReadable<T>
        {
            var power = json["Map"][field].ToString();

            foreach (var p in power.Split(';').Where(s => s.Any()))
            {
                var go = T.Parse(p);

                map[go.i, go.j] = go;
            }
        }

        public static SendMapCommand ParseFromJson(string jsonString)
        {
            var json = JObject.Parse(jsonString);
            var size = json["Map"]["Size"].Value<int>();

            var map = new GameObject?[size, size];

            ReadGameObjectToMap<Wall>(json, map, "Walls");
            ReadGameObjectToMap<Explosion>(json, map, "Explosions");
            ReadGameObjectToMap<BombPowerBonus>(json, map, "BPowder");
            ReadGameObjectToMap<BuildBonus>(json, map, "Builds");
            ReadGameObjectToMap<LandMine>(json, map, "Mines");
            ReadGameObjectToMap<Bomb>(json, map, "Bombs");
            ReadGameObjectToMap<SuperPowerBonus>(json, map, "SuperPower");
            ReadGameObjectToMap<Player>(json, map, "Players");

            var playersInfos = new List<PlayerInfo>();

            var info = json["PlayersInfo"].ToString();

            foreach (var p in info.Split(';').Where(s => s.Any()))
            {
                var data = p.Split(',');
                var name = data[0];
                var score = int.Parse(data[1]);
                var isAlive = int.Parse(data[2]);

                playersInfos.Add(new PlayerInfo("", name, score, isAlive == 1));
            }

            return new SendMapCommand(map, playersInfos);
        }
    }
}
