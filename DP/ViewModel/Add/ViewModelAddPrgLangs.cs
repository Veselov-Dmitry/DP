using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddPrgLangs : ViewModelBase
    {
            //Prg_Lang_ID
        private string _prg_Lang;
        public string Prg_Lang
        {
            get { return _prg_Lang; }
            set
            {
                _prg_Lang = value; OnPropertyChanged("_prg_Lang");
            }
        }

        public ViewModelAddPrgLangs() : base()
        {
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(Prg_Lang))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название языка программирования");
                return;
            }
            try
            {
                DB.PrgLangs.Add(new PrgLangs { Prg_Lang = Prg_Lang });
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
