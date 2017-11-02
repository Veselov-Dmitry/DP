using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddRecordHistories : ViewModelBase
    {// Record_ID
        //ПОЛЯ
        private ObservableCollection<CommonClass> _prgSystems;
        private int _selectedPrgSystems;
        private string _note;
        //СВОЙСТВА

        public ObservableCollection<CommonClass> PrgSystems
        {
            get { return _prgSystems; }
            set { _prgSystems = value; OnPropertyChanged("_prgSystems"); }
        }
        public int SelectedPrgSystems
        {
            get { return _selectedPrgSystems; }
            set { _selectedPrgSystems = value; OnPropertyChanged("_selectedPrgSystems"); }
        }
        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged("_note"); }
        }

        //КОНСТРУКТОР
        public ViewModelAddRecordHistories() : base()
        {
            PrgSystems = new ObservableCollection<CommonClass>(GetPrgSystems4View());

            SelectedPrgSystems = 0;

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {

            if (SelectedPrgSystems == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите язык прораммы");
                return;
            }
            else if (String.IsNullOrWhiteSpace(Note))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите изменение");
                return;
            }
            try
            {

                RecordHistories rc = new RecordHistories
                {
                    Note = Note,
                    PS_ID = PrgSystems[SelectedPrgSystems].PS_ID.Value
                };
                DB.RecordHistories.Add(rc);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetPrgSystems4View()
        {
            return (from ps in DB.PrgSystems
                    select new CommonClass
                    {
                        PS_ID = ps.PS_ID,
                        PS_Name = ps.PS_Name
                    }).ToList();
        }
    }
}
