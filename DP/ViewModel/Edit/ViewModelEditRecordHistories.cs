using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditRecordHistories : ViewModelBase
    {
        //ПОЛЯ
        private ObservableCollection<CommonClass> _prgSystems;
        private int _selectedPrgSystems;
        private int _record_ID;
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
        public ViewModelEditRecordHistories(CommonClass cc) : base()
        {
            PrgSystems = new ObservableCollection<CommonClass>(GetPrgSystems4View());
            Record_ID = cc.Record_ID;
            Note = cc.Note;
            SelectedPrgSystems = PrgSystems.IndexOf(PrgSystems.Where( x => x.PS_ID == cc.PS_ID).FirstOrDefault());
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
                RecordHistories Rc = (from rc in DB.RecordHistories where rc.Record_ID == Record_ID select rc).FirstOrDefault();
                Rc.Note = Note;
                Rc.PS_ID = PrgSystems[SelectedPrgSystems].PS_ID.Value;
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
