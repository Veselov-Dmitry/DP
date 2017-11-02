using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Model.Interfaces
{


    public interface IGeterModel
    {
        Dictionary<Type, List<object>> Dictionary { get; }
        List<Type> Types { get; }

        Dictionary<Type, List<object>> GetAllTables();
        List<Type> GetAllTypes();
        void SetAllTables(Dictionary<Type, List<object>> dict);
    }
}
