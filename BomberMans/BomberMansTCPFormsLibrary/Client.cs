using BomberMansTCPFormsLibrary.GameObjects;
using SuperSimpleTcp;
using System.Diagnostics;
using System.Text;

namespace BomberMansTCPFormsLibrary
{
    public class Client
    {
        SimpleTcpClient? client;
        public Func<GameObject[,]?, PlayerAction>? SendPlayerAction { get; set; }
        public Action<IList<PlayerInfo>>? UpdatePlayersInfo { get; set; }
        public Action<GameObject[,]?>? Visualize { get; set; }

        public void Connect(string serverIpPort, string playerName)
        {
            client = new(serverIpPort);

            client.Events.DataReceived += DataReceived;

            try
            {
                client.Connect();

                client.Send(new SendPlayerNameCommand(playerName).ConvertToJson());

                Debug.Print($"Вы подключились");
            }
            catch (Exception ex)
            {
                Debug.Print($"Ошибка подключения\n{ex.Message}");
            }
        }

        public void Disconnect()
        {
            if (client is { IsConnected: true })
                client.Disconnect();

            Debug.Print($"Вы отключились");
        }

        public void SendAction(PlayerAction act)
        {
            var command = new SendPlayerActionCommand(act);
            client.Send(command.ConvertToJson());
        }

        private async void DataReceived(object? sender, SuperSimpleTcp.DataReceivedEventArgs args)
        {
            var data = Encoding.UTF8.GetString(args.Data);

            var length = data.Length;

            var command = Command.ParseFromJson(data);

            try
            {
                switch (command)
                {
                    case SendMapCommand c:
                        {
                            var map = c.Map;
                            var info = c.PlayerInfos;

                            var writeInfoTask = Task.Run(() => UpdatePlayersInfo?.Invoke(info));
                            var visualizeTask = Task.Run(() => Visualize?.Invoke(map));
                            var actionTask = Task.Run(() => SendPlayerAction?.Invoke(map));

                            Task.WaitAll(writeInfoTask, visualizeTask, actionTask);

                            SendAction(actionTask.Result ?? PlayerAction.None);
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return;
            }
        }
    }
}