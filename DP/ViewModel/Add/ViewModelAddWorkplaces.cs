using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;


namespace DP.ViewModel.Add
{
    class ViewModelAddWorkplaces : ViewModelBase
    {
        private ObservableCollection<CommonClass> _offices;
        private ObservableCollection<CommonClass> _employees;
        private int _selectedOffice; 
        private int _selectedEmployee;
        private string _floor;
        private string _name_Department;
        private string _housing;
        private string _telephone;

        public ICommand SelectOffCommand { get; set; }

        public ObservableCollection<CommonClass> Offices
        {
            get { return _offices; }
            set { _offices = value; OnPropertyChanged("Offices"); }
        }
        public ObservableCollection<CommonClass> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("_employees"); }
        }
        public int SelectedOffice
        {
            get { return _selectedOffice; }
            set { _selectedOffice = value; OnPropertyChanged("_selectedOffice"); }
        }
        public int SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("_selectedEmployee"); }
        }        
        public string Name_Department
        {
            get { return _name_Department; }
            set { _name_Department = value; OnPropertyChanged("_name_Department"); }
        }
        public string Floor
        {
            get { return _floor; }
            set { _floor = value; OnPropertyChanged("_floor"); }
        }
        public string Housing
        {
            get { return _housing; }
            set { _housing = value; OnPropertyChanged("_housing"); }
        }
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; OnPropertyChanged("_telephone"); }
        }


        public ViewModelAddWorkplaces() : base()
        {
            Offices = new ObservableCollection<CommonClass>(GetOffices4View());
            Offices.Insert(0, new CommonClass { Name_Office = "(пусто)" });
            Employees = new ObservableCollection<CommonClass>(GetEmployees4View());
            Employees.Insert(0, new CommonClass { Personnel_Number = 0 });

            SelectedOffice = 0;
            SelectedEmployee = 0;

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
            SelectOffCommand = new RelayCommand<object>(arg => SelectOffCommandExecute());
        }

        private void SelectOffCommandExecute()
        {
            int id = Offices[SelectedOffice].Office_ID;
            Name_Department = (from dep in DB.Departments
                               from off in DB.Offices
                               where off.Office_ID == id
                               && dep.Department_ID == off.Department_ID
                               select dep.Name_Department).FirstOrDefault();
        }

        private void SaveCommandExecute()
        {
            try
            {
                Workplaces Wrk = new Workplaces
                {
                    Office_ID = (SelectedOffice != 0) ? Offices[SelectedOffice].Office_ID : default(int?),
                    Employee_ID = (SelectedEmployee != 0) ? Employees[SelectedEmployee].Employee_ID : default(int?),
                    Floor = Floor,
                    Housing = Housing,
                    Telephone = Telephone
                };
                DB.Workplaces.Add(Wrk);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetOffices4View()
        {
            var Off = (from off in DB.Offices
                       select new CommonClass
                       {
                           Office_ID = off.Office_ID,
                           Name_Office = off.Name_Office
                       }).ToList();
            return Off;
        }
        private List<CommonClass> GetEmployees4View()
        {
            var Emp = (from emp in DB.Employees

                        select new CommonClass
                        {
                            Employee_ID = emp.Employee_ID,
                            Personnel_Number = emp.Personnel_Number
                        }).ToList();
            return Emp;
        }
    }
}
