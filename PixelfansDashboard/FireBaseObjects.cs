using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixelfansDashboard
{
    public class PixelCollection
    {
        public List<Pixel> PixelList { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public PixelCollection()
        {
            PixelList = new List<Pixel>();
        }
    }

    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsLit { get; set; }
    }
}
