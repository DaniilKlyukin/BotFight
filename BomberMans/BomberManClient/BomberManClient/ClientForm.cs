using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberManClient
{
    public partial class ClientForm : Form
    {
        Random rnd = new Random();
        const string PlayerName = "YourM8";

        PlayerAction lastStep = PlayerAction.None;
        public double StepProbability { get; set; } = 0.8;
        public double RepeatProbability { get; set; } = 0.7;

        public ClientForm()
        {
            InitializeComponent();

            var c = new Client();

            c.UpdatePlayersInfo = (info) =>
            {
                Invoke(() => BomberMansTCPHelper.UpdatePlayersListBox(
                    playersListBox, info));
            };

            c.Visualize = (map) =>
            {
                BomberMansTCPHelper.DrawMap(pictureBox, map, PlayerName, 24);
            };

            c.SendPlayerAction = DoWork;

            c.Connect("192.168.0.194:9000", PlayerName);
        }

        public PlayerAction DoWork(GameObject?[,] map)
        {
            Player player = null;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is Player p && p.Name == PlayerName)
                    {
                        player = p;
                        break;
                    }
                }
            }

            if (player == null)
                return PlayerAction.None;

            var validSteps = new List<PlayerAction>() { PlayerAction.None };
            var validActs = new List<PlayerAction>() { };

            if (player.i - 1 >= 0)
            {
                if (map[player.i - 1, player.j]?.PlayerCanStep ?? true)
                {
                    validSteps.Add(PlayerAction.Top);
                    validActs.Add(PlayerAction.BombTop);
                }
                else if (player.SuperPowerTimeRemain != null && map[player.i - 1, player.j] is not HeavyWall)
                {
                    validActs.Add(PlayerAction.BombTop);
                }
            }
            if (player.i + 1 >= 0)
            {
                if (map[player.i + 1, player.j]?.PlayerCanStep ?? true)
                {
                    validSteps.Add(PlayerAction.Bottom);
                    validActs.Add(PlayerAction.BombBottom);
                }
                else if (player.SuperPowerTimeRemain != null && map[player.i + 1, player.j] is not HeavyWall)
                {
                    validActs.Add(PlayerAction.BombBottom);
                }
            }
            if (player.j - 1 >= 0)
            {
                if (map[player.i, player.j - 1]?.PlayerCanStep ?? true)
                {
                    validSteps.Add(PlayerAction.Left);
                    validActs.Add(PlayerAction.BombLeft);
                }
                else if (player.SuperPowerTimeRemain != null && map[player.i, player.j - 1] is not HeavyWall)
                {
                    validActs.Add(PlayerAction.BombLeft);
                }
            }
            if (player.j + 1 >= 0)
            {
                if (map[player.i, player.j + 1]?.PlayerCanStep ?? true)
                {
                    validSteps.Add(PlayerAction.Right);
                    validActs.Add(PlayerAction.BombRight);
                }
                else if (player.SuperPowerTimeRemain != null && map[player.i, player.j + 1] is not HeavyWall)
                {
                    validActs.Add(PlayerAction.BombRight);
                }
            }

            var stepOrAct = rnd.NextDouble();

            if (stepOrAct <= StepProbability)
            {
                var r = rnd.NextDouble();

                if (r <= RepeatProbability && validSteps.Contains(lastStep) && lastStep != PlayerAction.None)
                    return lastStep;

                var i = rnd.Next(validSteps.Count);
                lastStep = validSteps[i];
                return validSteps[i];
            }
            else
            {
                var i = rnd.Next(validActs.Count);
                return validActs[i];
            }
        }
    }
}