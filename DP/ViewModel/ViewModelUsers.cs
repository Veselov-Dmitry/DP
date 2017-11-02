using DP.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DP.ViewModel.Edit;
using DP.ViewModel.Add;
using DP.ViewModel.Del;
using DP.View;
using DP.View.Add;
using DP.View.Edit;
using DP.View.Del;
using System.Diagnostics;
using System;

namespace DP.ViewModel
{
    class ViewModelUsers : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddUsersCommand { get; set; }
        public ICommand DelUsersCommand { get; set; }
        public ICommand EditUsersCommand { get; set; }
        public ICommand OpenAccesRightUsersCommand { get; set; }
        
        //КОНСТРУКТОР
        public ViewModelUsers() : base()
        {
            AddUsersCommand = new RelayCommand<object>(arg => AddUsersCommandExecute());
            DelUsersCommand = new RelayCommand<object>(arg => DeleteUsersCommandExecute());
            EditUsersCommand = new RelayCommand<object>(arg => EditUsersCommandExecute());
            OpenAccesRightUsersCommand = new RelayCommand<object>(arg => OpenAccesRightUsersCommandExecute());
        }

        private void OpenAccesRightUsersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед просмотром выберите строку");
                return;
            }
            ViewModelAccesRightToUsers vm = new ViewModelAccesRightToUsers(SelectedRow);
            AccesRightToUsersView view = new AccesRightToUsersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        //МЕТОДЫ

        private void AddUsersCommandExecute()
        {
            ViewModelAddUsers vm = new Add.ViewModelAddUsers();
            AddUsersView view = new AddUsersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditUsersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditUsers vm = new ViewModelEditUsers(SelectedRow);
            EditUsersView view = new EditUsersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteUsersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelUsers vm = new ViewModelDelUsers(SelectedRow);
            DelUsersView view = new DelUsersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            return (from usr in dB.Users
                    from emp in dB.Employees

                    where usr.Employee_ID == emp.Employee_ID

                    select new CommonClass
                    {
                        Employee_ID = emp.Employee_ID,
                        User_ID = usr.User_ID,

                        Full_Name_Employee = emp.Full_Name_Employee,
                        Personnel_Number = emp.Personnel_Number,
                        Login = usr.Login
                    }).OrderBy(x => x.User_ID).ToList();
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.User_ID, "User_ID");
            var res3 = Cntns(res2, FindRule.Login, "Login");
            var res4 = Cntns(res3, FindRule.Personnel_Number, "Personnel_Number");
            var res5 = Cntns(res4, FindRule.Full_Name_Employee, "Full_Name_Employee");
            return res5;
        }
    }
}
