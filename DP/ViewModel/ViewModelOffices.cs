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

namespace DP.ViewModel
{
    public class ViewModelOffices : ViewModelBase
    {// Offices
        //СВОЙСТВА
        public ICommand AddOfficesCommand { get; set; }
        public ICommand DeleteOfficesCommand { get; set; }
        public ICommand EditOfficesCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelOffices() : base()
        {
            AddOfficesCommand = new RelayCommand<object>(arg => AddOfficesCommandExecute());
            DeleteOfficesCommand = new RelayCommand<object>(arg => DeleteOfficesCommandExecute());
            EditOfficesCommand = new RelayCommand<object>(arg => EditOfficesCommandExecute());
        }

        //МЕТОДЫ
        private void AddOfficesCommandExecute()
        {
            ViewModelAddOffices vm = new Add.ViewModelAddOffices();
            AddOfficesView view = new AddOfficesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditOfficesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditOffices vm = new ViewModelEditOffices(SelectedRow);
            EditOfficesView view = new EditOfficesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteOfficesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelOffices vm = new ViewModelDelOffices(SelectedRow);
            DelOfficesView view = new DelOfficesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {

            var of = from off in dB.Offices
                     from dep in dB.Departments

                     where off.Department_ID == dep.Department_ID

                     select new CommonClass
                     {
                         Office_ID = off.Office_ID,
                         Department_ID = dep.Department_ID,
                         Name_Office = off.Name_Office,
                         Name_Department = dep.Name_Department
                     };

            return of.ToList();
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Office_ID, "Office_ID");
            var res3 = Cntns(res2, FindRule.Name_Office, "Name_Office");
            var res4 = Cntns(res3, FindRule.Name_Department, "Name_Department");
            return res4;
        }
    }
}
