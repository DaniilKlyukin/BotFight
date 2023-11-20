using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberManClient
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        const string PlayerName = "YourM8";

        public Form1()
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

            c.Connect("192.168.0.2:9000", PlayerName);
        }

        public PlayerAction DoWork(GameObject[,]? map)
        {
            return (PlayerAction)rnd.Next(9);
        }
    }
}