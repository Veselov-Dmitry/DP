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
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Reflection;

namespace DP.ViewModel
{
    public class ViewModelDepartments : ViewModelBase
    {
        public ICommand AddDepartmentsCommand { get; set; }
        public ICommand DeleteDepartmentsCommand { get; set; }
        public ICommand EditDepartmentsCommand { get; set; }

        public ViewModelDepartments() : base()
        {
            AddDepartmentsCommand = new RelayCommand<object>(arg => AddDepartmentsCommandExecute());
            DeleteDepartmentsCommand = new RelayCommand<object>(arg => DeleteDepartmentsCommandExecute());
            EditDepartmentsCommand = new RelayCommand<object>(arg => EditDepartmentsCommandExecute());
        }

        private void AddDepartmentsCommandExecute()
        {
            ViewModelAddDepartments vm = new Add.ViewModelAddDepartments();
            AddDepartmentsView view = new AddDepartmentsView();
            ViewRequest.RequestDC(vm,view);
            RefreshDG();
        }

        private void EditDepartmentsCommandExecute()
        {

            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Выберите строку");
                return;
            }
            ViewModelEditDepartments vm = new ViewModelEditDepartments(SelectedRow);
            EditDepartmentsView view = new EditDepartmentsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteDepartmentsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Выберите строку");
                return;
            }
            ViewModelDelDepartments vm = new ViewModelDelDepartments(SelectedRow);
            DelDepartmentsView view = new DelDepartmentsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            try
            {
                var Dep = (from dep in DB.Departments
                           select new CommonClass
                           {
                               Department_ID = dep.Department_ID,
                               Name_Department = dep.Name_Department
                           }).ToList();
                return Dep;
            }catch(Exception e)
            {
                string mess = e.Message;
                string satck = "\nStackTrace:\n" + e.StackTrace;
                string inner = (e.InnerException != null) ? "\n Inner Exceptiion:\n" + e.InnerException.Message : "";
                System.Windows.Forms.MessageBox.Show(
                    "В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" +
                     mess + satck + inner, 
                    "",
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Exclamation, 
                    System.Windows.Forms.MessageBoxDefaultButton.Button1, 
                    System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);

                CloseMethod();
                return null; 
            }
        }

        public override List<CommonClass> GetFilterResult()
        {
            var result = (from dep in DB.Departments
                          where (String.IsNullOrEmpty(FindRule.Name_Department)) ? true : dep.Name_Department.Contains(FindRule.Name_Department)
                          && (FindRule.Department_ID == 0) ? true : dep.Department_ID == FindRule.Department_ID
                          select new CommonClass
                          {
                              Department_ID = dep.Department_ID,
                              Name_Department = dep.Name_Department
                          }).ToList();
            return result;
        }

    }
}
