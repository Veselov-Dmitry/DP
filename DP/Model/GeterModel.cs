using DP.Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.Model.XML
{

    class GeterModel: IGeterModel
    {
        private Dictionary<Type, List<object>> _dictionary;
        private List<Type> _types;
        private OASUEntity DB;


        public Dictionary<Type, List<object>> Dictionary
        {
            get
            {
                if (_dictionary == null)
                    return GetAllTables();
                return _dictionary;
            }
            private set
            {
                _dictionary = value;
            }
        }
        public List<Type> Types
        {
            get
            {
                if (_types == null)
                    return GetAllTypes();
                return _types;
            }
            private set
            {
                _types = value;
            }
        }

        public GeterModel(OASUEntity DB)
        {
            this.DB = DB;
            Dictionary = new Dictionary<Type, List<object>>();
        }
        public void SetAllTables(Dictionary<Type, List<object>> dect)
        {
            List<object> val = null;
            if (dect.TryGetValue(typeof(Computers), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Computers] ON");
                    val.ForEach(x =>
                    {
                        DB.Database.ExecuteSqlCommand(
                            "INSERT INTO dbo.Computers (Inventory_number,Computer_ID,Net_Name ) VALUES(" +
                            (x as Computers).Inventory_number + "," +
                            (x as Computers).Computer_ID +",N'"+
                            (x as Computers).Net_Name + "')");
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Computers] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Departments), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Departments] ON");
                    val.ForEach(x =>
                    {
                        DB.Database.ExecuteSqlCommand(
                            "INSERT INTO dbo.Departments (Department_ID,Name_Department) VALUES(" + 
                            (x as Departments).Department_ID + ",N'" +
                            (x as Departments).Name_Department + "')");
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Departments] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Offices), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Offices] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Offices (Office_ID, Name_Office, Department_ID, Backward_ID) VALUES (" +
                            (x as Offices).Office_ID + ",N'" +
                            (x as Offices).Name_Office + "'," +
                            (x as Offices).Department_ID + "," +
                            (((x as Offices).Backward_ID == null)?
                                "NULL":
                                (x as Offices).Backward_ID.Value.ToString()) + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Offices] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Professions), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Professions] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Professions (Profession_ID, Name_Profession) VALUES (" +
                            (x as Professions).Profession_ID + ",N'" +
                            (x as Professions).Name_Profession + "')";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Professions] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Employees), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Employees] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Employees (Office_ID, Employee_ID, Full_Name_Employee, Personnel_Number, Profession_ID) VALUES (" +
                            (((x as Employees).Office_ID == null) ?
                                "NULL" :
                                (x as Employees).Office_ID.Value.ToString()) + "," +
                            (x as Employees).Employee_ID + ",N'" +
                            (x as Employees).Full_Name_Employee + "'," +
                            (x as Employees).Personnel_Number + "," +
                            (((x as Employees).Profession_ID == null) ?
                                "NULL" :
                                (x as Employees).Profession_ID.Value.ToString()) + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Employees] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Workplaces), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Workplaces] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Workplaces (Office_ID, Workplace_ID, Employee_ID, Floor, Housing, Telephone) VALUES (" +
                            (((x as Workplaces).Office_ID == null) ?
                                "NULL" :
                                (x as Workplaces).Office_ID.Value.ToString()) + "," +
                            (x as Workplaces).Workplace_ID + "," +
                            (((x as Workplaces).Employee_ID == null) ?
                                "NULL" :
                                (x as Workplaces).Employee_ID.Value.ToString()) + ",N'" +
                            (x as Workplaces).Floor + "',N'" +
                            (x as Workplaces).Housing + "',N'" +
                            (x as Workplaces).Telephone + "')" ;
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Workplaces] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(WorkplaceToComputers_Proc_Result), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    val.ForEach(x =>
                    {
                        DB.Database.ExecuteSqlCommand(
                            "INSERT INTO dbo.WorkplaceToComputers ( Workplace_ID,Computer_ID ) VALUES(" +
                            (x as WorkplaceToComputers_Proc_Result).Workplace_ID + "," +
                            (x as WorkplaceToComputers_Proc_Result).Computer_ID + ")");
                    });
                    DB.SaveChanges();
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(DocumentTypes), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DocumentTypes] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.DocumentTypes (Document_type_ID, Name_document_type) VALUES (" +
                            (x as DocumentTypes).Document_type_ID + ",N'" +
                            (x as DocumentTypes).Name_document_type + "')";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[DocumentTypes] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(PrgLangs), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PrgLangs] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.PrgLangs (Prg_Lang_ID, Prg_Lang) VALUES (" +
                            (x as PrgLangs).Prg_Lang_ID + ",N'" +
                            (x as PrgLangs).Prg_Lang + "')";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PrgLangs] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(PrgSystems), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PrgSystems] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.PrgSystems (PS_ID, PS_Name, Prg_Lang_ID) VALUES (" +
                            (x as PrgSystems).PS_ID + ",N'" +
                            (x as PrgSystems).PS_Name + "'," +
                            (((x as PrgSystems).Prg_Lang_ID == null|| (x as PrgSystems).Prg_Lang_ID == 0) ?
                                "NULL" :
                                (x as PrgSystems).Prg_Lang_ID.Value.ToString()) + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[PrgSystems] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Documents), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Documents] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Documents (Document_Number, Document_ID, Document_Date, Document_type_ID, PS_ID, File_Name) VALUES (" +
                             "N'" + (x as Documents).Document_Number + "'," +
                            (x as Documents).Document_ID + ",CAST(N'" +
                            (((x as Documents).Document_Date == null) ?
                                "NULL" :
                                (x as Documents).Document_Date.Value.ToString()) + "' AS DateTime)," +
                            (((x as Documents).Document_type_ID == null) ?
                                "NULL" :
                                (x as Documents).Document_type_ID.Value.ToString()) + "," +
                            (((x as Documents).PS_ID == null) ?
                                "NULL" :
                                (x as Documents).PS_ID.Value.ToString()) + ",N'" +
                            (x as Documents).File_Name + "')";
                        DB.Database.ExecuteSqlCommand(query);
                        //(x as Documents).DocumentFile + "," +
                    });
                    DB.SaveChanges();
                    
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Documents] OFF");
                    transaction.Commit();
                }
                foreach(object x in val)
                {
                    if (!String.IsNullOrEmpty((x as Documents).File_Name))
                    {
                        int index = (x as Documents).Document_ID;
                        Documents Doc = (from doc in DB.Documents
                                         where doc.Document_ID == index
                                         select doc).FirstOrDefault();
                        Doc.DocumentFile = (x as Documents).DocumentFile;
                    }
                };
                DB.SaveChanges();
            }
            if (dect.TryGetValue(typeof(RecordHistories), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[RecordHistories] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.RecordHistories (Record_ID, Note, PS_ID) VALUES (" +
                            (x as RecordHistories).Record_ID + ",N'" +
                            (x as RecordHistories).Note + "'," +
                            (x as RecordHistories).PS_ID + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[RecordHistories] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Developers), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Developers] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Developers (Developer_part_ID, Record_ID, Date_start, Employee_ID) VALUES (" +
                            (x as Developers).Developer_part_ID + "," +
                            (x as Developers).Record_ID + ",CAST(N'" +
                            (((x as Developers).Date_start == null) ?
                                "NULL" :
                                (x as Developers).Date_start.Value.ToString()) + "' AS DateTime)," +
                            (((x as Developers).Employee_ID == null|| (x as Developers).Employee_ID == 0) ?
                                "NULL" :
                                (x as Developers).Employee_ID.Value.ToString()) + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Developers] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(SubSystems), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[SubSystems] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.SubSystems (Sub_System_ID, Sub_System) VALUES (" +
                            (x as SubSystems).Sub_System_ID + ",N'" +
                            (x as SubSystems).Sub_System + "')";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[SubSystems] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(Users), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.Users (User_ID, Login, Employee_ID) VALUES (" +
                            (x as Users).User_ID + ",N'" +
                            (x as Users).Login + "'," +
                            (x as Users).Employee_ID + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[Users] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(AccesRights), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[AccesRights] ON");
                    val.ForEach(x =>
                    {
                        string query = "INSERT INTO dbo.AccesRights (Sub_System_ID, AccesRightID, PS_ID) VALUES (" +
                            (((x as AccesRights).Sub_System_ID == null) ?
                                "NULL" :
                                (x as AccesRights).Sub_System_ID.Value.ToString()) + ",N'" +
                            (x as AccesRights).AccesRightID + "'," +
                            (x as AccesRights).PS_ID + ")";
                        DB.Database.ExecuteSqlCommand(query);
                    });
                    DB.SaveChanges();
                    DB.Database.ExecuteSqlCommand("SET IDENTITY_INSERT [dbo].[AccesRights] OFF");
                    transaction.Commit();
                }
            }
            if (dect.TryGetValue(typeof(AccesRightToUsers_Proc_Result), out val))
            {
                using (var transaction = DB.Database.BeginTransaction())
                {
                    val.ForEach(x =>
                    {
                        DB.Database.ExecuteSqlCommand(
                            "INSERT INTO dbo.AccesRightToUsers ( User_ID,AccesRightID ) VALUES(" +
                            (((x as AccesRightToUsers_Proc_Result).User_ID == null || (x as AccesRightToUsers_Proc_Result).User_ID == 0) ?
                                "NULL" :
                                (x as AccesRightToUsers_Proc_Result).User_ID.Value.ToString()) + "," +
                            (((x as AccesRightToUsers_Proc_Result).AccesRightID == null || (x as AccesRightToUsers_Proc_Result).AccesRightID == 0) ?
                                "NULL" :
                                (x as AccesRightToUsers_Proc_Result).AccesRightID.Value.ToString()) + ")");
                    });
                    DB.SaveChanges();
                    transaction.Commit();
                }
            }
        }

        public Dictionary<Type, List<object>> GetAllTables()
        {
            Types = GetAllTypes();
            List<Func<List<object>>> gets = new List<Func<List<object>>>
            {
                (() =>
                {//AccesRights
                    return (from obj in DB.AccesRights select obj).ToList<object>();
                }),
                (() =>
                {//Computers
                    return  (from obj in DB.Computers select obj).ToList<object>();
                }),
                (() =>
                {//Departments
                    return (from obj in DB.Departments select obj).ToList<object>();
                }),
                (() =>
                {//Developers
                    return (from obj in DB.Developers select obj).ToList<object>();
                }),
                (() =>
                {//Documents
                    return (from obj in DB.Documents select obj).ToList<object>();
                }),
                (() =>
                {//DocumentTypes
                    return (from obj in DB.DocumentTypes select obj).ToList<object>();
                }),
                (() =>
                {//Employees
                    return (from obj in DB.Employees select obj).ToList<object>();
                }),
                (() =>
                {//Offices
                    return (from obj in DB.Offices select obj).ToList<object>();
                }),
                (() =>
                {//PrgLangs
                    return (from obj in DB.PrgLangs select obj).ToList<object>();
                }),
                (() =>
                {//PrgSystems
                    return (from obj in DB.PrgSystems select obj).ToList<object>();
                }),
                (() =>
                {//Professions
                    return (from obj in DB.Professions select obj).ToList<object>();
                }),
                (() =>
                {//RecordHistories
                    return (from obj in DB.RecordHistories select obj).ToList<object>();
                }),
                (() =>
                {//SubSystems
                    return (from obj in DB.SubSystems select obj).ToList<object>();
                }),
                (() =>
                {//Users
                    return (from obj in DB.Users select obj).ToList<object>();
                }),
                (() =>
                {//Workplaces
                    return (from obj in DB.Workplaces select obj).ToList<object>();
                }),
                (() =>
                {//AccesRightToUsers_Proc
                    return (DB.AccesRightToUsers_Proc()).ToList<object>();
                }),
                (() =>
                {//WorkplaceToComputers_Proc
                    return (DB.WorkplaceToComputers_Proc ()).ToList<object>();
                })/**/
            };
            foreach (Type tp in Types)
            {
                Dictionary.Add(tp, gets[Types.IndexOf(tp)]());
            }
            return Dictionary;
        }
        public List<Type> GetAllTypes()
        {
            return new List<Type>
            {
                typeof(AccesRights),//1
                typeof(Computers),//2
                typeof(Departments),//3
                typeof(Developers),//4
                typeof(Documents),//5
                typeof(DocumentTypes),//6
                typeof(Employees),//7
                typeof(Offices),//8
                typeof(PrgLangs),//9
                typeof(PrgSystems),//10
                typeof(Professions),//11
                typeof(RecordHistories),//12
                typeof(SubSystems),//13
                typeof(Users),//14
                typeof(Workplaces),//15
                typeof(AccesRightToUsers_Proc_Result),//16
                typeof(WorkplaceToComputers_Proc_Result)//17  /**/
            };
        }
    }
}
