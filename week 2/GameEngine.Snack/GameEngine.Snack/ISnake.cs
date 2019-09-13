using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Snack
{
    public interface ISnake
    {
        /// <summary>
        /// Name of the snake
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Update map
        /// </summary>
        /// <param name="v"></param>
        /// 
        void UpdateMap(string map);

        /// <summary>
        /// Get next direction of the snake
        /// </summary>
        /// <param name="currentDirection"></param>
        /// <returns></returns>
        SnakeDirection GetNextDirection(SnakeDirection currentDirection);
    }
}
