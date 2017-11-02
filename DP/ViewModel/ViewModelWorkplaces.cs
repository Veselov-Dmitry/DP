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
    class ViewModelWorkplaces : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddWorkplacesCommand { get; set; }
        public ICommand DeleteWorkplacesCommand { get; set; }
        public ICommand EditWorkplacesCommand { get; set; }
        public ICommand OpenComputersCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelWorkplaces() : base()
        {
            AddWorkplacesCommand = new RelayCommand<object>(arg => AddWorkplacesCommandExecute());
            DeleteWorkplacesCommand = new RelayCommand<object>(arg => DeleteWorkplacesCommandExecute());
            EditWorkplacesCommand = new RelayCommand<object>(arg => EditWorkplacesCommandExecute());
            OpenComputersCommand = new RelayCommand<object>(arg => OpenComputersCommandExecute());
        }

        //МЕТОДЫ
        private void OpenComputersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед просмотром выберите строку");
                return;
            }
            ViewModelWorkplaceToComputers vm = new ViewModelWorkplaceToComputers(SelectedRow);
            WorkplaceToComputersView view = new WorkplaceToComputersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void AddWorkplacesCommandExecute()
        {
            ViewModelAddWorkplaces vm = new Add.ViewModelAddWorkplaces();
            AddWorkplacesView view = new AddWorkplacesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditWorkplacesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditWorkplaces vm = new ViewModelEditWorkplaces(SelectedRow);
            EditWorkplacesView view = new EditWorkplacesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteWorkplacesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelWorkplaces vm = new ViewModelDelWorkplaces(SelectedRow);
            DelWorkplacesView view = new DelWorkplacesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            List<Workplaces> WrkAll = (from wrk in dB.Workplaces select wrk).ToList();
            List<Employees> EmpAll = (from emp in dB.Employees select emp).ToList();
            List<Offices> OffAll = (from off in dB.Offices select off).ToList();

            List<CommonClass> Wrk = new List<CommonClass>();

            foreach(Workplaces wrk in WrkAll)
            {
                Employees emp = null;
                Offices off = null;

                if (wrk.Employees != null) emp = EmpAll.Where(x => x.Employee_ID == wrk.Employee_ID).FirstOrDefault();
                if(wrk.Offices != null) off = OffAll.Where( x => x.Office_ID == wrk.Office_ID).FirstOrDefault();
                Wrk.Add
                    (
                    new CommonClass
                    {

                        Workplace_ID = wrk.Workplace_ID,
                        Employee_ID = (wrk.Employee_ID == null) ? 0 : emp.Employee_ID,
                        Office_ID = (wrk.Office_ID == null) ? 0 : off.Office_ID,

                        Net_Name = ("нет"),
                        Personnel_Number = (wrk.Employee_ID == null) ? 0 : emp.Personnel_Number,
                        Name_Office = (wrk.Office_ID == null) ? "нет" : off.Name_Office,

                        Floor = wrk.Floor,
                        Housing = wrk.Housing,
                        Telephone = wrk.Telephone
                    }
                    );
            }
            return Wrk;
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Workplace_ID, "Workplace_ID");
            var res3 = Cntns(res2, FindRule.Personnel_Number, "Personnel_Number");
            var res4 = Cntns(res3, FindRule.Name_Office, "Name_Office");
            var res5 = Cntns(res4, FindRule.Floor, "Floor");
            var res6 = Cntns(res5, FindRule.Housing, "Housing");
            var res7 = Cntns(res6, FindRule.Telephone, "Telephone");
            return res7;
        }
    }
}
