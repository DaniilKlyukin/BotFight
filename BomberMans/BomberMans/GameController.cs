using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMans
{

    public class GameController
    {
        Dictionary<string, Player> players { get; set; } = new();
        Dictionary<(int, int), GameObject> gameObjects = new();
        Dictionary<string, PlayerAction> actions = new();

        public int? RessurectionTime { get; set; } = null;

        public double WallSpawnProbability { get; set; } = 0.2;


        public int MapSize { get; private set; }
        Random rnd = new Random();
        GameObjectsSpawner spawner = new GameObjectsSpawner();

        public GameController()
        {
            gameObjects = new Dictionary<(int, int), GameObject>();
            actions = new Dictionary<string, PlayerAction>();
        }

        public void AddAction(string ip, PlayerAction act)
        {
            if (!players.ContainsKey(ip))
                return;

            if (actions.ContainsKey(ip))
                actions[ip] = act;
            else
                actions.Add(ip, act);
        }

        public bool ContainsPlayer(string ipPort)
        {
            return players.ContainsKey(ipPort);
        }

        public void RemovePlayer(string ip)
        {
            var ps = gameObjects.Where(x => x.Value is Player p && p.IP == ip).ToArray();

            if (!ps.Any())
                return;

            foreach (var g in ps)
            {
                var p = (Player)g.Value;
                gameObjects.Remove(g.Key);
                players.Remove(p.IP);
            }
        }

        private List<(int, int)> GetValidCoordinates()
        {
            var validCells = new List<(int, int)>();

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    if (!gameObjects.ContainsKey((i, j)))
                    {
                        validCells.Add((i, j));
                    }
                }
            }

            return validCells;
        }

        public void AddPlayer(string ip, string name)
        {
            if (ContainsPlayer(ip))
                return;

            var validCells = GetValidCoordinates();

            var rndIndex = rnd.Next(validCells.Count);
            var (i0, j0) = validCells[rndIndex];

            var p = new Player(i0, j0, ip, name, 0, 0);

            players.Add(ip, p);
            gameObjects.Add((i0, j0), p);
        }

        public void PlacePlayer(string ip)
        {
            //Если игрока нет в игре то выходим
            if (!players.ContainsKey(ip))
                return;

            var ps = gameObjects.Where(x => x.Value is Player p && p.IP == ip).ToArray();

            //Если игрок уже размещен на карте то выходим
            if (ps.Any())
                return;

            var validCells = GetValidCoordinates();

            var rndIndex = rnd.Next(validCells.Count);
            var (i0, j0) = validCells[rndIndex];

            var p = players[ip];
            p.i = i0;
            p.j = j0;

            gameObjects.Add((i0, j0), p);
        }

        public void RessurectPlayer(string ip)
        {
            if (!players.ContainsKey(ip))
                return;

            var p = players[ip];

            p.TimeToRessurect = null;
            p.IsAlive = true;
            PlacePlayer(p.IP);
        }

        public void SetPlayerName(string ip, string name)
        {
            if (!players.ContainsKey(ip))
                return;

            players[ip].Name = name;
        }

        public void SetupRandomLevel(int mapSize)
        {
            MapSize = mapSize;
            gameObjects = new Dictionary<(int, int), GameObject>();

            for (int i = 0; i < MapSize; i++)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    var r = rnd.NextDouble();
                    if (i == 0 || j == 0 || i == MapSize - 1 || j == MapSize - 1 || r <= WallSpawnProbability)
                    {
                        gameObjects.Add((i, j), new Wall(i, j));
                    }
                }
            }

            PlaceObjects(2, 2, 5);
        }

        public void SetupMeshLevel(int mapSize)
        {
            MapSize = mapSize;
            gameObjects = new Dictionary<(int, int), GameObject>();

            for (int i = 0; i < MapSize; i += 3)
            {
                for (int j = 0; j < MapSize; j++)
                {
                    var r = rnd.NextDouble();
                    if (r <= 1 && !gameObjects.ContainsKey((i, j)))
                    {
                        gameObjects.Add((i, j), new Wall(i, j));
                    }
                }
            }

            for (int j = 0; j < MapSize; j += 3)
            {
                for (int i = 0; i < MapSize; i++)
                {
                    var r = rnd.NextDouble();
                    if (r <= 1 && !gameObjects.ContainsKey((i, j)))
                    {
                        gameObjects.Add((i, j), new Wall(i, j));
                    }
                }
            }

            PlaceObjects(2, 2, 5);
        }

        private void PlaceObjects(int minesCount = 1, int buildsCount = 1, int powderCount = 1, int superPowerCount = 1)
        {
            var totalCount = minesCount + buildsCount + powderCount + superPowerCount;

            if (totalCount == 0)
                return;

            var validCells = GetValidCoordinates();

            rnd.Shuffle(validCells);

            var cells = new Queue<(int, int)>(validCells.Take(totalCount).ToArray());

            while (true)
            {
                if (!cells.Any())
                    return;

                var (i, j) = cells.Dequeue();

                if (minesCount > 0)
                {
                    gameObjects.Add((i, j), new LandMine(i, j));
                    minesCount--;
                }
                else if (buildsCount > 0)
                {
                    gameObjects.Add((i, j), new BuildBonus(i, j));
                    buildsCount--;
                }
                else if (powderCount > 0)
                {
                    gameObjects.Add((i, j), new BombPowerBonus(i, j));
                    powderCount--;
                }
                else if (superPowerCount > 0)
                {
                    gameObjects.Add((i, j), new SuperPowerBonus(i, j));
                    superPowerCount--;
                }
            }
        }

        public GameObject? GetObject(int i, int j)
        {
            if (!gameObjects.ContainsKey((i, j)))
                return null;

            return gameObjects[(i, j)];
        }

        private void DoPlayerAction(string ip, PlayerAction act)
        {
            switch (act)
            {
                case PlayerAction.None:
                    break;
                case PlayerAction.Left: MovePlayer(ip, 0, -1); break;
                case PlayerAction.Right: MovePlayer(ip, 0, 1); break;
                case PlayerAction.Top: MovePlayer(ip, -1, 0); break;
                case PlayerAction.Bottom: MovePlayer(ip, 1, 0); break;
                case PlayerAction.BombLeft: BombAction(ip, 0, -1); break;
                case PlayerAction.BombRight: BombAction(ip, 0, 1); break;
                case PlayerAction.BombTop: BombAction(ip, -1, 0); break;
                case PlayerAction.BombBottom: BombAction(ip, 1, 0); break;
                default:
                    break;
            }
        }

        private void MovePlayer(string ip, int di, int dj)
        {
            var p = players[ip];
            var i0 = p.i;
            var j0 = p.j;

            if (!CanStep(i0 + di, j0 + dj, out var g))
                return;

            p.i += di;
            p.j += dj;

            gameObjects.Remove((i0, j0));

            if (g != null)
                gameObjects.Remove((i0 + di, j0 + dj));

            gameObjects.Add((i0 + di, j0 + dj), p);

            if (g is BombPowerBonus)
            {
                p.BombPowerModificator++;
                p.Score++;
            }
            else if (g is LandMine m)
            {
                MakeMineExplosion(m);
            }
            else if (g is BuildBonus)
            {
                BuildWallBox(i0 + di, j0 + dj, p.IP);
            }
            else if (g is SuperPowerBonus)
            {
                p.SuperPowerTimeRemain = SuperPowerBonus.BasicSuperPowerTime;
            }
        }

        private void BombAction(string ip, int di, int dj)
        {
            var p = players[ip];
            var i0 = p.i;
            var j0 = p.j;

            if (p.SuperPowerTimeRemain != null)
            {
                SuperPowerShot(i0, j0, di, dj, ip);

                return;
            }

            if (!IsValidEmptyCell(i0 + di, j0 + dj))
                return;

            var b = new Bomb(i0 + di, j0 + dj, Bomb.BasicPower + p.BombPowerModificator, Bomb.BasicTimeRemain, ip);

            gameObjects.Add((i0 + di, j0 + dj), b);
        }

        private void SuperPowerShot(int i0, int j0, int di, int dj, string ip)
        {
            var p = players[ip];

            var cellI = i0 + di;
            var cellJ = j0 + dj;

            var brokenWalls = 0;

            for (int k = 0; k < 2 * (Bomb.BasicPower + p.BombPowerModificator); k++)
            {
                var brokenWall = gameObjects.ContainsKey((cellI, cellJ)) && gameObjects[(cellI, cellJ)] is Wall;

                AddExplosion(cellI, cellJ, ip);
                cellI += di;
                cellJ += dj;

                if (brokenWall)
                    brokenWalls++;

                if (brokenWalls == SuperPowerBonus.MaxBrokenWalls)
                    break;
            }
        }

        private void AddExplosion(int i, int j, string? owner = null)
        {
            if (!IsValidCell(i, j))
                return;

            if (gameObjects.ContainsKey((i, j)))
            {
                var obj = gameObjects[(i, j)];

                if (obj is Player p)
                {
                    if (p.SuperPowerTimeRemain != null)
                        return;

                    KillPlayer(p.IP, owner);

                    gameObjects[(i, j)] = new Explosion(i, j, owner ?? "");
                }
                else
                {
                    gameObjects[(i, j)] = new Explosion(i, j, owner ?? "");

                    if (obj is Bomb b)
                    {
                        MakeBombExplosion(b, owner);
                    }
                    else if (obj is LandMine m)
                    {
                        MakeMineExplosion(m, owner);
                    }
                }
            }
            else
            {
                gameObjects.Add((i, j), new Explosion(i, j, owner ?? ""));
            }
        }

        private void KillPlayer(string ip, string? killerIp = null)
        {
            var p = players[ip];

            p.IsAlive = false;
            p.TimeToRessurect = RessurectionTime;
            gameObjects.Remove((p.i, p.j));

            if (killerIp == null)
                return;

            if (killerIp != p.IP)
                players[killerIp].Score += 5;
        }

        private void MakeMineExplosion(LandMine m, string? owner = null)
        {
            AddExplosion(m.i, m.j);

            for (int i = m.i - LandMine.Power; i <= m.i + LandMine.Power; i++)
            {
                for (int j = m.j - LandMine.Power; j <= m.j + LandMine.Power; j++)
                {
                    AddExplosion(i, j, owner);
                }
            }
        }

        private void BuildWallBox(int i0, int j0, string? owner = null)
        {
            for (int i = i0 - BuildBonus.HalfSize; i <= i0 + BuildBonus.HalfSize; i++)
            {
                var j1 = j0 - BuildBonus.HalfSize;

                if (IsValidEmptyCell(i, j1))
                    gameObjects.Add((i, j1), new Wall(i, j1));

                var j2 = j0 + BuildBonus.HalfSize;

                if (IsValidEmptyCell(i, j2))
                    gameObjects.Add((i, j2), new Wall(i, j2));
            }

            for (int j = j0 - BuildBonus.HalfSize; j <= j0 + BuildBonus.HalfSize; j++)
            {
                var i1 = i0 - BuildBonus.HalfSize;

                if (IsValidEmptyCell(i1, j))
                    gameObjects.Add((i1, j), new Wall(i1, j));

                var i2 = i0 + BuildBonus.HalfSize;

                if (IsValidEmptyCell(i2, j))
                    gameObjects.Add((i2, j), new Wall(i2, j));
            }
        }

        private void MakeBombExplosion(Bomb b, string? owner = null)
        {
            gameObjects[(b.i, b.j)] = new Explosion(b.i, b.j, owner ?? "");

            for (int dj = 1; dj <= b.Power; dj++)
            {
                var stop = gameObjects.ContainsKey((b.i, b.j + dj)) && gameObjects[(b.i, b.j + dj)] is Wall;
                AddExplosion(b.i, b.j + dj, owner);

                if (stop)
                    break;
            }

            for (int dj = 1; dj <= b.Power; dj++)
            {
                var stop = gameObjects.ContainsKey((b.i, b.j - dj)) && gameObjects[(b.i, b.j - dj)] is Wall;
                AddExplosion(b.i, b.j - dj, owner);

                if (stop)
                    break;
            }

            for (int di = 1; di <= b.Power; di++)
            {
                var stop = gameObjects.ContainsKey((b.i + di, b.j)) && gameObjects[(b.i + di, b.j)] is Wall;
                AddExplosion(b.i + di, b.j, owner);

                if (stop)
                    break;
            }

            for (int di = 1; di <= b.Power; di++)
            {
                var stop = gameObjects.ContainsKey((b.i - di, b.j)) && gameObjects[(b.i - di, b.j)] is Wall;
                AddExplosion(b.i - di, b.j, owner);

                if (stop)
                    break;
            }
        }

        public bool CanStep(int i, int j, out GameObject? g)
        {
            g = null;

            if (!IsValidCell(i, j))
                return false;

            if (gameObjects.ContainsKey((i, j)))
                g = gameObjects[(i, j)];
            else
                return true;

            return g is BombPowerBonus || g is LandMine || g is BuildBonus || g is SuperPowerBonus;
        }

        public bool IsValidEmptyCell(int i, int j)
        {
            return IsValidCell(i, j) && !gameObjects.ContainsKey((i, j));
        }

        public bool IsValidCell(int i, int j)
        {
            return i >= 0 && i <= MapSize - 1 && j >= 0 && j <= MapSize - 1;
        }

        public void GameStep()
        {
            gameObjects = gameObjects.Where(g => g.Value is not Explosion).ToDictionary(g => g.Key, g => g.Value);

            var bombs = gameObjects.Where(g => g.Value is Bomb).ToList();
            foreach (var ((i, j), g) in bombs)
            {
                var b = (Bomb)g;

                b.Tick();

                if (b.TimeRemain == 0)
                {
                    MakeBombExplosion(b);
                }
            }

            foreach (var (ip, act) in actions)
            {
                if (players.ContainsKey(ip) && players[ip].IsAlive)
                    DoPlayerAction(ip, act);
            }

            if (RessurectionTime != null)
            {
                foreach (var (ip, p) in players.Where(t => t.Value.TimeToRessurect != null).ToArray())
                {
                    p.TimeToRessurect--;

                    if (p.TimeToRessurect == 0)
                    {
                        RessurectPlayer(p.IP);
                    }
                }
            }

            foreach (var (ip, p) in players.Where(t => t.Value.SuperPowerTimeRemain != null).ToArray())
            {
                p.SuperPowerTimeRemain--;

                if (p.SuperPowerTimeRemain == 0)
                    p.SuperPowerTimeRemain = null;
            }

            PlaceObjects(
                spawner.GetSpawnLandMinesCount(),
                spawner.GetSpawnBuildsCount(),
                spawner.GetSpawnPowdersCount(),
                spawner.GetSpawnSuperPowerCount());
        }

        public GameObject?[,] GetMap()
        {
            var map = new GameObject?[MapSize, MapSize];

            foreach (var g in gameObjects)
            {
                var (i, j) = g.Key;

                if (!IsValidCell(i, j))
                    continue;

                map[i, j] = g.Value;
            }

            return map;
        }

        public IList<Player> GetPlayers()
        {
            return players.Values.ToList();
        }
    }
}
