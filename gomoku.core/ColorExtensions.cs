using System;
using System.Collections.Generic;
using System.Text;

namespace gomoku.core
{
    public static class ColorExtensions
    {
        public static Color Inverse(this Color color)
        {
            switch (color)
            {
                case Color.White: return Color.Black;
                case Color.Black: return Color.White;
                default: return Color.Undefined;
            }
        }

        public static string PrintColor(this Color color)
        {
            switch (color)
            {
                case Color.Black: return " X ";
                case Color.White: return " 0 ";
                default: return " _ ";
            }
        }
    }
}
