namespace BomberMansTCPFormsLibrary
{
    public static class RandomExtensions
    {
        public static void Shuffle<T>(this Random rnd, IList<T> array)
        {
            int n = array.Count;
            while (n > 1)
            {
                int k = rnd.Next(n--);
                T temp = array[n];
                array[n] = array[k];
                array[k] = temp;
            }
        }

        public static double NextNormal(this Random rnd, double mean, double std)
        {
            double u1 = 1.0 - rnd.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rnd.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            return mean + std * randStdNormal; //random normal(mean,stdDev^2)         
        }
    }
}
