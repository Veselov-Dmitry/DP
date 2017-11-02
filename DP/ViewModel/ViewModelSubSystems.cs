using DP.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DP.ViewModel.Edit;
using DP.ViewModel.Add;
using DP.ViewModel.Del;
using DP.View.Add;
using DP.View.Edit;
using DP.View.Del;
using DP.View;

namespace DP.ViewModel
{
    class ViewModelSubSystems : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddSubSystemsCommand { get; set; }
        public ICommand DelSubSystemsCommand { get; set; }
        public ICommand EditSubSystemsCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelSubSystems() : base()
        {
            AddSubSystemsCommand = new RelayCommand<object>(arg => AddSubSystemsCommandExecute());
            DelSubSystemsCommand = new RelayCommand<object>(arg => DeleteSubSystemsCommandExecute());
            EditSubSystemsCommand = new RelayCommand<object>(arg => EditSubSystemsCommandExecute());
        }

        //МЕТОДЫ
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Sub_System_ID, "Sub_System_ID");
            var res3 = Cntns(res2, FindRule.Sub_System, "Sub_System");
            return res3;
        }
        private void AddSubSystemsCommandExecute()
        {
            ViewModelAddSubSystems vm = new Add.ViewModelAddSubSystems();
            AddSubSystemsView view = new AddSubSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        private void EditSubSystemsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditSubSystems vm = new ViewModelEditSubSystems(SelectedRow);
            EditSubSystemsView view = new EditSubSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        private void DeleteSubSystemsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelSubSystems vm = new ViewModelDelSubSystems(SelectedRow);
            DelSubSystemsView view = new DelSubSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            return (from ssys in dB.SubSystems
                    select new CommonClass
                    {
                        Sub_System = ssys.Sub_System,
                        Sub_System_ID = ssys.Sub_System_ID
                    }).ToList();
        }
    }
}
