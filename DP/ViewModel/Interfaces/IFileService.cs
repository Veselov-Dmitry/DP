using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.XML
{
    interface IFileService
    {
        Dictionary<Type,List<object>> Open(string filename);
        void Save(string filename, Dictionary<Type, List<object>> allTables);
    }
}
