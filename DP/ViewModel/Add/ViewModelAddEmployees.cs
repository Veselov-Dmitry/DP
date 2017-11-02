using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddEmployees : ViewModelBase
    {
        private ObservableCollection<CommonClass> _offices;
        private int _selectedOffice;
        private ObservableCollection<CommonClass> _professions;
        private int _selectedProfession;
        private string _full_Name_Employee;
        private int _personnel_Number;
        private string _name_Department;

        public ICommand SelectOffCommand { get; set; }

        public ObservableCollection<CommonClass> Offices
        {
            get { return _offices; }
            set { _offices = value; OnPropertyChanged("Offices"); }
        }
        public ObservableCollection<CommonClass> Professions
        {
            get { return _professions; }
            set { _professions = value; OnPropertyChanged("Professions"); }
        }
        public int SelectedOffice
        {
            get { return _selectedOffice; }
            set { _selectedOffice = value; OnPropertyChanged("_selectedOffice"); }
        }
        public int SelectedProfession
        {
            get { return _selectedProfession; }
            set { _selectedProfession = value; OnPropertyChanged("_selectedProfession"); }
        }
        public string Full_Name_Employee
        {
            get { return _full_Name_Employee; }
            set { _full_Name_Employee = value; OnPropertyChanged("_full_Name_Employee"); }
        }
        public int Personnel_Number
        {
            get { return _personnel_Number; }
            set { _personnel_Number = value; OnPropertyChanged("_personnel_Number"); }
        }
        public string Name_Department
        {
            get { return _name_Department; }
            set { _name_Department = value; OnPropertyChanged("Name_Department"); }
        }


        public ViewModelAddEmployees() : base()
        {
            Offices = new ObservableCollection<CommonClass>(GetOffices4View());
            Professions = new ObservableCollection<CommonClass>(GetProfessions4View());

            SelectedOffice = 1;
            SelectedProfession = 1;

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

            if (SelectedOffice == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите бюро");
                return;
            }
            else if (SelectedProfession == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите профессию");
                return;
            }
            try
            {
                Employees Emp = new Employees
                {
                    Office_ID = Offices[SelectedOffice].Office_ID,
                    Profession_ID = Professions[SelectedProfession].Profession_ID,
                    Full_Name_Employee = Full_Name_Employee,
                    Personnel_Number = Personnel_Number
                };
                DB.Employees.Add(Emp);
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
        private List<CommonClass> GetProfessions4View()
        {
            var Prof = (from prof in DB.Professions

                        select new CommonClass
                        {
                            Name_Profession = prof.Name_Profession,
                            Profession_ID = prof.Profession_ID
                        }).ToList();
            return Prof;
        }
    }
}
