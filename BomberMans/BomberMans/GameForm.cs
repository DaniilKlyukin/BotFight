using SuperSimpleTcp;
using BomberMansTCPFormsLibrary;
using System.Diagnostics;
using System.Text;

namespace BomberMans
{

    public partial class GameForm : Form
    {
        SimpleTcpServer server;
        GameController controller;
        const int CellSize = 24;
        const int MapSize = 40;
        HashSet<string> bannedPlayers = new HashSet<string>();

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

                switch (command)
                {
                    case SendPlayerNameCommand c:
                        {
                            if (controller.ContainsPlayer(e.IpPort))
                                controller.SetPlayerName(e.IpPort, c.Name);

                            if (!GameTimer.Enabled)
                            {
                                Invoke(VisualizationUpdate);
                            }
                        }
                        break;
                    case SendPlayerActionCommand c:
                        {
                            controller.AddAction(e.IpPort, c.Action);
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
            controller.RemovePlayer(e.IpPort);

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }

        private void ClientConnected(object? sender, ConnectionEventArgs e)
        {
            if (bannedPlayers.Contains(e.IpPort))
            {
                server.DisconnectClient(e.IpPort);
                return;
            }

            controller.AddPlayer(e.IpPort, e.IpPort);

            if (!GameTimer.Enabled)
            {
                Invoke(VisualizationUpdate);
            }
        }

        private void VisualizationUpdate()
        {
            BomberMansTCPHelper.DrawMap(pictureBox, controller.GetMap(), null, CellSize);
            BomberMansTCPHelper.UpdatePlayersListBox(
                playersListBox,
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
            controller.SetupRandomLevel(MapSize);
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
            var selectedPlayers = playersListBox.SelectedItems;

            foreach (var item in selectedPlayers)
            {
                var player = (PlayerInfo)item;
                server.DisconnectClient(player.IP);
            }
        }

        private void banPlayerButton_Click(object sender, EventArgs e)
        {
            var selectedPlayers = playersListBox.SelectedItems;

            foreach (var item in selectedPlayers)
            {
                var player = (PlayerInfo)item;
                bannedPlayers.Add(player.IP);
                server.DisconnectClient(player.IP);
            }
        }
    }
}
