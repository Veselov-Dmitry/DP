using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace DP.ViewModel.Add
{
    class ViewModelAddDevelopers : ViewModelBase
    {
        private int _selectedIndexRec;
        private int _selectedIndexPersNum;
        private DateTime _date_start;
        private string _pS_Name;
        private string _note;

        public ICommand SelectRecCommand { get; set; }

        public ObservableCollection<CommonClass> RecordHistories { get; set; }
        public ObservableCollection<CommonClass> Employees { get; set; }

        public int SelectedIndexRec
        {
            get { return _selectedIndexRec; }
            set
            {
                _selectedIndexRec = value;
                OnPropertyChanged("_selectedIndexRec");
            }
        }
        public int SelectedIndexPersNum
        {
            get { return _selectedIndexPersNum; }
            set
            {
                _selectedIndexPersNum = value;
                OnPropertyChanged("_selectedIndexPersNum");
            }
        }

        public DateTime Date_start
        {
            get { return _date_start; }
            set
            {
                _date_start = value;
                OnPropertyChanged("Date_start");
            }
        }
        public string PS_Name
        {
            get { return _pS_Name; }
            set
            {
                _pS_Name = value;
                OnPropertyChanged("PS_Name");
            }
        }
        public string Note
        {
            get { return _note; }
            set
            {
                _note = value;
                OnPropertyChanged("Note");
            }
        }

        public ViewModelAddDevelopers() : base()
        {
            Date_start = DateTime.Now;
            RecordHistories = new ObservableCollection<CommonClass>(GetRecordHistories4DB());
            Employees = new ObservableCollection<CommonClass>(GetEmployees4DB());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
            SelectRecCommand = new RelayCommand<object>(arg => SelectRecCommandExecute());
            SelectedIndexRec = 0;
        }

        private List<CommonClass> GetEmployees4DB()
        {
            var Emp = (from emp in DB.Employees select new CommonClass { Personnel_Number = emp.Personnel_Number, Employee_ID = emp.Employee_ID }).ToList();
            return Emp;
        }

        private List<CommonClass> GetRecordHistories4DB()
        {
            var Rec = (from rec in DB.RecordHistories
                       from ps in DB.PrgSystems
                       where ps.PS_ID == rec.PS_ID
                       select new CommonClass { Record_ID = rec.Record_ID, Note = rec.Note, PS_Name = ps.PS_Name, PS_ID = ps.PS_ID }).ToList();
            return Rec;
        }

        private void SelectRecCommandExecute()
        {
            int rec_id = RecordHistories[SelectedIndexRec].Record_ID;
            CommonClass sel = (from rec in DB.RecordHistories
                               from ps in DB.PrgSystems
                               where rec.Record_ID == rec_id
                               && ps.PS_ID == rec.PS_ID
                               select new CommonClass
                               {
                                   PS_Name = ps.PS_Name,
                                   Note = rec.Note
                               }).FirstOrDefault();

            Note = sel.Note;
            PS_Name = sel.PS_Name;
        }

        private void SaveCommandExecute()
        {
            if (SelectedIndexRec == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите запись от изменении");
                return;
            }
            else
            if (Date_start == DateTime.MinValue)
            {
                if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Создать запись без даты?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                    CloseMethod();
            }
            try
            {
                Developers Dev = new Developers
                {
                    Record_ID = RecordHistories[SelectedIndexRec].Record_ID,
                    Date_start = (Date_start == DateTime.MinValue) ? default(DateTime) : Date_start,
                    Employee_ID = Employees[SelectedIndexPersNum].Employee_ID
                };
                DB.Developers.Add(Dev);
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
