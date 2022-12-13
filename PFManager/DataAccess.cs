using PFManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFManager
{
    public class DataAccess
    {
        public static IDocumentDataBaseAPI DataBaseAPI { get; private set; } = null!;

        public DataAccess(IDocumentDataBaseAPI dataBaseAPI)
        {
            DataBaseAPI = dataBaseAPI;
        }
    }
}
