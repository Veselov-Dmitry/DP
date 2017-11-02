using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddOffices : ViewModelBase
    {
        private ObservableCollection<CommonClass> _departments;
        private int _selectedDepartment;
        private string _name_Office;

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


        public ViewModelAddOffices() : base()
        {
            Departments = new ObservableCollection<CommonClass>(GetDepartments4View());

            SelectedDepartment = 1;

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

                Offices Off = new Offices
                {
                    Name_Office = Name_Office,
                    Department_ID = Departments[SelectedDepartment].Department_ID
                };
                DB.Offices.Add(Off);
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
            var Dep = (from dep in DB.Departments select new CommonClass
            {
                Name_Department = dep.Name_Department,
                Department_ID = dep.Department_ID
            }).ToList();
            return Dep;
        }
    }
}
