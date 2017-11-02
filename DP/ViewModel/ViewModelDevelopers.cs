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
    class ViewModelDevelopers : ViewModelBase
    {
        public ICommand AddDevelopersCommand { get; set; }
        public ICommand DeleteDevelopersCommand { get; set; }
        public ICommand EditDevelopersCommand { get; set; }
        

        public ViewModelDevelopers() : base()
        {
            AddDevelopersCommand = new RelayCommand<object>(arg => AddDevelopersCommandExecute());
            DeleteDevelopersCommand = new RelayCommand<object>(arg => DeleteDevelopersCommandExecute());
            EditDevelopersCommand = new RelayCommand<object>(arg => EditDevelopersCommandExecute());
        }

        private void AddDevelopersCommandExecute()
        {
            ViewModelAddDevelopers vm = new ViewModelAddDevelopers();
            AddDevelopersView view = new AddDevelopersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditDevelopersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditDevelopers vm = new ViewModelEditDevelopers(SelectedRow);
            EditDevelopersView view = new EditDevelopersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteDevelopersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelDevelopers vm = new ViewModelDelDevelopers(SelectedRow);
            DelDevelopersView view = new DelDevelopersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        /// <summary>
        /// создает представление для текущей страницы
        /// </summary>
        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            List<Developers> DevAll = (from dev in dB.Developers select dev).ToList();
            List<Employees> EmpAll = (from emp in DB.Employees select emp).ToList();
            List<Professions> ProfAll = (from prof in DB.Professions select prof).ToList();
            List<RecordHistories> RechAll = (from rech in DB.RecordHistories select rech).ToList();
            List<PrgSystems> PsysAll = (from psys in DB.PrgSystems select psys).ToList();

            List<CommonClass> Dev1 = new List<CommonClass>();

            foreach (Developers dev in DevAll)
            {
                Employees emp = null;
                Professions prof = null;
                PrgSystems psys = null;

                RecordHistories rech = RechAll.Where( x => x.Record_ID == dev.Record_ID).FirstOrDefault();
                if(rech != null) psys = PsysAll.Where(x => x.PS_ID == rech.PS_ID).FirstOrDefault();

                if (dev.Employee_ID != null)
                {
                    emp = EmpAll.Where(x => x.Employee_ID == dev.Employee_ID).FirstOrDefault();
                    if(emp != null && emp.Profession_ID != null)
                        prof = ProfAll.Where(x => x.Profession_ID == emp.Profession_ID).FirstOrDefault();
                    else
                    {
                        prof = new Professions();
                        prof.Name_Profession = "нет";
                    }
                }

                Dev1.Add(
                    new CommonClass
                    {
                        Developer_part_ID = dev.Developer_part_ID,
                        Employee_ID = (dev.Employee_ID == null) ? 0 : emp.Employee_ID,
                        Profession_ID = (dev.Employee_ID == null) ? 0 : prof.Profession_ID,
                        Record_ID = rech.Record_ID,
                        PS_ID = psys.PS_ID,

                        Full_Name_Employee = (dev.Employee_ID == null) ? "нет" : emp.Full_Name_Employee,
                        Personnel_Number = (dev.Employee_ID == null) ? 0 : emp.Personnel_Number,
                        Name_Profession = (dev.Employee_ID == null) ? "нет" : prof.Name_Profession,
                        Date_start = dev.Date_start,
                        PS_Name = psys.PS_Name,
                        Note = rech.Note
                    }
                    );
            }
            return Dev1;
        }

        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Developer_part_ID, "Developer_part_ID"); 
            var res3 = Cntns(res2, FindRule.Personnel_Number, "Personnel_Number");
            var res4 = Cntns(res3, FindRule.Full_Name_Employee, "Full_Name_Employee");
            var res5 = Cntns(res4, FindRule.Name_Profession, "Name_Profession");
            var res6 = Cntns(res5, FindRule.Record_ID, "Record_ID");
            var res7 = Cntns(res6, FindRule.PS_Name, "PS_Name");
            var res8 = Cntns(res7, FindRule.Note, "Note");
            var res9 = Cntns(res8, (FindRule.Date_start as DateTime?), "Date_start");
            return res9;
        }
    }
}
