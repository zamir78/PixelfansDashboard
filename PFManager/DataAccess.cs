using PFManager.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PFManager
{
    public class DataAccess
    {
        public static IDocumentDataBaseAPI DataBaseAPI { get; private set; } = null!;

        public const string Users = "Users";
        public const string Feedings = "Feedings";

        public DataAccess(IDocumentDataBaseAPI dataBaseAPI)
        {
            DataBaseAPI = dataBaseAPI;
        }
    }
}
