using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public class Dog : Animal, ISignable
    {
        public Dog()
        {
            Speed = 2;
        }

        public string Sign => "@";                    

        public override string Say()
        {
            return "Gaf gaf";
        }        
    }

    public class DogRacer : Dog, IRacer
    {
        public DogRacer(int lineNumber)
        {
            LineNumber = lineNumber;
        }

        public int LineNumber { get; }

        public string WinWords()
        {
            return Say();
        }
    }

    public class Cat : Animal, ISignable
    {
        public Cat()
        {
            Speed = 3;
        }

        public string Sign => "*";

        public override string Say()
        {
            return "Meow meow";
        }
    }

    public class CatRacer : Cat, IRacer
    {
        public CatRacer(int lineNumber)
        {
            LineNumber = lineNumber;
        }

        public int LineNumber { get; }

        public string WinWords()
        {
            return base.Say();
        }
    }
}
