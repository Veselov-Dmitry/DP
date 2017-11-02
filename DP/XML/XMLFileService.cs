using DP.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace DP.XML
{
    class XMLFileService : IFileService
    {
        public Dictionary<Type, List<object>> Open(string filename)
        {
            Dictionary<Type, List<object>> tablesDist = new Dictionary<Type, List<object>>();
            //читаем данные из файла
            XDocument doc = XDocument.Load(filename);
            //проходим по каждому элементу в найшей library
            //(этот элемент сразу доступен через свойство doc.Root)
            foreach (XElement table in doc.Root.Elements())
            {
                

                int val = 0;
                DateTime vatDate = default(DateTime);
                switch (table.Attribute("Name").Value.ToString())
                {
                    case nameof(AccesRights):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                AccesRights op = new AccesRights();
                                int.TryParse(item.Element(nameof(op.AccesRightID)).Value, out val);
                                op.AccesRightID = val;

                                int.TryParse(item.Element(nameof(op.Sub_System_ID)).Value,out val);
                                op.Sub_System_ID = (val == 0)? default(int?) : val;

                                int.TryParse(item.Element(nameof(op.PS_ID)).Value, out val);
                                op.PS_ID = val;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(AccesRights), tableList);
                            break;
                        }
                    case nameof(Computers):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Computers op = new Computers();

                                int.TryParse(item.Element(nameof(op.Inventory_number)).Value, out val);
                                op.Inventory_number = val;

                                int.TryParse(item.Element(nameof(op.Computer_ID)).Value, out val);
                                op.Computer_ID = val;

                                op.Net_Name = item.Element(nameof(op.Net_Name)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Computers), tableList);
                            break;
                        }
                    case nameof(Departments):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Departments op = new Departments();
                                op.Name_Department = item.Element(nameof(op.Name_Department)).Value;

                                int.TryParse(item.Element(nameof(op.Department_ID)).Value, out val);
                                op.Department_ID = val;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Departments), tableList);
                            break;
                        }
                    case nameof(Developers):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Developers op = new Developers();
                                int.TryParse(item.Element(nameof(op.Developer_part_ID)).Value, out val);
                                op.Developer_part_ID = val;

                                int.TryParse(item.Element(nameof(op.Record_ID)).Value, out val);
                                op.Record_ID = val;

                                int.TryParse(item.Element(nameof(op.Employee_ID)).Value, out val);
                                op.Employee_ID = (val == 0) ? default(int?) : val;

                                DateTime.TryParse(item.Element(nameof(op.Date_start)).Value, out vatDate);
                                op.Date_start = (System.DateTime.MinValue == vatDate) ? default(DateTime?) : vatDate;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Developers), tableList);
                            break;
                        }
                    case nameof(Documents):
                        {
                            List<object> tableList = new List<object>();
                            Dictionary<int, byte[]> files = new Dictionary<int, byte[]>();
                            string path4Data = Path.GetDirectoryName(filename) + @"\" + Path.GetFileNameWithoutExtension(filename) + ".dat";
                            if (File.Exists(path4Data)) {
                                using (BinaryReader reader = new BinaryReader(File.Open(path4Data, FileMode.Open)))
                                {
                                    // пока не достигнут конец файла
                                    // считываем каждое значение из файла
                                    while (reader.PeekChar() > -1)
                                    {
                                        int area = reader.ReadInt32();
                                        int length = reader.ReadInt32();
                                        byte[] s = reader.ReadBytes(length);
                                        files.Add(area, s);
                                    }
                                }
                            }
                            else { System.Windows.Forms.MessageBox.Show("Нет файла с документами инморта вида <Имя XML файла>.dat"); }
                            foreach (XElement item in table.Elements())
                            {
                                Documents op = new Documents();

                                int.TryParse(item.Element(nameof(op.Document_ID)).Value, out val);
                                op.Document_ID = val;

                                op.Document_Number = item.Element(nameof(op.Document_Number)).Value;

                                DateTime.TryParse(item.Element(nameof(op.Document_Date)).Value, out vatDate);
                                op.Document_Date = (System.DateTime.MinValue == vatDate) ? default(DateTime?) : vatDate;

                                int.TryParse(item.Element(nameof(op.Document_type_ID)).Value, out val);
                                op.Document_type_ID = (val == 0) ? default(int?) : val;

                                int.TryParse(item.Element(nameof(op.PS_ID)).Value, out val);
                                op.PS_ID = (val == 0) ? default(int?) : val;

                                op.File_Name = item.Element(nameof(op.File_Name)).Value;
                                if(!string.IsNullOrWhiteSpace(op.File_Name)&& files.Count>0)
                                {
                                    byte[] byteArray = null;
                                    files.TryGetValue(op.Document_ID, out byteArray);
                                    op.DocumentFile = byteArray;
                                }

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Documents), tableList);
                            break;
                        }
                    case nameof(DocumentTypes):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                DocumentTypes op = new DocumentTypes();
                                int.TryParse(item.Element(nameof(op.Document_type_ID)).Value, out val);
                                op.Document_type_ID = val;
                                
                                op.Name_document_type = item.Element(nameof(op.Name_document_type)).Value;
                                
                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(DocumentTypes), tableList);
                            break;
                        }
                    case nameof(Employees):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Employees op = new Employees();
                                int.TryParse(item.Element(nameof(op.Employee_ID)).Value, out val);
                                op.Employee_ID = val;

                                int.TryParse(item.Element(nameof(op.Personnel_Number)).Value, out val);
                                op.Personnel_Number = val;

                                int.TryParse(item.Element(nameof(op.Office_ID)).Value, out val);
                                op.Office_ID = (val == 0) ? default(int?) : val;

                                op.Full_Name_Employee = item.Element(nameof(op.Full_Name_Employee)).Value;

                                int.TryParse(item.Element(nameof(op.Profession_ID)).Value, out val);
                                op.Profession_ID = (val == 0) ? default(int?) : val;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Employees), tableList);
                            break;
                        }
                    case nameof(Offices):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Offices op = new Offices();

                                int.TryParse(item.Element(nameof(op.Office_ID)).Value, out val);
                                op.Office_ID = val;

                                int.TryParse(item.Element(nameof(op.Department_ID)).Value, out val);
                                op.Department_ID = val;

                                op.Name_Office = item.Element(nameof(op.Name_Office)).Value;

                                int.TryParse(item.Element(nameof(op.Backward_ID)).Value, out val);
                                op.Backward_ID = (val == 0) ? default(int?) : val; 

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Offices), tableList);
                            break;
                        }
                    case nameof(PrgLangs):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                PrgLangs op = new PrgLangs();
                                int.TryParse(item.Element(nameof(op.Prg_Lang_ID)).Value, out val);
                                op.Prg_Lang_ID = val;

                                op.Prg_Lang = item.Element(nameof(op.Prg_Lang)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(PrgLangs), tableList);
                            break;
                        }
                    case nameof(PrgSystems):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                PrgSystems op = new PrgSystems();
                                int.TryParse(item.Element(nameof(op.PS_ID)).Value, out val);
                                op.PS_ID = val;

                                int.TryParse(item.Element(nameof(op.Prg_Lang_ID)).Value, out val);
                                op.Prg_Lang_ID = val;

                                op.PS_Name = item.Element(nameof(op.PS_Name)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(PrgSystems), tableList);
                            break;
                        }
                    case nameof(Professions):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Professions op = new Professions();
                                int.TryParse(item.Element(nameof(op.Profession_ID)).Value, out val);
                                op.Profession_ID = val;

                                op.Name_Profession = item.Element(nameof(op.Name_Profession)).Value;
                                
                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Professions), tableList);
                            break;
                        }
                    case nameof(RecordHistories):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                RecordHistories op = new RecordHistories();
                                int.TryParse(item.Element(nameof(op.Record_ID)).Value, out val);
                                op.Record_ID = val;

                                int.TryParse(item.Element(nameof(op.PS_ID)).Value, out val);
                                op.PS_ID = val;

                                op.Note = item.Element(nameof(op.Note)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(RecordHistories), tableList);
                            break;
                        }
                    case nameof(SubSystems):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                SubSystems op = new SubSystems();
                                int.TryParse(item.Element(nameof(op.Sub_System_ID)).Value, out val);
                                op.Sub_System_ID = val;

                                op.Sub_System = item.Element(nameof(op.Sub_System)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(SubSystems), tableList);
                            break;
                        }
                    case nameof(Users):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Users op = new Users();
                                int.TryParse(item.Element(nameof(op.User_ID)).Value, out val);
                                op.User_ID = val;

                                int.TryParse(item.Element(nameof(op.Employee_ID)).Value, out val);
                                op.Employee_ID = val;

                                op.Login = item.Element(nameof(op.Login)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Users), tableList);
                            break;
                        }
                    case nameof(Workplaces):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                Workplaces op = new Workplaces();

                                int.TryParse(item.Element(nameof(op.Workplace_ID)).Value, out val);
                                op.Workplace_ID = val;

                                int.TryParse(item.Element(nameof(op.Employee_ID)).Value, out val);
                                op.Employee_ID = (val == 0) ? default(int?) : val;

                                int.TryParse(item.Element(nameof(op.Office_ID)).Value, out val);
                                op.Office_ID = (val == 0) ? default(int?) : val;

                                op.Floor = item.Element(nameof(op.Floor)).Value;

                                op.Housing = item.Element(nameof(op.Housing)).Value;

                                op.Telephone = item.Element(nameof(op.Telephone)).Value;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(Workplaces), tableList);
                            break;
                        }
                    case nameof(AccesRightToUsers_Proc_Result):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                AccesRightToUsers_Proc_Result op = new AccesRightToUsers_Proc_Result();

                                int.TryParse(item.Element(nameof(op.AccesRightID)).Value, out val);
                                op.AccesRightID = (val == 0) ? default(int?) : val;

                                int.TryParse(item.Element(nameof(op.User_ID)).Value, out val);
                                op.User_ID = (val == 0) ? default(int?) : val;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(AccesRightToUsers_Proc_Result), tableList);
                            break;
                        }
                    case nameof(WorkplaceToComputers_Proc_Result):
                        {
                            List<object> tableList = new List<object>();
                            foreach (XElement item in table.Elements())
                            {
                                WorkplaceToComputers_Proc_Result op = new WorkplaceToComputers_Proc_Result();

                                int.TryParse(item.Element(nameof(op.Computer_ID)).Value, out val);
                                op.Computer_ID = (val == 0) ? default(int?) : val;

                                int.TryParse(item.Element(nameof(op.Workplace_ID)).Value, out val);
                                op.Workplace_ID = (val == 0) ? default(int?) : val;

                                tableList.Add(op);
                            }
                            tablesDist.Add(typeof(WorkplaceToComputers_Proc_Result), tableList);
                            break;
                        }

                    default: { break; }
                }
            }
                return tablesDist;
        }
        
        public void Save(string filename, Dictionary<Type, List<object>> tablesDist)
        {
            int i = 0;
            XDocument doc = new XDocument();

            #region Tables Insert
            XElement AccesRights = new XElement("Table",
            new XAttribute("Name", typeof(AccesRights).ToString().Split('.')[2]));
            XElement Computers = new XElement("Table",
                new XAttribute("Name", typeof(Computers).ToString().Split('.')[2]));
            XElement Departments = new XElement("Table",
                new XAttribute("Name", typeof(Departments).ToString().Split('.')[2]));
            XElement Developers = new XElement("Table",
                new XAttribute("Name", typeof(Developers).ToString().Split('.')[2]));
            XElement Documents = new XElement("Table",
                new XAttribute("Name", typeof(Documents).ToString().Split('.')[2]));
            XElement DocumentTypes = new XElement("Table",
                new XAttribute("Name", typeof(DocumentTypes).ToString().Split('.')[2]));
            XElement Employees = new XElement("Table",
                new XAttribute("Name", typeof(Employees).ToString().Split('.')[2]));
            XElement Offices = new XElement("Table",
                new XAttribute("Name", typeof(Offices).ToString().Split('.')[2]));
            XElement PrgLangs = new XElement("Table",
                new XAttribute("Name", typeof(PrgLangs).ToString().Split('.')[2]));
            XElement PrgSystems = new XElement("Table",
                new XAttribute("Name", typeof(PrgSystems).ToString().Split('.')[2]));
            XElement Professions = new XElement("Table",
                new XAttribute("Name", typeof(Professions).ToString().Split('.')[2]));
            XElement RecordHistories = new XElement("Table",
                new XAttribute("Name", typeof(RecordHistories).ToString().Split('.')[2]));
            XElement SubSystems = new XElement("Table",
                new XAttribute("Name", typeof(SubSystems).ToString().Split('.')[2]));
            XElement Users = new XElement("Table",
                new XAttribute("Name", typeof(Users).ToString().Split('.')[2]));
            XElement Workplaces = new XElement("Table",
                new XAttribute("Name", typeof(Workplaces).ToString().Split('.')[2]));
            XElement AccesRightToUsers_Proc_Result = new XElement("Table",
                new XAttribute("Name", typeof(AccesRightToUsers_Proc_Result).ToString().Split('.')[2]));
            XElement WorkplaceToComputers_Proc_Result = new XElement("Table",
                new XAttribute("Name", typeof(WorkplaceToComputers_Proc_Result).ToString().Split('.')[2]));
            XElement root = new XElement("DB1146",
                AccesRights, Computers, Departments,
                Developers, Documents, DocumentTypes,
                Employees, Offices, PrgLangs,
                PrgSystems, Professions, RecordHistories,
                SubSystems, Users, Workplaces,
                AccesRightToUsers_Proc_Result, WorkplaceToComputers_Proc_Result);
            doc.Add(root);
            #endregion

            #region Values Insert
            i = 0;
            foreach (AccesRights op in (tablesDist[typeof(AccesRights)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.AccesRightID), op.AccesRightID),
                        new XElement(nameof(op.Sub_System_ID), op.Sub_System_ID),
                        new XElement(nameof(op.PS_ID), op.PS_ID));
                AccesRights.Add(item);
            };
            i = 0;
            foreach (Computers op in (tablesDist[typeof(Computers)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Net_Name), op.Net_Name),
                        new XElement(nameof(op.Inventory_number), op.Inventory_number),
                        new XElement(nameof(op.Computer_ID), op.Computer_ID));
                Computers.Add(item);
            };
            i = 0;
            foreach (Departments op in (tablesDist[typeof(Departments)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Department_ID), op.Department_ID),
                        new XElement(nameof(op.Name_Department), op.Name_Department));
                Departments.Add(item);
            };
            i = 0;
            foreach (Developers op in (tablesDist[typeof(Developers)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Developer_part_ID), op.Developer_part_ID),
                        new XElement(nameof(op.Employee_ID), op.Employee_ID),
                        new XElement(nameof(op.Date_start), op.Date_start),
                        new XElement(nameof(op.Record_ID), op.Record_ID));
                Developers.Add(item);
            };
            i = 0;
            string path4Data = Path.GetDirectoryName(filename) + @"\" + Path.GetFileNameWithoutExtension(filename) + ".dat";
            using (BinaryWriter writer = new BinaryWriter(File.Open(path4Data, FileMode.OpenOrCreate)))
            // записываем в файл значение файлов из таблицы
            {
                foreach (Documents op in (tablesDist[typeof(Documents)]))
                {
                    XElement item = new XElement("Item",
                            new XAttribute("id", i++.ToString()),
                            new XElement(nameof(op.Document_ID), op.Document_ID),
                            new XElement(nameof(op.Document_Number), op.Document_Number),
                            new XElement(nameof(op.Document_Date), op.Document_Date),
                            new XElement(nameof(op.Document_type_ID), op.Document_type_ID),
                            new XElement(nameof(op.PS_ID), op.PS_ID),
                            new XElement(nameof(op.File_Name), op.File_Name)
                            );
                    byte[] byteArray = op.DocumentFile;
                    if (byteArray != null)
                    {
                        var file = new XElement(nameof(op.DocumentFile), op.File_Name);
                        writer.Write(op.Document_ID);
                        writer.Write(byteArray.Length);
                        writer.Write(byteArray);
                        item.Add(file);
                    }
                    Documents.Add(item);
                }
            }
            i = 0;
            foreach (DocumentTypes op in (tablesDist[typeof(DocumentTypes)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Document_type_ID), op.Document_type_ID),
                        new XElement(nameof(op.Name_document_type), op.Name_document_type));
                DocumentTypes.Add(item);
            };
            i = 0;
            foreach (Employees op in (tablesDist[typeof(Employees)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Employee_ID), op.Employee_ID),
                        new XElement(nameof(op.Personnel_Number), op.Personnel_Number),
                        new XElement(nameof(op.Office_ID), op.Office_ID),
                        new XElement(nameof(op.Full_Name_Employee), op.Full_Name_Employee),
                        new XElement(nameof(op.Profession_ID), op.Profession_ID));
                Employees.Add(item);
            };
            i = 0;
            foreach (Offices op in (tablesDist[typeof(Offices)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Office_ID), op.Office_ID),
                        new XElement(nameof(op.Name_Office), op.Name_Office),
                        new XElement(nameof(op.Department_ID), op.Department_ID),
                        new XElement(nameof(op.Backward_ID), op.Backward_ID));
                Offices.Add(item);
            };
            i = 0;
            foreach (PrgLangs op in (tablesDist[typeof(PrgLangs)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Prg_Lang_ID), op.Prg_Lang_ID),
                        new XElement(nameof(op.Prg_Lang), op.Prg_Lang));
                PrgLangs.Add(item);
            };
            i = 0;
            foreach (PrgSystems op in (tablesDist[typeof(PrgSystems)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.PS_ID), op.PS_ID),
                        new XElement(nameof(op.PS_Name), op.PS_Name),
                        new XElement(nameof(op.Prg_Lang_ID), op.Prg_Lang_ID));
                PrgSystems.Add(item);
            };
            i = 0;
            foreach (Professions op in (tablesDist[typeof(Professions)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Profession_ID), op.Profession_ID),
                        new XElement(nameof(op.Name_Profession), op.Name_Profession));
                Professions.Add(item);
            };
            i = 0;
            foreach (RecordHistories op in (tablesDist[typeof(RecordHistories)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Record_ID), op.Record_ID),
                        new XElement(nameof(op.PS_ID), op.PS_ID),
                        new XElement(nameof(op.Note), op.Note));
                RecordHistories.Add(item);
            };
            i = 0;
            foreach (SubSystems op in (tablesDist[typeof(SubSystems)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Sub_System_ID), op.Sub_System_ID),
                        new XElement(nameof(op.Sub_System), op.Sub_System));
                SubSystems.Add(item);
            };
            i = 0;
            foreach (Users op in (tablesDist[typeof(Users)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.User_ID), op.User_ID),
                        new XElement(nameof(op.Employee_ID), op.Employee_ID),
                        new XElement(nameof(op.Login), op.Login));
                Users.Add(item);
            };
            i = 0;
            foreach (Workplaces op in (tablesDist[typeof(Workplaces)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Workplace_ID), op.Workplace_ID),
                        new XElement(nameof(op.Employee_ID), op.Employee_ID),
                        new XElement(nameof(op.Office_ID), op.Office_ID),
                        new XElement(nameof(op.Floor), op.Floor),
                        new XElement(nameof(op.Housing), op.Housing),
                        new XElement(nameof(op.Telephone), op.Telephone));
                Workplaces.Add(item);
            };
            i = 0;
            foreach (AccesRightToUsers_Proc_Result op in (tablesDist[typeof(AccesRightToUsers_Proc_Result)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.AccesRightID), op.AccesRightID),
                        new XElement(nameof(op.User_ID), op.User_ID));
                AccesRightToUsers_Proc_Result.Add(item);
            };
            i = 0;
            foreach (WorkplaceToComputers_Proc_Result op in (tablesDist[typeof(WorkplaceToComputers_Proc_Result)]))
            {
                XElement item = new XElement("Item",
                        new XAttribute("id", i++.ToString()),
                        new XElement(nameof(op.Workplace_ID), op.Workplace_ID),
                        new XElement(nameof(op.Computer_ID), op.Computer_ID));
                WorkplaceToComputers_Proc_Result.Add(item);
            };
            #endregion

            //сохраняем наш документ
            doc.Save(filename);
            Process.Start(filename);
        }
    }
}
