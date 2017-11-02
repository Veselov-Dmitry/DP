using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelUsers : ViewModelBase
    {
        //ПОЛЯ
        private int _personnel_Number;
        private string _login;
        private int _user_ID;
        //СВОЙСТВА

        public int Personnel_Number
        {
            get { return _personnel_Number; }
            set { _personnel_Number = value; OnPropertyChanged("_personnel_Number"); }
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
        public ViewModelDelUsers(CommonClass cc) : base()
        {
            Personnel_Number = cc.Personnel_Number;
            Login = cc.Login;
            User_ID = cc.User_ID;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        //МЕТОДЫ
        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Users Us = (from us in DB.Users where us.User_ID == User_ID select us).FirstOrDefault();
                DB.Users.Remove(Us);
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
