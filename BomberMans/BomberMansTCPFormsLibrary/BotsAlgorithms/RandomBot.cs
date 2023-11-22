using BomberMansTCPFormsLibrary.GameObjects;

namespace BomberMansTCPFormsLibrary.BotsAlgorithms
{
    public class RandomBot : Bot
    {
        Random rnd = new Random();
        public override PlayerAction DoAction(GameObject[,]? map, Player me)
        {
            return (PlayerAction)rnd.Next(9);
        }
    }
}
