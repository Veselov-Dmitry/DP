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
    
    public partial class Developers
    {
        public Nullable<int> Employee_ID { get; set; }
        public int Developer_part_ID { get; set; }
        public Nullable<System.DateTime> Date_start { get; set; }
        public int Record_ID { get; set; }
    
        public virtual Employees Employees { get; set; }
        public virtual RecordHistories RecordHistories { get; set; }
    }
}