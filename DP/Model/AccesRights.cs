//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DP.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AccesRights
    {
        public Nullable<int> Sub_System_ID { get; set; }
        public int AccesRightID { get; set; }
        public int PS_ID { get; set; }
    
        public virtual PrgSystems PrgSystems { get; set; }
        public virtual SubSystems SubSystems { get; set; }
    }
}
