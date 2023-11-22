using System.Net.Sockets;
using System.Net;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMansTCPFormsLibrary
{
    public static class BomberMansTCPHelper
    {
        public static string ParseIP(string ipPort) => ipPort.Split(':')[0];

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
                    var go = map[i, j];
                    var point = new Point(j * cellSize, i * cellSize);

                    var bmp = GetBitmap(go, cellSize);

                    if (bmp == null)
                        continue;

                    g.DrawImage(bmp, point);

                    if (go is Player p)
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

        private static Bitmap GetBitmap(GameObject go, int cellSize = 20)
        {
            switch (go)
            {
                case Wall:
                    return CombineImages(cellSize, Resource.Wall);
                case HeavyWall:
                    return CombineImages(cellSize, Resource.HeavyWall);
                case Player p:
                    return CombineImages(
                        cellSize,
                        Resource.Grass,
                        p.SuperPowerTimeRemain == null ? Resource.Player : Resource.SuperPlayer);
                case Bomb:
                    return CombineImages(cellSize, Resource.Grass, Resource.Bomb);
                case Explosion:
                    return CombineImages(cellSize, Resource.Grass, Resource.Explosion);
                case Powder:
                    return CombineImages(cellSize, Resource.Grass, Resource.Powder);
                case LandMine:
                    return CombineImages(cellSize, Resource.Grass, Resource.Mine);
                case BuildBonus:
                    return CombineImages(cellSize, Resource.Grass, Resource.Build);
                case SuperPowerBonus:
                    return CombineImages(cellSize, Resource.Grass, Resource.SuperPowerBonus);
                case Diamond:
                    return CombineImages(cellSize, Resource.Grass, Resource.Diamond);

                default: return CombineImages(cellSize, Resource.Grass);
            }
        }

        private static Bitmap CombineImages(int cellSize = 20, params Bitmap[] bmps)
        {
            var result = new Bitmap(cellSize, cellSize);
            var s = new Size(cellSize, cellSize);
            using Graphics g = Graphics.FromImage(result);
            var p0 = new Point(0, 0);

            foreach (var bmp in bmps)
            {
                g.DrawImage(new Bitmap(bmp, s), p0);
            }

            return result;
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