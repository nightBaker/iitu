using PixelEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Snack.Extentions
{
    public static class PixelExtensions
    {
        public static bool isWall(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Grey;
        }

        public static bool isEmpty(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Black;
        }

        public static bool isHead(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Magenta;
        }

        public static bool isBody(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Yellow;
        }

        public static bool isFood(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Green;
        }

        public static bool isTrap(this Pixel pixel)
        {
            return pixel == Pixel.Presets.Red;
        }
    }
}
