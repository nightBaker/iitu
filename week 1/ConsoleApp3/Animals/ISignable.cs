using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public interface ISignable
    {
        string Sign { get; }
    }

    public interface IHaveLineNumber
    {
        int LineNumber { get; }
    }

    public interface IRacer: ISignable, IHaveLineNumber
    {
        void Move();

        int Position { get; }

        string WinWords();
    }
}
