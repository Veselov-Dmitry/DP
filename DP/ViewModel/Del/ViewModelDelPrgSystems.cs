using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelPrgSystems : ViewModelBase
    {
        //ПОЛЯ
        private ObservableCollection<CommonClass> _prgLangs;
        private Nullable<int> _pS_ID;
        private string _pS_Name;
        private string _prg_Lang;

        //СВОЙСТВА
        public ObservableCollection<CommonClass> PrgLangs
        {
            get { return _prgLangs; }
            set { _prgLangs = value; OnPropertyChanged("_prgLangs"); }
        }
        public Nullable<int> PS_ID
        {
            get { return _pS_ID; }
            set { _pS_ID = value; OnPropertyChanged("_pS_ID"); }
        }
        public string PS_Name
        {
            get { return _pS_Name; }
            set { _pS_Name = value; OnPropertyChanged("_pS_Name"); }
        }
        public string Prg_Lang
        {
            get { return _prg_Lang; }
            set { _prg_Lang = value; OnPropertyChanged("_prg_Lang"); }
        }

        //КОНСТРУКТОР
        public ViewModelDelPrgSystems(CommonClass cc) : base()
        {
            Prg_Lang = cc.Prg_Lang;
            PS_ID = cc.PS_ID;
            PS_Name = cc.PS_Name;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        //МЕТОДЫ
        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                PrgSystems Ps = (from ps in DB.PrgSystems where ps.PS_ID == PS_ID select ps).FirstOrDefault();
                DB.PrgSystems.Remove(Ps);
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
