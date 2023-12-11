using SuperSimpleTcp;
using BomberMansTCPFormsLibrary;
using System.Diagnostics;
using System.Text;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMans
{

    public partial class GameForm : Form
    {
        bool developerMode = false;
        SimpleTcpServer server;
        GameController controller;
        int CellSize = 30;
        const int MapSize = 40;
        HashSet<string> bannedPlayers = new HashSet<string>();
        static Random rnd = new Random();

        public GameForm()
        {
            InitializeComponent();

            server = new($"{BomberMansTCPHelper.GetLocalIPAddress()}:9000");

            server.Events.ClientConnected += ClientConnected;
            server.Events.ClientDisconnected += ClientDisconnected;
            server.Events.DataReceived += DataReceived;

            DoubleBuffered = true;
            controller = new GameController() { RessurectionTime = 3 };
            server.Start();
            Debug.WriteLine(server.IpAddress);

        }

        private void DataReceived(object? sender, SuperSimpleTcp.DataReceivedEventArgs e)
        {
            try
            {
                var data = Encoding.UTF8.GetString(e.Data);

                var command = Command.ParseFromJson(data);
                var ip = BomberMansTCPHelper.ParseIP(e.IpPort);

                switch (command)
                {
                    case SendPlayerNameCommand c:
                        {
                            if (controller.ContainsPlayer(ip))
                                controller.SetPlayerName(ip, c.Name);

                            if (!GameTimer.Enabled)
                            {
                                Invoke(VisualizationUpdate);
                            }
                        }
                        break;
                    case SendPlayerActionCommand c:
                        {
                            controller.AddAction(ip, c.Action);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void ClientDisconnected(object? sender, ConnectionEventArgs e)
        {

        }

        private void ClientConnected(object? sender, ConnectionEventArgs e)
        {
            var ip = BomberMansTCPHelper.ParseIP(e.IpPort);

            if (bannedPlayers.Contains(ip))
            {
                server.DisconnectClient(ip);
                return;
            }

            if (controller.ContainsPlayer(ip))
                return;

            controller.AddPlayer(ip, ip);

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }

        private void VisualizationUpdate()
        {
            BomberMansTCPHelper.DrawMap(pictureBox, controller.GetMap(), null, CellSize);
            BomberMansTCPHelper.UpdatePlayersDataGrid(
                playersDataGridView,
                GetPlayerInfos());
        }

        private IList<PlayerInfo> GetPlayerInfos()
        {
            return controller.GetPlayers()
                .Select(p => new PlayerInfo(p.IP, p.Name, p.Score, p.IsAlive))
                .ToList();
        }

        private void CreateLevel_Click(object sender, EventArgs e)
        {
            controller.GenerateRandomMap(MapSize);
            Invoke(VisualizationUpdate);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            GameTimer.Start();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            GameTimer.Stop();
        }

        private void SendDataToPlayers()
        {
            var command = new SendMapCommand(controller.GetMap(), GetPlayerInfos()).ConvertToJson();

            foreach (var ip in server.GetClients())
            {
                server.SendAsync(ip, command);
            }
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            try
            {
                controller.GameStep();
                Task.Run(() => Invoke(VisualizationUpdate));
                SendDataToPlayers();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private void kickPlayerButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in playersDataGridView.SelectedRows)
            {
                var player = (PlayerInfo)r.Cells[1].Value;
                controller.RemovePlayer(player.IP);
                server.DisconnectClient(player.IP);
            }

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }

        private void banPlayerButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow r in playersDataGridView.SelectedRows)
            {
                var player = (PlayerInfo)r.Cells[1].Value;
                bannedPlayers.Add(player.IP);
                controller.RemovePlayer(player.IP);
                server.DisconnectClient(player.IP);
            }

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }

        private void DeveloperButtom_Click(object sender, EventArgs e)
        {
            developerMode = !developerMode;
            DeveloperButtom.BackColor = developerMode ? Color.Green : Color.FromArgb(255, 240, 240, 240);
        }

        private void DeveloperModeButtonClick(PreviewKeyDownEventArgs e)
        {
            //F1 - heavy wall, F2 - wall, F3 - powder, F4 - mine, F5 - diamond, F6 - build, F7 - super power, F8 - bomb, F9 - dummy, F12 - delete

            var relativePoint = pictureBox.PointToClient(Cursor.Position);

            var x = relativePoint.X;
            var y = relativePoint.Y;

            var j = x / CellSize;
            var i = y / CellSize;

            if (i < 0 || j < 0 || i >= controller.MapSize || j >= controller.MapSize)
            {
                return;
            }

            switch (e.KeyCode)
            {
                case Keys.F1: controller.PlaceGameObject(new HeavyWall(i, j)); break;
                case Keys.F2: controller.PlaceGameObject(new Wall(i, j)); break;
                case Keys.F3: controller.PlaceGameObject(new Powder(i, j)); break;
                case Keys.F4: controller.PlaceGameObject(new LandMine(i, j)); break;
                case Keys.F5: controller.PlaceGameObject(new Diamond(i, j)); break;
                case Keys.F6: controller.PlaceGameObject(new BuildBonus(i, j)); break;
                case Keys.F7: controller.PlaceGameObject(new SuperPowerBonus(i, j)); break;
                case Keys.F8: controller.PlaceGameObject(new Bomb(i, j, 3, 3, "")); break;
                case Keys.F9: controller.PlaceGameObject(new Player(i, j, $"YourM8", $"YourM8", 0, 0)); break;
                //case Keys.F9: controller.PlaceGameObject(new Player(i, j, $"{rnd.Next(255)}.{rnd.Next(255)}.{rnd.Next(255)}.{rnd.Next(255)}", $"Player {rnd.Next(1000)}", 0, 0)); break;
                case Keys.F12: controller.RemoveGameObject(i, j); break;
                case Keys.Left: controller.AddAction("YourM8", PlayerAction.Left); break;
                case Keys.Right: controller.AddAction("YourM8", PlayerAction.Right); break;
                case Keys.Down: controller.AddAction("YourM8", PlayerAction.Bottom); break;
                case Keys.Up: controller.AddAction("YourM8", PlayerAction.Top); break;
                case Keys.NumPad4: controller.AddAction("YourM8", PlayerAction.BombLeft); break;
                case Keys.NumPad6: controller.AddAction("YourM8", PlayerAction.BombRight); break;
                case Keys.NumPad2: controller.AddAction("YourM8", PlayerAction.BombBottom); break;
                case Keys.NumPad8: controller.AddAction("YourM8", PlayerAction.BombTop); break;
            }
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

            if (developerMode)
            {
                DeveloperModeButtonClick(e);
            }            

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }
    }
}
