using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddPrgSystems : ViewModelBase
    {
        //ПОЛЯ
        private ObservableCollection<CommonClass> _prgLangs;
        private int _selectedPrgLangs;
        private string _pS_Name;
        private string _prg_Lang;
        //СВОЙСТВА

        public ObservableCollection<CommonClass> PrgLangs
        {
            get { return _prgLangs; }
            set { _prgLangs = value; OnPropertyChanged("_prgLangs"); }
        }
        public int SelectedPrgLang
        {
            get { return _selectedPrgLangs; }
            set { _selectedPrgLangs = value; OnPropertyChanged("_selectedPrgLangs"); }
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
        public ViewModelAddPrgSystems() : base()
        {
            PrgLangs = new ObservableCollection<CommonClass>(GetPrgLangs4View());

            SelectedPrgLang = 0;

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {

            if (SelectedPrgLang == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите язык прораммы");
                return;
            }
            else if (String.IsNullOrWhiteSpace(PS_Name))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название программы");
                return;
            }
            try
            {

                PrgSystems Ps = new PrgSystems
                {
                    PS_Name = PS_Name,
                    Prg_Lang_ID = PrgLangs[SelectedPrgLang].Prg_Lang_ID
                };
                DB.PrgSystems.Add(Ps);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetPrgLangs4View()
        {
            return (from psl in DB.PrgLangs
                       select new CommonClass
                       {
                           Prg_Lang_ID = psl.Prg_Lang_ID,
                           Prg_Lang = psl.Prg_Lang
                       }).ToList();
        }
    }
}
