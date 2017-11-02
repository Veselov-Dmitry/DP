using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Model
{
    public class CommonClass
    {
        //все ID
        public int Department_ID { get; set; }
        public int Office_ID { get; set; }
        public int Profession_ID { get; set; }
        public int Employee_ID { get; set; }
        public int Workplace_ID { get; set; }
        public int Computer_ID { get; set; }
        public int User_ID { get; set; }
        public Nullable<int> PS_ID { get; set; }
        public int Developer_part_ID { get; set; }
        public int Record_ID { get; set; }
        public int Sub_System_ID { get; set; }
        public int Prg_Lang_ID { get; set; }
        public int Document_ID { get; set; }
        public Nullable<int> Document_type_ID { get; set; }
        public int AccesRightID { get; set; }

        //остальные поля
        public string Name_Department { get; set; }

        public string Name_Office { get; set; }

        public string Name_Profession { get; set; }

        public string Full_Name_Employee { get; set; }
        public int Personnel_Number { get; set; }

        public string Floor { get; set; }
        public string Housing { get; set; }
        public string Telephone { get; set; }

        public int Inventory_number { get; set; }
        public string Net_Name { get; set; }

        public Nullable<System.DateTime> Date_start { get; set; }

        public string PS_Name { get; set; }

        public string Note { get; set; }

        public string Sub_System { get; set; }

        public string Prg_Lang { get; set; }

        public Nullable<System.DateTime> Document_Date { get; set; }
        public string Document_Number { get; set; }

        public string Name_document_type { get; set; }

        public string Login { get; set; }

        public Nullable<bool> DocumentFile { get; set; }

        public string File_Name { get; set; }

        public object NameOf(string name)
        {
            switch (name)
            {
                case "Department_ID": { return Department_ID; }
                case "Office_ID": { return Office_ID; }
                case "Profession_ID": { return Profession_ID; }
                case "Employee_ID": { return Employee_ID; }
                case "Workplace_ID": { return Workplace_ID; }
                case "Computer_ID": { return Computer_ID; }
                case "User_ID": { return User_ID; }
                case "PS_ID": { return PS_ID; }
                case "Developer_part_ID": { return Developer_part_ID; }
                case "Record_ID": { return Record_ID; }
                case "Sub_System_ID": { return Sub_System_ID; }
                case "Prg_Lang_ID": { return Prg_Lang_ID; }
                case "Document_ID": { return Document_ID; }
                case "Document_type_ID": { return Document_type_ID; }
                case "AccesRightID": { return AccesRightID; }
                case "Name_Department": { return Name_Department; }
                case "Name_Office": { return Name_Office; }
                case "Name_Profession": { return Name_Profession; }
                case "Full_Name_Employee": { return Full_Name_Employee; }
                case "Personnel_Number": { return Personnel_Number; }
                case "Floor": { return (Floor == null) ? "" : Floor; }
                case "Housing": { return (Housing == null) ? "" : Housing; }
                case "Telephone": { return ( Telephone== null) ? "" : Telephone; }
                case "Inventory_number": { return Inventory_number; }
                case "Net_Name": { return Net_Name; }
                case "Date_start": { return Date_start; }
                case "PS_Name": { return PS_Name; }
                case "Note": { return Note; }
                case "Sub_System": { return Sub_System; }
                case "Prg_Lang": { return Prg_Lang; }
                case "Document_Date": { return Document_Date; }
                case "Document_Number": { return Document_Number; }
                case "Name_document_type": { return Name_document_type; }
                case "Login": { return Login; }
                case "DocumentFile": { return DocumentFile; }
                case "File_Name": { return (File_Name == null) ? "" :File_Name; }
                default: return null;
            }
        }
    }
}
