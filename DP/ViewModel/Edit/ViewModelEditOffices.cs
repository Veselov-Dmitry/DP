using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditOffices : ViewModelBase
    {
        private ObservableCollection<CommonClass> _departments;
        private int _selectedDepartment;
        private string _name_Office;
        private int _office_ID;

        public ICommand SelectOffCommand { get; set; }

        public ObservableCollection<CommonClass> Departments
        {
            get { return _departments; }
            set { _departments = value; OnPropertyChanged("_departments"); }
        }
        public int SelectedDepartment
        {
            get { return _selectedDepartment; }
            set { _selectedDepartment = value; OnPropertyChanged("_selectedDepartment"); }
        }
        public string Name_Office
        {
            get { return _name_Office; }
            set { _name_Office = value; OnPropertyChanged("_name_Office"); }
        }
        public int Office_ID
        {
            get { return _office_ID; }
            set { _office_ID = value; OnPropertyChanged("_office_ID"); }
        }


        public ViewModelEditOffices(CommonClass cc) : base()
        {
            Departments = new ObservableCollection<CommonClass>(GetDepartments4View());
            Name_Office = cc.Name_Office;
            Office_ID = cc.Office_ID;
            SelectedDepartment = Departments.IndexOf(Departments.Where(x => x.Department_ID == cc.Department_ID).FirstOrDefault());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {

            if (SelectedDepartment == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите отдел");
                return;
            }
            else if (String.IsNullOrWhiteSpace(Name_Office))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название бюро");
                return;
            }
            try
            {
                Offices Off = (from off in DB.Offices where off.Office_ID == Office_ID select off).FirstOrDefault();
                Off.Name_Office = Name_Office;
                Off.Department_ID = Departments[SelectedDepartment].Department_ID;
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetDepartments4View()
        {
            var Dep = (from dep in DB.Departments
                       select new CommonClass
                       {
                           Name_Department = dep.Name_Department,
                           Department_ID = dep.Department_ID
                       }).ToList();
            return Dep;
        }
    }
}
