using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.BotsAlgorithms;
using BomberMansTCPFormsLibrary.GameObjects;
using System.Runtime.CompilerServices;

namespace BomberManClient
{
    public partial class ClientForm : Form
    {
        int CellSize = 30;
        const string PlayerName = "YourM8";
        Random rnd = new Random();

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
                BomberMansTCPHelper.DrawMap(pictureBox, map, PlayerName, CellSize);
            };

            c.SendPlayerAction = DoWork;

            c.Connect("192.168.0.12:9000", PlayerName);
        }

        public PlayerAction DoWork(GameObject?[,] map)
        {
            return (PlayerAction)rnd.Next(Enum.GetValues<PlayerAction>().Length);
        }

        private void splitContainer_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Oemplus:
                case Keys.Add: CellSize += 4; break;
                case Keys.OemMinus:
                case Keys.Subtract: if (CellSize > 24) CellSize -= 4; break;
            }
        }
    }
}