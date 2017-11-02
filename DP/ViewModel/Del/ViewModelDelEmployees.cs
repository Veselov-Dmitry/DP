using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;
using System.Windows;

namespace DP.ViewModel.Del
{
    class ViewModelDelEmployees : ViewModelBase
    {
        private int _selectedOffice;
        private int _selectedProfession;
        private string _full_Name_Employee;
        private int _personnel_Number;
        private string _name_Department;
        private int _employee_ID;
        private string _name_Office;
        private string _name_Profession;
        
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
        public int Employee_ID
        {
            get { return _employee_ID; }
            set { _employee_ID = value; OnPropertyChanged("_employee_ID"); }
        }
        public string Name_Office
        {
            get { return _name_Office; }
            set { _name_Office = value; OnPropertyChanged("_name_Office"); }
        }
        public string Name_Profession
        {
            get { return _name_Profession; }
            set { _name_Profession = value; OnPropertyChanged("_name_Profession"); }
        }

        public ViewModelDelEmployees(CommonClass cc) : base()
        {
            Full_Name_Employee = cc.Full_Name_Employee;
            Personnel_Number = cc.Personnel_Number;
            Employee_ID = cc.Employee_ID;
            Name_Office = cc.Name_Office;
            Name_Profession = cc.Name_Profession;
            Name_Department = cc.Name_Department;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if(System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Employees Emp = (from emp in DB.Employees where emp.Employee_ID == Employee_ID select emp).FirstOrDefault();
                DB.Employees.Remove(Emp);
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
