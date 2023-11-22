using BomberMansTCPFormsLibrary;
using BomberMansTCPFormsLibrary.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BomberMans
{
    public class MapGenerator
    {
        static Random rnd = new Random();

        public static void AddMapBorder(Dictionary<(int, int), GameObject> map, int mapSize)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map.ContainsKey((i, j)))
                        continue;

                    if (i == 0 || j == 0 || i == mapSize - 1 || j == mapSize - 1)
                    {
                        map.Add((i, j), new HeavyWall(i, j));
                    }
                }
            }
        }

        public static void AddRandomWalls(Dictionary<(int, int), GameObject> map, int mapSize, GameObjectsSpawner spawner)
        {
            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (map.ContainsKey((i, j)))
                        continue;

                    var hw = rnd.NextDouble();
                    var w = rnd.NextDouble();
                    if (hw <= spawner.HeavyWallSpawnProbability)
                    {
                        map.Add((i, j), new HeavyWall(i, j));
                    }
                    else if (w <= spawner.WallSpawnProbability)
                    {
                        map.Add((i, j), new Wall(i, j));
                    }
                }
            }
        }

        public static void PlaceObjects(
            Dictionary<(int, int), GameObject> map,
            int mapSize,
            int minesCount = 1,
            int buildsCount = 1,
            int powderCount = 1,
            int superPowerCount = 1,
            int diamondsCount = 1)
        {
            var totalCount = minesCount + buildsCount + powderCount + superPowerCount + diamondsCount;

            if (totalCount == 0)
                return;

            var validCells = GetValidItemsCoordinates(map, mapSize).ToList();

            rnd.Shuffle(validCells);

            var cells = new Queue<(int, int)>(validCells.Take(totalCount).ToArray());

            while (true)
            {
                if (!cells.Any())
                    return;

                var (i, j) = cells.Dequeue();

                if (minesCount > 0)
                {
                    map.Add((i, j), new LandMine(i, j));
                    minesCount--;
                }
                else if (buildsCount > 0)
                {
                    map.Add((i, j), new BuildBonus(i, j));
                    buildsCount--;
                }
                else if (powderCount > 0)
                {
                    map.Add((i, j), new Powder(i, j));
                    powderCount--;
                }
                else if (superPowerCount > 0)
                {
                    map.Add((i, j), new SuperPowerBonus(i, j));
                    superPowerCount--;
                }
                else if (diamondsCount > 0)
                {
                    map.Add((i, j), new Diamond(i, j));
                    diamondsCount--;
                }
            }
        }

        public static HashSet<(int, int)> GetValidItemsCoordinates(Dictionary<(int, int), GameObject> map, int mapSize)
        {
            var validCells = new HashSet<(int, int)>();

            for (int i = 0; i < mapSize; i++)
            {
                for (int j = 0; j < mapSize; j++)
                {
                    if (!map.ContainsKey((i, j)))
                    {
                        validCells.Add((i, j));
                    }
                }
            }

            return validCells;
        }

        public static HashSet<(int, int)> GetValidPlayerCoordinates(Dictionary<(int, int), GameObject> map, int mapSize)
        {
            var validCells = new HashSet<(int, int)>();

            for (int i = 0; i < mapSize - 1; i++)
            {
                for (int j = 0; j < mapSize - 1; j++)
                {
                    var emptyCells = new List<(int, int)>();

                    for (int di = 0; di < 2; di++)
                    {
                        for (int dj = 0; dj < 2; dj++)
                        {
                            if (!map.ContainsKey((i + di, j + dj)))
                            {
                                emptyCells.Add((i + di, j + dj));
                            }
                        }
                    }

                    if (emptyCells.Count >= 3)
                    {
                        foreach (var cell in emptyCells)
                            validCells.Add(cell);
                    }
                }
            }

            return validCells;
        }

    }
}
