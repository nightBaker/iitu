using System;

namespace Animals
{
    public abstract class Animal
    {
        protected int position;

        public int Position {
            get { return position; }
        }

        public int Speed { get; protected set; }

        public Animal() { }

        public abstract string Say();

        public virtual void Move()
        {
            position += Speed;
        }
    }
}
