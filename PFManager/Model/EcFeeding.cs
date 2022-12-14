using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFManager.Model
{
    public class EcFeeding
    {
        [Browsable(false)]
        public Guid Id { get; set; }

        public string KKS { get; set; } = null!;
        public string DeviceName { get; set; } = null!;
        public string ERoom { get; set; } = null!;
        public string Distribution { get; set; } = null!;
        public string Cabinet { get; set; } = null!;
        public string Fuse { get; set; } = null!;
    }
}
