using PropertyTools.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PFManager.Model
{
    public class User
    {
        [Browsable(false)]
        public Guid Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string IdNumber { get; set; } = null!;
        public string ClassName { get; set; } = null!;
        public bool AllowToSign { get; set; }
        public bool AllowedElectric { get; set; }
        public bool IsAdmin { get; set; }
    }
}
