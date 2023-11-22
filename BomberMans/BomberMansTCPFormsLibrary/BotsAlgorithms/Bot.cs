using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMansTCPFormsLibrary.BotsAlgorithms
{
    public abstract class Bot
    {
        public (int, int) StepToDelta(PlayerAction act)
        {
            return act switch
            {
                PlayerAction.Left => (-1, 0),
                PlayerAction.Right => (1, 0),
                PlayerAction.Top => (0, 1),
                PlayerAction.Bottom => (0, -1),
                _ => (0, 0)
            };
        }

        public abstract PlayerAction DoAction(GameObject[,]? map, Player me);

        public IEnumerable<(int, int)> GetMines(GameObject[,]? map)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (map[i, j] is LandMine)
                        yield return (i, j);
                }
            }
        }

        public IEnumerable<Player> GetEnemies(GameObject[,]? map, Player me)
        {
            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == me.i && j == me.j)
                        continue;

                    if (map[i, j] is Player p)
                        yield return p;
                }
            }
        }

        public bool[,] GetBadCoordinates(GameObject[,]? map, Player me)
        {
            var mapBad = new bool[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                {
                    if (i == me.i && j == me.j)
                        continue;

                    if (map[i, j] is Wall
                        || map[i, j] is LandMine
                        || map[i, j] is Bomb
                        || map[i, j] is Player
                        || map[i, j] is BuildBonus)
                        mapBad[i, j] = true;

                    if (map[i, j] is Bomb b && b.TimeRemain == 1)
                    {
                        for (int di = -b.Power; di <= b.Power; di++)
                        {
                            mapBad[i + di, j] = true;
                        }

                        for (int dj = -b.Power; dj <= b.Power; dj++)
                        {
                            mapBad[i, j + dj] = true;
                        }
                    }
                }
            }

            return mapBad;
        }
    }
}
