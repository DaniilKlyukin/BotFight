using BomberMansTCPFormsLibrary.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
