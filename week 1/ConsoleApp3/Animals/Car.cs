using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Car : IRacer
    {
        public int Position { get; private set; }

        public string Sign => "o-o";

        public int LineNumber { get; }

        public Car(int line)
        {
            LineNumber = line;
            Position = 0;
        }

        public void Move()
        {
            Position += 5;
        }

        public string WinWords()
        {
            return "--";
        }
    }
}
