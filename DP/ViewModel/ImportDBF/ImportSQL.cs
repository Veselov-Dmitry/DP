using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DP.ViewModel.ImportDBF
{
    class ImportSQL : ViewModelBase
    {
        public ImportSQL()
        {
        }
        public void ImportKadr(List<Dictionary<string, string>> profDictionary, string[] ColumnsNames)
        {
            //ColumnsNames = new[] {  0 "TAB", 1 "FAM", 2 "IM", 3 "OT", 4 "DATAUV", 5 "KODPROF", 6 "KODDOLZN" };
            // endOfImport = -1 обнуляет запомненные записи и блокитрует таблицу
            ObjectParameter islock = new ObjectParameter("Lock", typeof(int));
            try
            {
                int endOfImport = -1;
                DB.IMPORT_EMPLOYEES( null, null, null, endOfImport, islock);
                if (islock.Value.ToString() == "-1") throw new Exception("lock");
                StringBuilder sb = new StringBuilder();// fio
                //endOfImport = 0 - добавляет запись и запоминает
                endOfImport = 0;
                int ID = 0;
                int prof = 0;
                int count = 0;
                foreach (Dictionary<string, string> RowDict in profDictionary)
                {
                    count++;
                    if (String.IsNullOrWhiteSpace(RowDict[ColumnsNames[4]]))
                    {
                        Int32.TryParse(RowDict[ColumnsNames[0]], out ID);
                        Int32.TryParse(RowDict[ColumnsNames[5]], out prof);
                        if (prof == 0)
                            Int32.TryParse(RowDict[ColumnsNames[6]], out prof);

                        //if (ID != 0)
                        {
                            sb.Clear().Append(RowDict[ColumnsNames[1]] + " " + RowDict[ColumnsNames[2]] + " " + RowDict[ColumnsNames[3]]);
                            DB.IMPORT_EMPLOYEES(
                            (sb.Length > 200) ? sb.ToString(0, 200) : sb.ToString(),
                            ID, (prof == 0) ? default(int?) : prof, endOfImport, islock);
                            
                        }
                    }

                }
                // endOfImport = 1 - очистка таблицы от не запомненных записей (не актуальных)
                endOfImport = 1;
                DB.IMPORT_EMPLOYEES( null, null, null, endOfImport, islock);
                DB.SaveChanges();
                //System.Windows.Forms.MessageBox.Show("total=" + (DateTime.Now - d1).ToString(@"mm\:ss\.ff")+
                //    "\nEF=" + d3.ToString(@"mm\:ss\.ff") +
                //    "\nClear=" + (DateTime.Now - d1 - d3).ToString(@"mm\:ss\.ff") );
            }
            catch(Exception ex)
            {
                if (ex.Message == "lock")// REMOVE = -2 - РАЗБЛОКИРОВКА И  очистка таблицы от не запомненных записей (не актуальных)
                {
                    if (System.Windows.Forms.MessageBox.Show("Таблица импорта заблокирована.\n" +
                      "Что бы отменить импорт нажмите Нет\nЕсли вы уверены что больше никто не" +
                      " пользуется импортом, то чтобы разблокировать нажмите Да и повторите импорт",
                      "Разблокировка", System.Windows.Forms.MessageBoxButtons.YesNo,
                      System.Windows.Forms.MessageBoxIcon.Question) ==
                      System.Windows.Forms.DialogResult.Yes)
                        DB.IMPORT_EMPLOYEES(null, null, null, -2, islock);
                }
                else
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + "\n произошла ошибка: \n" + ex.Message + "\nподробно: \n" + ex.InnerException.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            }
        }

        public void ImportShtat(List<Dictionary<string, string>> profDicShtat, string[] col)
        {  
            // endOfImport = -1 обнуляет запомненные записи и блокитрует таблицу
            ObjectParameter islock = new ObjectParameter("Lock", typeof(int));
            try
            {
                int endOfImport = -1;
                int IDCeh = 0;
                int IDUch = 0;
                int PersNum = 0;
                DB.IMPORT_EMPLOYEE_PLACEMENT(null, null, null, endOfImport, islock);
                if (islock.Value.ToString() == "-1") throw new Exception("lock");
                StringBuilder sb = new StringBuilder();// fio
                //endOfImport = 0 - добавляет запись и запоминает
                endOfImport = 0;

                //col = new[] { 0 "CEX", 1 "UCH", 2 "TABN" , 3 "FIO" };
                foreach (Dictionary<string, string> row in profDicShtat)
                {
                    if (!String.IsNullOrEmpty(row[col[3]]))
                    {
                        Int32.TryParse(row[col[0]], out IDCeh);
                        Int32.TryParse(row[col[1]], out IDUch);
                        Int32.TryParse(row[col[2]], out PersNum);
                        DB.IMPORT_EMPLOYEE_PLACEMENT(
                            IDCeh,
                            IDUch,
                            PersNum,
                            endOfImport,
                            islock);
                    }
                }
                // endOfImport = 1 - очистка таблицы от не запомненных записей (не актуальных)
                endOfImport = 1;
                DB.IMPORT_EMPLOYEE_PLACEMENT(null, null, null, endOfImport, islock);
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.Message == "lock")// REMOVE = -2 - РАЗБЛОКИРОВКА И  очистка таблицы от не запомненных записей (не актуальных)
                {  if (System.Windows.Forms.MessageBox.Show("Таблица импорта заблокирована.\n" +
                    "Что бы отменить импорт нажмите Нет\nЕсли вы уверены что больше никто не" +
                    " пользуется импортом, то чтобы разблокировать нажмите Да и повторите импорт",
                    "Разблокировка", System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question) ==
                    System.Windows.Forms.DialogResult.Yes)
                        DB.IMPORT_EMPLOYEES(null, null, null, -2, islock); }
                else
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + "\n произошла ошибка: \n" + ex.Message + "\nподробно: \n" + ex.InnerException.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            }/**/
        }

        public void ImportProf(List<Dictionary<string, string>> profDictionary, string[] ColumnsNames)
        {
            //string[] ColumnsName = new[] { "KOD", "NAIM_PECH" };
            // endOfImport = -1 обнуляет запомненные записи
            ObjectParameter islock = new ObjectParameter("Lock", typeof(int));
            try
            {
                int endOfImport = -1;                
                DB.IMPORT_PROFESSIONS(null, null, endOfImport, islock);
                if (islock.Value.ToString() == "-1") throw new Exception("lock");
                StringBuilder sb = new StringBuilder();
                //endOfImport = 0 - добавляет запись и запоминает
                endOfImport = 0;
                foreach (Dictionary<string, string> RowDict in profDictionary)
                {// 
                    int ID = 0;
                    Int32.TryParse(RowDict[ColumnsNames[0]], out ID);
                    if (ID != 0)
                    {
                        sb.Clear().Append(RowDict[ColumnsNames[1]]);
                        DB.IMPORT_PROFESSIONS((sb.Length > 100) ? sb.ToString(0, 100) : sb.ToString(), ID, endOfImport, islock);
                    }

                }
                // endOfImport = 1 - очистка таблицы от не запомненных записей (не актуальных)
                endOfImport = 1;
                DB.IMPORT_PROFESSIONS(null, null, endOfImport, islock);
                DB.SaveChanges();
            }
            catch(Exception ex)
            {// REMOVE = -2 - очистка таблицы от не запомненных записей (не актуальных)
                if (ex.Message == "lock")
                {
                    if (System.Windows.Forms.MessageBox.Show("Таблица импорта заблокирована.\n" +
                         "Что бы отменить импорт нажмите Нет\nЕсли вы уверены что больше никто не" +
                         " пользуется импортом, то чтобы разблокировать нажмите Да и повторите импорт",
                         "Разблокировка", System.Windows.Forms.MessageBoxButtons.YesNo,
                         System.Windows.Forms.MessageBoxIcon.Question) ==
                         System.Windows.Forms.DialogResult.Yes)
                        DB.IMPORT_PROFESSIONS(null, null, -2, islock);
                }
                else
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + "\n произошла ошибка: \n" + ex.Message + "\nподробно: \n" + ex.InnerException.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            }
        }

        public void ImportAA042(List<Dictionary<string, string>> profDictionary, string[] col)
        {  
            ObjectParameter islock = new ObjectParameter("Lock", typeof(int));
            try
            {
                //---------------импорт отделов
                // endOfImport = -1 обнуляет запомненные записи и блокитрует таблицу
                int endOfImport = -1;
                int IDCeh = 0;
                int IDUch = 0;
                DB.IMPORT_DEPARTMENTS(null, null, endOfImport, islock);
                if (islock.Value.ToString() == "-1") throw new Exception("lock");
                StringBuilder sb = new StringBuilder();// fio
                //endOfImport = 0 - добавляет запись и запоминает
                endOfImport = 0;
                //col =  new[] { 0 "CEX", 1 "UCH", 2 "NAMESOKR", 3 "NAMEPOLN" }; 
                foreach (Dictionary<string, string> row in profDictionary)
                {// 
                    if(row[col[1]] == "00")
                    {
                        Int32.TryParse(row[col[0]], out IDCeh);
                        DB.IMPORT_DEPARTMENTS(
                            (row[col[2]].Length > 200)? row[col[2]].Substring(0,200): row[col[2]],
                            IDCeh, 
                            endOfImport, 
                            islock);
                    }
                }
                // endOfImport = 1 - очистка таблицы от не запомненных записей (не актуальных)
                endOfImport = 1;
                DB.IMPORT_DEPARTMENTS(null, null, endOfImport, islock);

                //---------------импорт бюро
                // endOfImport = -1 обнуляет запомненные записи и блокитрует таблицу
                endOfImport = -1;
                DB.IMPORT_OFFICES(null, null, null, endOfImport, islock);
                //endOfImport = 0 - добавляет запись и запоминает
                endOfImport = 0;
                //col =  new[] { 0 "CEX", 1 "UCH", 2 "NAMESOKR", 3 "NAMEPOLN" }; 
                foreach (Dictionary<string, string> row in profDictionary)
                {// 
                    if (row[col[1]] != "00")
                    {
                        Int32.TryParse(row[col[0]], out IDCeh);
                        Int32.TryParse(row[col[1]], out IDUch);
                        
                        DB.IMPORT_OFFICES(
                            IDCeh, 
                            IDUch,
                            (row[col[3]].Length > 200) ? row[col[3]].Substring(0, 200) : row[col[3]],
                            endOfImport, 
                            islock);
                    }
                }
                // endOfImport = 1 - очистка таблицы от не запомненных записей (не актуальных)
                endOfImport = 1;
                DB.IMPORT_OFFICES(null, null, null, endOfImport, islock);
                DB.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.Message == "lock")// REMOVE = -2 - РАЗБЛОКИРОВКА И  очистка таблицы от не запомненных записей (не актуальных)
                {
                    if (System.Windows.Forms.MessageBox.Show("Таблица импорта заблокирована.\n" +
                     "Что бы отменить импорт нажмите Нет\nЕсли вы уверены что больше никто не" +
                     " пользуется импортом, то чтобы разблокировать нажмите Да и повторите импорт",
                     "Разблокировка", System.Windows.Forms.MessageBoxButtons.YesNo,
                     System.Windows.Forms.MessageBoxIcon.Question) ==
                     System.Windows.Forms.DialogResult.Yes)
                        DB.IMPORT_EMPLOYEES(null, null, null, -2, islock);
                }
                else
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + "\n произошла ошибка: \n" + ex.Message + "\nподробно: \n" + ex.InnerException.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

            }/**/
        }
    }
}
