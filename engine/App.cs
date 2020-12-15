using System;

namespace SMF.engine
{
    class App
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Pseudo-Gaem Engine started!");
            GameInstance game = new GameInstance("Smell Mah Fishy", EResolution.E1600X900);
            game.Loop();
        }
    }
}
