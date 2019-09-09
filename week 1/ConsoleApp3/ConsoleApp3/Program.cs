using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Animals;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int finishPOsition = 50;

            var a = new DogRacer(0);

            //var racers = new List<Animal>();
            var racers = new List<IRacer>();            

            racers.Add(a);
            racers.Add(new CatRacer(1));
            racers.Add(new Car(2));
         
            while(!racers.Any(r=> r.Position >= finishPOsition))
            {
                Console.SetCursorPosition(0, 0);

                foreach(var racer in racers)
                {
                    racer.Move();

                    var lineNumber = racer.LineNumber;
                    
                    Draw(racer.Position, lineNumber, racer);

                    if(racer.Position >= finishPOsition)
                    {
                        Console.WriteLine(racer.WinWords());

                        break;
                    }
                        
                    
                }                                               

                

                Thread.Sleep(300);
            }
            
        }

        static void Draw(int position , int lineNumber, ISignable sign)
        {
            Console.SetCursorPosition(0, lineNumber);

            for (int i = 0; i < position; i++)
            {
                Console.Write("_");
            }
            if(sign != null )
                Console.Write(sign.Sign);
        }
    }


    
}
