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
        public PixelCollectionData Data { get; set; }

        public PixelCollection()
        {
            PixelList = new List<Pixel>();
            Data = new PixelCollectionData();
        }

        public bool IsPlaying { get; set; }
    }

    public class Pixel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsLit { get; set; }
    }

    public class PixelCollectionData
    {
        public int Columns { get; set; }
        public int Rows { get; set; }
    }
}
