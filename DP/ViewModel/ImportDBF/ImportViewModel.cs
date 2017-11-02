using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DP.ViewModel.ImportDBF
{
    class ImportVM
    {
        private struct DataSetDBF
        {
            public string[] col;// = { "", "", "" };
        };
        private DataSetDBF DS;


        public string tempPath { get; private set; }
        public string Path { get; set; }
        public string Text { get; set; }
        public Brush Color { get; set; }
        public bool Enable { get; set; }
        public OleDbConnection conn { get; set; }

        //КОНСТРУКТОР
        public ImportVM()
        {
            Color = Brushes.Green;
            Enable = false;
            Text = "";
            conn = new OleDbConnection();
            DS = new DataSetDBF();

        }

        /// <summary>
        /// создает соединение с DBF файлом
        /// </summary>
        /// <returns></returns>
        public string Connect()
        {
            if (!File.Exists(tempPath))
                return "нет файла длясоединения с DBF";
            try
            {
                if(conn.State == ConnectionState.Closed)
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Path.GetDirectoryName(tempPath) + ";Extended Properties=dBASE IV;User ID=;Password=;";
            }
            catch(Exception exConnetcDBF)
            {
                return "ошибка соединения с DBF: " + exConnetcDBF.Message;
            }
            return "";
        }

        /// <summary>
        /// копирует DBF в папку %temp%\ImportDBF\
        /// </summary>
        /// <returns></returns>
        public string CopyTemp()
        {
            tempPath = System.IO.Path.GetTempPath() + @"ImportDBF\" + System.IO.Path.GetFileName(Path);
            if(!Directory.Exists(System.IO.Path.GetDirectoryName(tempPath)))
                Directory.CreateDirectory(System.IO.Path.GetDirectoryName(tempPath));
            if (File.Exists(tempPath))
            {
                DateTime ftime = File.GetLastWriteTime(Path);
                DateTime ftime2 = File.GetLastWriteTime(tempPath);
                if (ftime == ftime2)
                    return "";
            }
            try
            {       
                File.Copy(Path, tempPath);
            }
            catch(Exception exCopy)
            {
                return "ошибка при копировании: " + exCopy.Message;
            }
            return "";
                
        }

        /// <summary>
        /// проверка полей таблицы на соответствие
        /// </summary>
        /// <returns></returns>
        public string IsCorrect()
        {
            DataTable dt = new DataTable();
            try
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OleDbCommand comm = conn.CreateCommand();
                comm.CommandText = @"SELECT * FROM " + System.IO.Path.GetFileNameWithoutExtension(tempPath);
                dt.Load(comm.ExecuteReader());
            }
            catch(Exception exConn)
            {
                return "ошибка обращения к файлу: " + exConn.Message;
            }
            if (dt.Rows.Count == 0) return "в таблице нет строк";
            if (dt.Columns.Count == 0) return "в таблице нет колонок";
            
            //число совпадений имен столбцов
            int col = DS.col.Length;
            foreach (DataColumn column in dt.Columns)
            {
                foreach(string op in DS.col)
                {
                    //если все колонки найдены в итоге будет ноль
                    if (op.ToLower() == column.ColumnName.ToLower())
                        col--;
                }
            }
            conn.Close();
            if (col == 0)
                return "";
            else
            {
                string val = "";
                foreach (string op in DS.col)
                    val += "[" + op + "] ";
                return "файл имее не коректное количество необходимых столбцов: " + val;
            }
        }

        //!!!ХАРДКОД с полями таблиц по взможности заменить на конфиг или таблизу из БД
        public void InitStruct(string[] name)
        {
            DS.col = name;
        }

        /// <summary>
        /// проверка файла перед импортом
        /// </summary>
        /// <returns></returns>
        public ImportVM PreImport(string path)
        {
            Path = path;
            string error = "";
            if (!string.IsNullOrEmpty(error = CopyTemp()))
            {                
                MessageBox.Show(error);
                return EmptyField();
            }
            if (!string.IsNullOrEmpty(error = Connect()))
            {
                Path = "";
                MessageBox.Show(error);
                return EmptyField();
            }
            if (!string.IsNullOrEmpty(error = IsCorrect()))
            {
                Path = "";
                MessageBox.Show(error);
                return EmptyField();
            }
            CheckIsOK();
            return this;
        }

        public ImportVM CheckIsOK()
        {
            Text = "OK";
            Color = Brushes.Green;
            Enable = true;
            return this;
        }

        /// <summary>
        /// Очиска поля выбора файла
        /// </summary>
        /// <returns></returns>
        public ImportVM EmptyField()
        {
            Path = "";
            Text = "Нет \nфайла";
            Color = Brushes.Red;
            Enable = false;
            return this;
        }

        internal List<Dictionary<string, string>> StartImport()
        {
            DataTable dt = new DataTable();
            try
            {

                if (conn.State != ConnectionState.Open)
                    conn.Open();
                OleDbCommand comm = conn.CreateCommand();
                comm.CommandText = @"SELECT * FROM " + System.IO.Path.GetFileNameWithoutExtension(tempPath);
                dt.Load(comm.ExecuteReader());
                if (dt.Rows.Count == 0) throw new Exception( "в таблице нет строк");
                if (dt.Columns.Count == 0) throw new Exception("в таблице нет колонок");
            }
            catch (Exception exConn)
            {
                MessageBox.Show("" + exConn.Message);
            }

            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            
            foreach(DataRow row in dt.Rows)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();

                int colCnt = 0;
                var cells = row.ItemArray;
                foreach (object cell in cells)
                {
                    foreach(string op in DS.col)
                    {
                        if(dt.Columns[colCnt].ColumnName == op)
                        {
                            dic.Add(op, cell.ToString());
                        }
                    }
                    colCnt++;
                }
                res.Add(dic);
            }
            return res;

        }

        internal ImportVM CompliteImport()
        {
            this.Text = "Готово";
            this.Color = Brushes.Orange;
            this.Path = "";
            Enable = false;
            return this;
        }
    }

    class ImportViewModel : ViewModelBase
    {
        private ImportVM _KADR;
        private ImportVM _SHTAT;
        private ImportVM _SRP_PROF;
        private ImportVM _AA042;
        private string _kadrPath;
        private string _shtatPath;
        private string _arp_prfPath;
        private string _aa042Path;
        private Visibility _visible = Visibility.Hidden;
        private bool _enableWindow = true;

        private string[] fieldsKADR = new[] { "TAB", "FAM", "IM", "OT", "DATAUV", "KODPROF", "KODDOLZN" };
        private string[] fieldsSHTAT = new[] { "CEX", "UCH", "TABN" , "FIO" };
        private string[] fieldsSRP_PROFR = new[] { "KOD", "NAIM_PECH" };
        private string[] fieldsAA042 = new[] { "CEX", "UCH", "NAMESOKR", "NAMEPOLN" }; 
        public ImportVM KADR
        {
            get { return _KADR; }
            set { _KADR = value; OnPropertyChanged("KADR");}
        }
        public ImportVM SHTAT
        {
            get { return _SHTAT; }
            set { _SHTAT = value; OnPropertyChanged("SHTAT"); }
        }
        public ImportVM SRP_PROF
        {
            get { return _SRP_PROF; }
            set { _SRP_PROF = value; OnPropertyChanged("SRP_PROF"); }
        }
        public ImportVM AA042
        {
            get { return _AA042; }
            set { _AA042 = value; OnPropertyChanged("AA042"); }
        }
        public string KadrPath
        {
            get { return _kadrPath; }
            set { _kadrPath = value; OnPropertyChanged("KadrPath"); }
        }
        public string ShtatPath
        {
            get { return _shtatPath; }
            set { _shtatPath = value; OnPropertyChanged("ShtatPath"); }
        }
        public string Srp_prfPath
        {
            get { return _arp_prfPath; }
            set { _arp_prfPath = value; OnPropertyChanged("Srp_prfPath"); }
        }
        public string Aa042Path
        {
            get { return _aa042Path; }
            set { _aa042Path = value; OnPropertyChanged("Aa042Path"); }
        }
        public Visibility Visible {
            get { return _visible; }
            set { _visible = value; OnPropertyChanged("Visible"); }
        }
        public bool EnableWindow {
            get { return _enableWindow; }
            set { _enableWindow = value; OnPropertyChanged("EnableWindow"); }
        }

        public ICommand ImportCommand { get; set; }
        public ICommand BrowseCommand { get; set; }
        public ICommand EnterCommand{ get; set; }


        public ImportViewModel()
        {
            Load(false);
            KADR = new ImportVM();
            SHTAT = new ImportVM();
            SRP_PROF = new ImportVM();
            AA042 = new ImportVM();
            bwSave = new BackgroundWorker();
            ImportCommand = new RelayCommand<object>(arg => ImportCommandMethod(arg));
            BrowseCommand = new RelayCommand<object>(arg => BrowseCommandMethod(arg));
            EnterCommand = new RelayCommand<object>(arg => EnterCommandMethod(arg));
            
            KADR = KADR.EmptyField();
            SHTAT = KADR.EmptyField();
            SRP_PROF = KADR.EmptyField();
            AA042 = AA042.EmptyField();

            KadrPath = " ";
            ShtatPath = " ";
            Srp_prfPath = " ";
            Aa042Path = " ";
            /*KADR.Path = @"D:\Kadry\ok\kadr.dbf";
            SHTAT.Path = @"D:\Kadry\ok\Shtat.dbf";
            SRP_PROF.Path = @"D:\Kadry\ok\SPR_PROF.dbf";/**/
        }

        private void Load(bool v)
        {
            if (v)
            {
                Visible = Visibility.Visible;
                EnableWindow = false;
            }
            else
            {
                Visible = Visibility.Hidden;
                EnableWindow = true;
            }
        }

        private void EnterCommandMethod(object arg)
        {
            Load(true);
            switch (arg.ToString().ToLower())
            {
                case "kadr":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            KADR.Path = KadrPath;
                            KADR.InitStruct(fieldsKADR);
                            KADR = KADR.PreImport(KadrPath);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "shtat":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            SHTAT.Path = ShtatPath;
                            SHTAT.InitStruct(fieldsSHTAT);
                            SHTAT = SHTAT.PreImport(ShtatPath);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "spr_prof":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            SRP_PROF.Path = Srp_prfPath;
                            SRP_PROF.InitStruct(fieldsSRP_PROFR);
                            SRP_PROF = SRP_PROF.PreImport(Srp_prfPath);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "aa042":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            AA042.Path = Aa042Path;
                            AA042.InitStruct(fieldsAA042);
                            AA042 = AA042.PreImport(Aa042Path);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                default: { return; }
            }
            Load(false);
        }

        private void ImportCommandMethod(object arg)
        {
            Load(true);
            ImportSQL sql = new ImportSQL();
            switch (arg.ToString().ToLower())
            {
                case "kadr":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            List<Dictionary<string, string>> kd = KADR.StartImport();
                            sql.ImportKadr(kd, fieldsKADR);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            KADR = KADR.CompliteImport();
                            KadrPath = " ";
                            Load(false);
                        };
                        
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "shtat":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            List<Dictionary<string, string>> kdShtat = SHTAT.StartImport();
                            sql.ImportShtat(kdShtat, fieldsSHTAT);
                        };

                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            SHTAT = SHTAT.CompliteImport();
                            ShtatPath = " ";
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "aa042":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            List<Dictionary<string, string>> kdAA042 = AA042.StartImport();
                            sql.ImportAA042(kdAA042, fieldsAA042);
                        };

                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            AA042 = AA042.CompliteImport();
                            Aa042Path = " ";
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                case "spr_prof":
                    {
                        bwSave.DoWork += (sender, args) =>
                        {
                            List<Dictionary<string, string>> kd = SRP_PROF.StartImport();
                            sql.ImportProf(kd, fieldsSRP_PROFR);
                        };
                        bwSave.RunWorkerCompleted += (sender, args) =>
                        {
                            SRP_PROF = SRP_PROF.CompliteImport();
                            Srp_prfPath = " ";
                            Load(false);
                        };
                        bwSave.RunWorkerAsync();
                        break;
                    }
                default: break;
            }
        }

        private void BrowseCommandMethod(object arg)
        {
            Load(true);
            OpenFileDialog ofd = new OpenFileDialog()
            {
                Filter = "DBF files (*.DBF)|*.dbf|XLS files(переименованые из *.dbf) (*.XLS)|*.xls",
                FilterIndex = 0,
                Multiselect = false
            };
            switch (arg.ToString().ToLower())
            {
                case "kadr":
                    {
                        KADR.InitStruct(fieldsKADR);
                        ofd.InitialDirectory = (KadrPath.Length > 1)?Path.GetDirectoryName(KadrPath) :"";
                        if (ofd.ShowDialog() == false) break;
                        KadrPath = ofd.FileName;
                        KADR = KADR.PreImport(ofd.FileName);
                        break;
                    }
                case "spr_prof":
                    {
                        SRP_PROF.InitStruct(fieldsSRP_PROFR);
                        ofd.InitialDirectory = (Srp_prfPath.Length > 1) ? Path.GetDirectoryName(Srp_prfPath) : "";
                        if (ofd.ShowDialog() == false) break;
                        Srp_prfPath = ofd.FileName;
                        SRP_PROF = SRP_PROF.PreImport(ofd.FileName);
                        break;
                    }
                case "shtat":
                    {
                        SHTAT.InitStruct(fieldsSHTAT);
                        ofd.InitialDirectory = (ShtatPath.Length > 1) ? Path.GetDirectoryName(ShtatPath) : "";
                        if (ofd.ShowDialog() == false) break;
                        SHTAT = SHTAT.PreImport(ofd.FileName);
                        ShtatPath = ofd.FileName;
                        break;
                    }
                case "aa042":
                    {
                        AA042.InitStruct(fieldsAA042);
                        ofd.InitialDirectory = (Aa042Path.Length > 1) ? Path.GetDirectoryName(Aa042Path) : "";
                        if (ofd.ShowDialog() == false) break;
                        Aa042Path = ofd.FileName;
                        AA042 = AA042.PreImport(ofd.FileName);
                        break;
                    }
                    
                default: { break; }
            }
            Load(false);
        }

    }
}
