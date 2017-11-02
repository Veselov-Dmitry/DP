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

namespace DP.ViewModel
{
    public class ViewModelEmployees : ViewModelBase
    {// Employees
        //ПОЛЯ

        //СВОЙСТВА
        public ICommand AddEmployeesCommand { get; set; }
        public ICommand DeleteEmployeesCommand { get; set; }
        public ICommand EditEmployeesCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelEmployees() : base()
        {
            AddEmployeesCommand = new RelayCommand<object>(arg => AddEmployeesCommandExecute());
            DeleteEmployeesCommand = new RelayCommand<object>(arg => DeleteEmployeesCommandExecute());
            EditEmployeesCommand = new RelayCommand<object>(arg => EditEmployeesCommandExecute());
        }
        

        //МЕТОДЫ
        private void AddEmployeesCommandExecute()
        {
            ViewModelAddEmployees vm = new Add.ViewModelAddEmployees();
            AddEmployeesView view = new AddEmployeesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditEmployeesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditEmployees vm = new ViewModelEditEmployees(SelectedRow);
            EditEmployeesView view = new EditEmployeesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteEmployeesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelEmployees vm = new ViewModelDelEmployees(SelectedRow);
            DelEmployeesView view = new DelEmployeesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            List< Employees> EmpAll = (from emp in DB.Employees select emp).ToList();
            List<Professions> ProfAll = (from prof in DB.Professions select prof).ToList();
            List<Offices> OffAll = (from off in DB.Offices select off).ToList();
            List<Departments> DepAll = (from dep in DB.Departments select dep).ToList();

            List<CommonClass> Emp = new List<CommonClass>();

            foreach(Employees emp in EmpAll)
            {
                Professions prof = null;
                Offices off = null;
                Departments dep = null;

                if (emp.Profession_ID != null) prof = ProfAll.Where(x => x.Profession_ID == emp.Profession_ID).FirstOrDefault();
                if(emp.Office_ID != null) off = OffAll.Where(x => x.Office_ID == emp.Office_ID).FirstOrDefault();
                if((emp.Office_ID != null)&&(off != null)) dep = DepAll.Where(x => x.Department_ID == off.Department_ID).FirstOrDefault();
                Emp.Add(
                    new CommonClass
                    {
                        Employee_ID = emp.Employee_ID,
                        Profession_ID = (emp.Profession_ID == null) ? 0 : (int)emp.Profession_ID,
                        Office_ID = (emp.Office_ID == null) ? 0 : off.Office_ID,

                        Name_Office = (emp.Office_ID == null)?"нет":off.Name_Office,
                        Name_Department = (emp.Office_ID == null ) ? "нет" : dep.Name_Department,

                        Name_Profession = (emp.Profession_ID == null)?"нет":prof.Name_Profession,

                        Full_Name_Employee = emp.Full_Name_Employee,
                        Personnel_Number = emp.Personnel_Number

                    }
                    );
            }
            return Emp;
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Employee_ID, "Employee_ID");
            var res3 = Cntns(res2, FindRule.Personnel_Number, "Personnel_Number");
            var res4 = Cntns(res3, FindRule.Full_Name_Employee, "Full_Name_Employee");
            var res5 = Cntns(res4, FindRule.Name_Profession, "Name_Profession");
            var res6 = Cntns(res5, FindRule.Name_Department, "Name_Department");
            var res7 = Cntns(res6, FindRule.Name_Office, "Name_Office");
            return res7;
        }
    }
}
