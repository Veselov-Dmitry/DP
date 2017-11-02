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
    class ViewModelComputers : ViewModelBase
    {
        //Computers
        public ICommand AddComputersCommand { get; set; }
        public ICommand DeleteComputersCommand { get; set; }
        public ICommand EditComputersCommand { get; set; }

        public ViewModelComputers() : base()
        {
            AddComputersCommand = new RelayCommand<object>(arg => AddComputersCommandExecute());
            DeleteComputersCommand = new RelayCommand<object>(arg => DeleteComputersCommandExecute());
            EditComputersCommand = new RelayCommand<object>(arg => EditComputersCommandExecute());
        }
        private void AddComputersCommandExecute()
        {
            ViewModelAddComputers vm = new ViewModelAddComputers();
            AddComputersView view = new AddComputersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditComputersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditComputers vm = new ViewModelEditComputers(SelectedRow);
            EditComputersView view = new EditComputersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteComputersCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelComputers vm = new ViewModelDelComputers(SelectedRow);
            DelComputersView view = new DelComputersView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var co = (from comp in dB.Computers

                      select new CommonClass
                      {
                          Computer_ID = comp.Computer_ID,

                          Net_Name = comp.Net_Name,
                          Inventory_number = comp.Inventory_number
                      }).ToList();
            return co;
        }

        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);
            var res2 = (FindRule.Computer_ID == 0) ? res1 : res1.Where(x => x.Computer_ID == FindRule.Computer_ID).ToList();
            var res3 = (string.IsNullOrEmpty(FindRule.Net_Name)) ? res2 : res2.Where(x => x.Net_Name.ToLower().Contains(FindRule.Net_Name.ToLower())).ToList();
            var res4 = (FindRule.Inventory_number == 0) ? res3 : res2.Where(x => x.Inventory_number == FindRule.Inventory_number).ToList();
            return res4;
        }
    }
}
