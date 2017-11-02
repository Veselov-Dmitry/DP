using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelPrgLangs : ViewModelBase
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

        public ViewModelDelPrgLangs(CommonClass cc) : base()
        {
            Prg_Lang = cc.Prg_Lang;
            Prg_Lang_ID = cc.Prg_Lang_ID;
            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                PrgLangs Prl = (from prl in DB.PrgLangs where prl.Prg_Lang_ID == Prg_Lang_ID select prl).FirstOrDefault();
                DB.PrgLangs.Remove(Prl);
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
