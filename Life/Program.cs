using System;
using System.Threading;

namespace Life
{
    class Program
    {
        static void Main()
        {
            var lifeManager = new LifeManager(120, 450);
            var counter = 1;

            do
            {
                Console.SetCursorPosition(0, 0);
                lifeManager.PrintCellBoard();
                Console.WriteLine($"Iteration {counter}");
                //Thread.Sleep(10);
                counter++;
                lifeManager.ApplyLifeRules();

            } while (true);
        }
    }
}
