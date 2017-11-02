using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditUsers : ViewModelBase
    {
        //ПОЛЯ
        private ObservableCollection<CommonClass> _employees;
        private int _selectedEmployee;
        private string _login;
        private int _user_ID;
        //СВОЙСТВА

        public ObservableCollection<CommonClass> Employees
        {
            get { return _employees; }
            set { _employees = value; OnPropertyChanged("_employees"); }
        }
        public int SelectedEmployee
        {
            get { return _selectedEmployee; }
            set { _selectedEmployee = value; OnPropertyChanged("_selectedEmployee"); }
        }
        public string Login
        {
            get { return _login; }
            set { _login = value; OnPropertyChanged("_login"); }
        }        
        public int User_ID
        {
            get { return _user_ID; }
            set { _user_ID = value; OnPropertyChanged("_user_ID"); }
        }

        //КОНСТРУКТОР
        public ViewModelEditUsers(CommonClass cc) : base()
        {
            Employees = new ObservableCollection<CommonClass>(GetEmployees4View());
            Login = cc.Login;
            User_ID = cc.User_ID;
            SelectedEmployee = Employees.IndexOf(Employees.Where(x => x.Employee_ID == cc.Employee_ID).FirstOrDefault());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        //МЕТОДЫ
        private void SaveCommandExecute()
        {

            if (SelectedEmployee == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите работника");
                return;
            }
            try
            {
                Users Us = (from us in DB.Users where us.User_ID == User_ID select us).FirstOrDefault();
                Us.Login = Login;
                Us.Employee_ID = Employees[SelectedEmployee].Employee_ID;
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetEmployees4View()
        {
            return (from emp in DB.Employees
                    select new CommonClass
                    {
                        Employee_ID = emp.Employee_ID,
                        Personnel_Number = emp.Personnel_Number
                    }).ToList();
        }
    }
}
