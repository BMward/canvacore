using System;
using System.Collections.Generic;

namespace CanvacoreLib
{
    public class Canvas
    {
        public List<Pixel> Pixels = new List<Pixel>();
        public double Width { get; set; }
        public double Height { get; set; }
        public int PixelCount { get; set; }
    }
}
