using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditPrgLangs : ViewModelBase
    {
        private string _prg_Lang;
        private int _prg_Lang_ID;

        public string Prg_Lang
        {
            get { return _prg_Lang; }
            set
            {
                _prg_Lang = value; OnPropertyChanged("_prg_Lang");
            }
        }
        public int Prg_Lang_ID
        {
            get { return _prg_Lang_ID; }
            set
            {
                _prg_Lang_ID = value; OnPropertyChanged("_prg_Lang_ID");
            }
        }

        public ViewModelEditPrgLangs(CommonClass cc) : base()
        {
            Prg_Lang = cc.Prg_Lang;
            Prg_Lang_ID = cc.Prg_Lang_ID;
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
                PrgLangs Prl = (from prl in DB.PrgLangs where prl.Prg_Lang_ID == Prg_Lang_ID select prl).FirstOrDefault();
                Prl.Prg_Lang = Prg_Lang;
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
