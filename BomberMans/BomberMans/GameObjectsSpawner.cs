using BomberMansTCPFormsLibrary;

namespace BomberMans
{
    public class GameObjectsSpawner
    {
        static Random rnd = new Random();

        public double PowderBonusMean { get; set; } = 0.8;
        public double PowderBonusStd { get; set; } = 0.9;

        public double DiamondMean { get; set; } = 0.15;
        public double DiamondStd { get; set; } = 0.3;

        public double BuildBonusMean { get; set; } = 0.2;
        public double BuildBonusStd { get; set; } = 0.3;

        public double LandMineMean { get; set; } = 0.5;
        public double LandMineStd { get; set; } = 0.7;

        public double SuperPowerMean { get; set; } = 0.175;
        public double SuperPowerStd { get; set; } = 0.3;

        public double WallSpawnProbability { get; set; } = 0.2;
        public double HeavyWallSpawnProbability { get; set; } = 0.17;

        private int GetSpawnCount(double mean, double std)
        {
            var val = rnd.NextNormal(mean, std);
            if (val <= 0)
                return 0;

            return (int)Math.Round(val);
        }

        public int GetSpawnPowdersCount() => GetSpawnCount(PowderBonusMean, PowderBonusStd);
        public int GetSpawnBuildsCount() => GetSpawnCount(BuildBonusMean, BuildBonusStd);
        public int GetSpawnLandMinesCount() => GetSpawnCount(LandMineMean, LandMineStd);
        public int GetSpawnSuperPowerCount() => GetSpawnCount(SuperPowerMean, SuperPowerStd);
        public int GetSpawnDiamondsCount() => GetSpawnCount(DiamondMean, DiamondStd);
    }
}
