using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMans
{

    public class GameController
    {
        Dictionary<string, Player> players { get; set; } = new();
        Dictionary<(int, int), GameObject> gameObjects = new();
        //Время необходимо, чтобы отслеживать кто прислал ответ первый
        Dictionary<string, (DateTime, PlayerAction)> actions = new();

        public int? RessurectionTime { get; set; } = null;

        public int MapSize { get; private set; }
        Random rnd = new Random();
        GameObjectsSpawner spawner = new GameObjectsSpawner();

        public GameController(int mapSize = 40)
        {
            MapSize = mapSize;
            gameObjects = new Dictionary<(int, int), GameObject>();
            actions = new Dictionary<string, (DateTime, PlayerAction)>();
        }

        public void GenerateRandomMap(int mapSize = 40)
        {
            MapSize = mapSize;
            gameObjects = new Dictionary<(int, int), GameObject>();

            MapGenerator.AddMapBorder(gameObjects, mapSize);
            MapGenerator.AddRandomWalls(gameObjects, mapSize, spawner);

            MapGenerator.PlaceObjects(
                gameObjects, mapSize, 7, 4, 15, 1, 1);
        }

        public void GenerateMeshMap(int mapSize = 40, int meshCellSize = 3)
        {
            MapSize = mapSize;
            gameObjects = new Dictionary<(int, int), GameObject>();
            var dirs = new[] { (meshCellSize + 1, 1), (1, meshCellSize + 1) };

            MapGenerator.AddMapBorder(gameObjects, mapSize);

            foreach (var (dx, dy) in dirs)
            {
                for (int i = 0; i < mapSize; i += dx)
                {
                    for (int j = 0; j < mapSize; j += dy)
                    {
                        if (gameObjects.ContainsKey((i, j)))
                            continue;

                        var hw = rnd.NextDouble();
                        if (hw <= spawner.HeavyWallSpawnProbability)
                        {
                            gameObjects.Add((i, j), new HeavyWall(i, j));
                        }
                        else
                        {
                            gameObjects.Add((i, j), new Wall(i, j));
                        }
                    }
                }
            }

            MapGenerator.PlaceObjects(
                gameObjects, mapSize, 7, 4, 15, 1, 1);
        }

        public void AddAction(string ip, PlayerAction act)
        {
            if (!players.ContainsKey(ip))
                return;

            if (actions.ContainsKey(ip))
                actions[ip] = (DateTime.Now, act);
            else
                actions.Add(ip, (DateTime.Now, act));
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

        public void AddPlayer(string ip, string name)
        {
            if (ContainsPlayer(ip))
                return;

            var validCells = MapGenerator.GetValidPlayerCoordinates(gameObjects, MapSize).ToList();

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

            var validCells = MapGenerator.GetValidPlayerCoordinates(gameObjects, MapSize).ToList();

            var rndIndex = rnd.Next(validCells.Count);
            var (i0, j0) = validCells[rndIndex];

            var p = players[ip];
            p.i = i0;
            p.j = j0;

            gameObjects.Add((i0, j0), p);
        }

        public void ResurrectPlayer(string ip)
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
                case PlayerAction.Left: MovePlayer(ip, 0, -1); break;
                case PlayerAction.Right: MovePlayer(ip, 0, 1); break;
                case PlayerAction.Top: MovePlayer(ip, -1, 0); break;
                case PlayerAction.Bottom: MovePlayer(ip, 1, 0); break;
                case PlayerAction.BombLeft: BombAction(ip, 0, -1); break;
                case PlayerAction.BombRight: BombAction(ip, 0, 1); break;
                case PlayerAction.BombTop: BombAction(ip, -1, 0); break;
                case PlayerAction.BombBottom: BombAction(ip, 1, 0); break;
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

            if (g is Powder)
            {
                p.BombPowerModificator++;
                p.Score += Powder.Score;
            }
            else if (g is Diamond)
            {
                p.Score += Diamond.Score;
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

            var broken = 0;

            for (int k = 0; k < 2 * (Bomb.BasicPower + p.BombPowerModificator); k++)
            {
                var coords = (cellI, cellJ);
                var brokenObj = gameObjects.ContainsKey(coords) && gameObjects[coords].BlockExplosion;

                AddExplosion(cellI, cellJ, ip);
                cellI += di;
                cellJ += dj;

                if (brokenObj)
                    broken++;

                if (broken > SuperPowerBonus.MaxPenetration || gameObjects.ContainsKey(coords) && !gameObjects[coords].Destructible && gameObjects[coords].BlockExplosion)
                    break;
            }
        }

        private void AddExplosion(int i, int j, string? owner = null)
        {
            if (!IsValidCell(i, j))
                return;

            if (gameObjects.ContainsKey((i, j)))
            {
                var go = gameObjects[(i, j)];

                if (go is Player p)
                {
                    if (p.SuperPowerTimeRemain != null)
                        return;

                    KillPlayer(p.IP, owner);

                    gameObjects[(i, j)] = new Explosion(i, j, owner ?? "");
                }
                else if (go.Destructible)
                {
                    gameObjects[(i, j)] = new Explosion(i, j, owner ?? "");

                    if (go is Bomb b)
                    {
                        MakeBombExplosion(b, b.TimeRemain == 0 ? b.Owner : owner);
                    }
                    else if (go is LandMine m)
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
            var dirs = new[] { (-1, 0), (1, 0), (0, -1), (0, 1) };

            var visited = new Dictionary<(int, int), int>();
            var queue = new Queue<(int, int, int)>();
            queue.Enqueue((m.i, m.j, 0));

            while (queue.Any())
            {
                var (i, j, dist) = queue.Dequeue();

                if (visited.TryGetValue((i, j), out var nodeDist))
                {
                    if (dist < nodeDist)
                        visited[(i, j)] = dist;

                    continue;
                }

                if (dist > LandMine.Power)
                    continue; 

                if (gameObjects.TryGetValue((i, j), out var go) && go.BlockExplosion)
                {
                    if (go.Destructible)
                        visited.Add((i, j), dist);

                    continue;
                }

                visited.Add((i, j), dist);

                foreach (var (di, dj) in dirs)
                {
                    queue.Enqueue((i + di, j + dj, dist + 1));
                }
            }

            foreach (var (i, j) in visited.Keys)
            {
                AddExplosion(i, j, owner);
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

            var directions = new List<(int, int)>() { (1, 0), (-1, 0), (0, 1), (0, -1) };

            foreach (var (dx, dy) in directions)
            {
                var i = b.i + dx;
                var j = b.j + dy;
                for (int delta = 1; delta <= b.Power; delta++)
                {
                    var stop = gameObjects.ContainsKey((i, j)) && gameObjects[(i, j)].BlockExplosion;
                    AddExplosion(i, j, owner);

                    if (stop)
                        break;

                    i += dx;
                    j += dy;
                }
            }
        }

        private bool CanStep(int i, int j, out GameObject? g)
        {
            g = null;

            if (!IsValidCell(i, j))
                return false;

            if (gameObjects.ContainsKey((i, j)))
                g = gameObjects[(i, j)];
            else
                return true;

            return g.PlayerCanStep;
        }

        private bool IsValidEmptyCell(int i, int j)
        {
            return IsValidCell(i, j) && !gameObjects.ContainsKey((i, j));
        }

        private bool IsValidCell(int i, int j)
        {
            return i >= 0 && i <= MapSize - 1 && j >= 0 && j <= MapSize - 1;
        }

        public void GameStep()
        {
            gameObjects = gameObjects.Where(g => g.Value is not Explosion).ToDictionary(g => g.Key, g => g.Value);

            foreach (var (ip, p) in players.Where(t => t.Value.SuperPowerTimeRemain != null).ToArray())
            {
                p.SuperPowerTimeRemain--;

                if (p.SuperPowerTimeRemain < 0)
                    p.SuperPowerTimeRemain = null;
            }

            var bombs = gameObjects.Select(g => g.Value).OfType<Bomb>().ToList();
            foreach (var b in bombs)
            {
                b.Tick();
            }

            foreach (var b in bombs.Where(b => b.TimeRemain == 0))
            {
                MakeBombExplosion(b, b.Owner);
            }

            foreach (var (ip, (time, act)) in actions.OrderBy(v => v.Value.Item1))
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
                        ResurrectPlayer(p.IP);
                    }
                }
            }

            
            MapGenerator.PlaceObjects(
                gameObjects,
                MapSize,
                spawner.GetSpawnLandMinesCount(),
                spawner.GetSpawnBuildsCount(),
                spawner.GetSpawnPowdersCount(),
                spawner.GetSpawnSuperPowerCount(),
                spawner.GetSpawnDiamondsCount());
            
            actions.Clear();
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

        public void PlaceGameObject(GameObject go)
        {
            if (!gameObjects.ContainsKey((go.i, go.j)))
            {
                gameObjects.Add((go.i, go.j), go);

                if (go is Player p)
                {
                    if (players.ContainsKey(p.IP))
                        players[p.IP] = p;
                    else
                        players.Add(p.IP, p);
                }
            }
        }

        public void RemoveGameObject(int i, int j)
        {
            if (gameObjects.TryGetValue((i, j), out var go))
            {
                gameObjects.Remove((i, j));
                if (go is Player p)
                {
                    players.Remove(p.IP);
                }
            }
        }
    }
}
