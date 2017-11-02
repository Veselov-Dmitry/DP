using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddProfessions : ViewModelBase
    {
        //ПОЛЯ
        private string _name_Profession;

        //СВОЙСТВА
        public string Name_Profession
        {
            get { return _name_Profession; }
            set
            {
                _name_Profession = value;
                OnPropertyChanged("_name_Profession");
            }
        }

        //КОНСТРУКТОР
        public ViewModelAddProfessions() : base()
        {
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(Name_Profession))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название профессии");
                return;
            }
            try
            {
                DB.Professions.Add(new Professions { Name_Profession = Name_Profession });
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
