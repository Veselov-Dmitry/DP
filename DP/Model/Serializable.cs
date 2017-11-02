using DP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DP.Model
{
    public partial class AccesRights
    {
        public List<object> GetFields()
        {
            List<object> fields = new List<object>();
            fields.Add(this.AccesRightID);
            fields.Add(this.Sub_System_ID);
            fields.Add(this.PS_ID);
            return fields;
        }
    }
    
    public partial class Computers
    {
        public List<object> GetFields()
        {
            List<object> fields = new List<object>();
            fields.Add(this.Inventory_number);
            fields.Add(this.Computer_ID);
            fields.Add(this.Net_Name);
            return fields;
        }


    }

    //[Serializable]
    //[XmlType(nameof(Departments))]
    //public partial class Departments
    //{
    //    [XmlAttribute("Name_Department")]
    //    public string Name_DepartmentCore
    //    {
    //        get => Name_Department;
    //        set => Name_Department = value;
    //    }
    //    [XmlAttribute("Department_ID")]
    //    public int Department_IDCore
    //    {
    //        get => Department_ID;
    //        set => Department_ID = value;
    //    }
    //}

    //[Serializable]
    //[XmlType(nameof(Developers))]
    //public partial class Developers
    //{
    //    [XmlAttribute("Employee_ID")]
    //    public Nullable<int> Employee_ID { get; set; }
    //    [XmlAttribute("Developer_part_ID")]
    //    public int Developer_part_ID { get; set; }
    //    [XmlAttribute("Date_start")]
    //    public Nullable<System.DateTime> Date_start { get; set; }
    //    [XmlAttribute("Record_ID")]
    //    public int Record_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Documents))]
    //public partial class Documents
    //{
    //    [XmlAttribute("Document_Number")]
    //    public string Document_Number { get; set; }
    //    [XmlAttribute("Document_ID")]
    //    public int Document_ID { get; set; }
    //    [XmlAttribute("Document_Date")]
    //    public Nullable<System.DateTime> Document_Date { get; set; }
    //    [XmlAttribute("Document_type_ID")]
    //    public Nullable<int> Document_type_ID { get; set; }
    //    [XmlAttribute("PS_ID")]
    //    public Nullable<int> PS_ID { get; set; }
    //    [XmlAttribute("DocumentFile")]
    //    public byte[] DocumentFile { get; set; }
    //    [XmlAttribute("File_Name")]
    //    public string File_Name { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(DocumentTypes))]
    //public partial class DocumentTypes
    //{
    //    [XmlAttribute("Name_document_type")]
    //    public string Name_document_type { get; set; }
    //    [XmlAttribute("Document_type_ID")]
    //    public int Document_type_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Employees))]
    //public partial class Employees
    //{
    //    [XmlAttribute("Office_ID")]
    //    public Nullable<int> Office_ID { get; set; }
    //    [XmlAttribute("Employee_ID")]
    //    public int Employee_ID { get; set; }
    //    [XmlAttribute("Full_Name_Employee")]
    //    public string Full_Name_Employee { get; set; }
    //    [XmlAttribute("Personnel_Number")]
    //    public int Personnel_Number { get; set; }
    //    [XmlAttribute("Profession_ID")]
    //    public Nullable<int> Profession_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Offices))]
    //public partial class Offices
    //{
    //    [XmlAttribute("Name_Office")]
    //    public string Name_Office { get; set; }
    //    [XmlAttribute("Office_ID")]
    //    public int Office_ID { get; set; }
    //    [XmlAttribute("Department_ID")]
    //    public int Department_ID { get; set; }
    //    [XmlAttribute("Backward_ID")]
    //    public int Backward_ID { get; set; }


    //}

    //[Serializable]
    //[XmlType(nameof(PrgLangs))]
    //public partial class PrgLangs
    //{
    //    [XmlAttribute("Prg_Lang")]
    //    public string Prg_Lang { get; set; }
    //    [XmlAttribute("Prg_Lang_ID")]
    //    public int Prg_Lang_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(PrgSystems))]
    //public partial class PrgSystems
    //{
    //    [XmlAttribute("PS_Name")]
    //    public string PS_Name { get; set; }
    //    [XmlAttribute("PS_ID")]
    //    public int PS_ID { get; set; }
    //    [XmlAttribute("Prg_Lang_ID")]
    //    public Nullable<int> Prg_Lang_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Professions))]
    //public partial class Professions
    //{
    //    [XmlAttribute("Name_Profession")]
    //    public string Name_Profession { get; set; }
    //    [XmlAttribute("Profession_ID")]
    //    public int Profession_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(RecordHistories))]
    //public partial class RecordHistories
    //{
    //    [XmlAttribute("Note")]
    //    public string Note { get; set; }
    //    [XmlAttribute("Record_ID")]
    //    public int Record_ID { get; set; }
    //    [XmlAttribute("PS_ID")]
    //    public int PS_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(SubSystems))]
    //public partial class SubSystems
    //{
    //    [XmlAttribute("Sub_System")]
    //    public string Sub_System { get; set; }
    //    [XmlAttribute("Sub_System_ID")]
    //    public int Sub_System_ID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Users))]
    //public partial class Users
    //{
    //    [XmlAttribute("Employee_ID")]
    //    public int Employee_ID { get; set; }
    //    [XmlAttribute("User_ID")]
    //    public int User_ID { get; set; }
    //    [XmlAttribute("Login")]
    //    public string Login { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(Workplaces))]
    //public partial class Workplaces
    //{
    //    [XmlAttribute("Office_ID")]
    //    public Nullable<int> Office_ID { get; set; }
    //    [XmlAttribute("Workplace_ID")]
    //    public int Workplace_ID { get; set; }
    //    [XmlAttribute("Employee_ID")]
    //    public Nullable<int> Employee_ID { get; set; }
    //    [XmlAttribute("Floor")]
    //    public string Floor { get; set; }
    //    [XmlAttribute("Housing")]
    //    public string Housing { get; set; }
    //    [XmlAttribute("Telephone")]
    //    public string Telephone { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(AccesRightToUsers_Proc_Result))]
    //public partial class AccesRightToUsers_Proc_Result
    //{
    //    [XmlAttribute("User_ID")]
    //    public Nullable<int> User_ID { get; set; }
    //    [XmlAttribute("AccesRightID")]
    //    public Nullable<int> AccesRightID { get; set; }
    //}

    //[Serializable]
    //[XmlType(nameof(WorkplaceToComputers_Proc_Result))]
    //public partial class WorkplaceToComputers_Proc_Result
    //{
    //    [XmlAttribute("Workplace_ID")]
    //    public Nullable<int> Workplace_ID { get; set; }
    //    [XmlAttribute("Computer_ID")]
    //    public Nullable<int> Computer_ID { get; set; }
    //}

}
