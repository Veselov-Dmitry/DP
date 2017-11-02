using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddSubSystems : ViewModelBase
    {
        //ПОЛЯ
        private string _sub_System;

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

        //КОНСТРУКТОР
        public ViewModelAddSubSystems() : base()
        {
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(Sub_System))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название подсистемы");
                return;
            }
            try
            {
                DB.SubSystems.Add(new SubSystems { Sub_System = Sub_System });
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
