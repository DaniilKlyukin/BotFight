using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.BotsAlgorithms;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberManClient
{
    public partial class ClientForm : Form
    {
        const string PlayerName = "YourM8";
        VerySimpleBot bot = new VerySimpleBot();

        public ClientForm()
        {
            InitializeComponent();

            var c = new Client();

            c.UpdatePlayersInfo = (info) =>
            {
                Invoke(() => BomberMansTCPHelper.UpdatePlayersDataGrid(
                    playersDataGridView, info));
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
            return bot.DoAction(map, PlayerName);
        }
    }
}