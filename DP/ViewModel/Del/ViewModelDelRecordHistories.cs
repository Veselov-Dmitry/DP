using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelRecordHistories : ViewModelBase
    {//ПОЛЯ
        private string _pS_Name;
        private int _record_ID;
        private string _note;

        //СВОЙСТВА
        public string PS_Name
        {
            get { return _pS_Name; }
            set { _pS_Name = value; OnPropertyChanged("_pS_Name"); }
        }
        public int Record_ID
        {
            get { return _record_ID; }
            set { _record_ID = value; OnPropertyChanged("_record_ID"); }
        }
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged("_note"); }
        }

        //КОНСТРУКТОР
        public ViewModelDelRecordHistories(CommonClass cc) : base()
        {
            PS_Name = cc.PS_Name;
            Record_ID = cc.Record_ID;
            Note = cc.Note;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        //МЕТОДЫ
        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                RecordHistories Rc = (from rc in DB.RecordHistories where rc.Record_ID == Record_ID select rc).FirstOrDefault();
                DB.RecordHistories.Remove(Rc);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }
    }
}
