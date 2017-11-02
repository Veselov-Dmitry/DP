using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Model
{
    public partial class OASUEntity : DbContext
    {
        public OASUEntity(string s)
            : base(s)
        {
        }
    }
}
