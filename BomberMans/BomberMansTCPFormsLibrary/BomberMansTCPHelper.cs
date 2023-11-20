using System.Net.Sockets;
using System.Net;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMansTCPFormsLibrary
{
    public static class BomberMansTCPHelper
    {
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var name in host.AddressList.Reverse())
            {
                if (name.AddressFamily == AddressFamily.InterNetwork)
                {
                    return name.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }

        public static void DrawMap(PictureBox pb, GameObject?[,] map, string? watchPlayerName = null, int cellSize = 20)
        {
            var mainBitmap = new Bitmap(map.GetLength(0) * cellSize, map.GetLength(1) * cellSize);
            using Font myFont = new Font("Arial", 12, FontStyle.Bold);
            using Graphics g = Graphics.FromImage(mainBitmap);

            var players = new List<Player>();

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    var c = map[i, j];
                    var point = new Point(j * cellSize, i * cellSize);

                    var bmp = c switch
                    {
                        Wall => Resource.wall,
                        Player => ((Player)c).SuperPowerTimeRemain == null ? Resource.player : Resource.SuperPlayer,
                        Bomb => Resource.bomb,
                        Explosion => Resource.explosion,
                        BombPowerBonus => Resource.bombPowerBonus,
                        LandMine => Resource.mine,
                        BuildBonus => Resource.build,
                        SuperPowerBonus => Resource.SuperPowerBonus,
                        _ => null
                    };

                    if (bmp == null)
                        continue;

                    g.DrawImage(new Bitmap(bmp, new Size(cellSize, cellSize)), point);

                    if (c is Player p)
                        players.Add(p);
                }
            }

            foreach (var p in players)
            {
                var brush = watchPlayerName != null && watchPlayerName == p.Name ? Brushes.Green : Brushes.Red;

                var point = new Point(p.j * cellSize, (p.i + 1) * cellSize);
                g.DrawString(p.Name, myFont, brush, point);
            }

            pb.Invoke(() => pb.Image = mainBitmap);
        }

        public static void UpdatePlayersListBox(ListBox playersListBox, IList<PlayerInfo> playersInfo)
        {
            playersListBox.Items.Clear();

            foreach (var p in playersInfo.OrderByDescending(p => p.Score))
            {
                playersListBox.Items.Add(p);
            }
        }
    }
}