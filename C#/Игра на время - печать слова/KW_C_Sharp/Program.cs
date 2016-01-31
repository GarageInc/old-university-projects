using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Game_of_Words
{
    

    class Program
    {
        

        static void Main(string[] args)
        {

            Game g = new Game();
            g.Start();
            
            //Пауза экрана
            Console.ReadKey();
        }
    }
}
