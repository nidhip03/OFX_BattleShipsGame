using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OFX_BattleShipsGame.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Start();

            Console.ReadLine();
        }
    }
}
