using System.Windows;
using System.Windows.Input;
using DP.View;
using System.ComponentModel;
using System.Windows.Controls;
using DP.Model;
using System;
using System.Data.Entity.Core.EntityClient;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Net.NetworkInformation;

namespace DP.ViewModel.Autorisation
{

    class ViewModelAutorisation : ViewModelBase
    {

        private int _index = 0;
        private string _serverPath;
        private bool _editServer = false;
        
        private bool _dResult = false;
        public bool DResult
        {
            get { return _dResult; }
            set { _dResult = value; OnPropertyChanged("DResult"); }
        }
        public int Index
        {
            get
            {
                _index = SingleConnection.LoginSelect;
                return _index;
            }
            set
            {
                _index = value; OnPropertyChanged("Index");
                SingleConnection.LoginSelect = _index;
            }
        }
        public string ServerPath
        {
            get
            {
                _serverPath = SingleConnection.DataBase;
                return _serverPath;
            }
            set
            {
                _serverPath = value;
                OnPropertyChanged("ServerPath");
                SingleConnection.DataBase = value;
            }
        }
        public bool EditServer
        {
            get { return _editServer; }
            set { _editServer = value; OnPropertyChanged("EditServer"); }
        }
        


        public ICommand LoginCommand { get; set; }
        public ICommand CheckCommand { get; set; }
        
        public ViewModelAutorisation()
        {
            CheckCommand = new RelayCommand<object>(arg => CheckCommandMethod());
            LoginCommand = new RelayCommand<object>(arg => LoginCommandMethod(arg));
            ExitCommand = new RelayCommand<object>(arg => ExitCommandMethod(arg));
        }

        private void CheckCommandMethod()
        {
            EditServer = EditServer ? false : true;
        }

        private void ExitCommandMethod(object wnd)
        {
            Window w = wnd as Window;
            if (w != null)
            {
                DResult = false;
                w.Close();
            }
        }

        private bool i(object o)
        {
            if (o != null)
                return false;
            else
                return true;
        }

        private void LoginCommandMethod(object wnd)
        {
            Window w = wnd as Window;
            if(w != null)
            {
                Grid g1; StackPanel s1; StackPanel s2; PasswordBox p2;
                PasswordBox p1 = i(w) ? null :
                    i(g1 = w.Content as Grid) ? null :
                    i(s1 = g1.Children[0] as StackPanel) ? null :
                    i(s2 = s1.Children[0] as StackPanel) ? null :
                    i(p2 = s2.Children[3] as PasswordBox) ? null : p2 as PasswordBox;
                SingleConnection.LoginSelect = Index;
                
                if (p1.Password + ";" != SingleConnection.Logins.Split(new char[] { '=' })[2])
                {
                    MessageBox.Show("Неверный пароль","Неверный ввод");
                    return;
                }
                
                Exception exception;
                if (ConnectionTry(SingleConnection.ConString, out exception))
                {
                    MessageBox.Show("Не удалось установить связь с сервером:\n" + (exception.Message), "Проверка соединения");
                    return;
                }
                DResult = true; 
                w.Close();           
            }
        }

        private bool ConnectionTry(string conString, out Exception exception)
        {
            exception = null; 
            try
            {
                Ping pingSender = new Ping();
                string adr = SingleConnection.DataBase.Split('\\')[0];
                PingReply reply = pingSender.Send(adr);

                if (reply.Status != IPStatus.Success)
                    throw new Exception("Нет соединения с Базой данных");
                
                using (DepContext dbContext = new DepContext(SingleConnection.ConString))
                {
                    if (!dbContext.Database.Exists())
                    {
                        throw new Exception("База данных не существует.");
                    }
                }/**/
            }
            catch (Exception ex)
            {
                exception = ex;
                return true;
            }
            return false;
        }
    }

    class DepContext : DbContext
    {
        public DepContext(string s) : base(s) { }
        public DbSet<Test> Departments { get; set; }
    }

    public class Test
    {
        public string Name_Department { get; set; }
        public int Department_ID { get; set; }
    }
}
