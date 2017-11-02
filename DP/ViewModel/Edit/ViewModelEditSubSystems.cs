using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditSubSystems : ViewModelBase
    {
        //ПОЛЯ
        private string _sub_System;
        private int _sub_System_ID;

        //СВОЙСТВА
        public string Sub_System
        {
            get { return _sub_System; }
            set
            {
                _sub_System = value;
                OnPropertyChanged("_sub_System");
            }
        }
        public int Sub_System_ID
        {
            get { return _sub_System_ID; }
            set
            {
                _sub_System_ID = value;
                OnPropertyChanged("_sub_System_ID");
            }
        }

        //КОНСТРУКТОР
        public ViewModelEditSubSystems(CommonClass cc) : base()
        {
            Sub_System = cc.Sub_System;
            Sub_System_ID = cc.Sub_System_ID;

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            if (String.IsNullOrWhiteSpace(Sub_System))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название подсистемы");
                return;
            }
            try
            {
                SubSystems Ss = (from ss in DB.SubSystems where ss.Sub_System_ID == Sub_System_ID select ss).FirstOrDefault();
                Ss.Sub_System = Sub_System;
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
