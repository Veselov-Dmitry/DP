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
    
    public partial class Documents
    {
        public string Document_Number { get; set; }
        public int Document_ID { get; set; }
        public Nullable<System.DateTime> Document_Date { get; set; }
        public Nullable<int> Document_type_ID { get; set; }
        public Nullable<int> PS_ID { get; set; }
        public byte[] DocumentFile { get; set; }
        public string File_Name { get; set; }
    
        public virtual DocumentTypes DocumentTypes { get; set; }
        public virtual PrgSystems PrgSystems { get; set; }
    }
}
