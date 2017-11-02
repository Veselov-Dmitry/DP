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
    class ViewModelPrgLangs : ViewModelBase
    {        // PrgLangs
        public ICommand AddPrgLangsCommand { get; set; }
        public ICommand DelPrgLangsCommand { get; set; }
        public ICommand EditPrgLangsCommand { get; set; }

        public ViewModelPrgLangs() : base()
        {
            AddPrgLangsCommand = new RelayCommand<object>(arg => AddPrgLangsCommandExecute());
            DelPrgLangsCommand = new RelayCommand<object>(arg => DeletePrgLangsCommandExecute());
            EditPrgLangsCommand = new RelayCommand<object>(arg => EditPrgLangsCommandExecute());
        }

        private void AddPrgLangsCommandExecute()
        {
            ViewModelAddPrgLangs vm = new Add.ViewModelAddPrgLangs();
            AddPrgLangsView view = new AddPrgLangsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditPrgLangsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditPrgLangs vm = new ViewModelEditPrgLangs(SelectedRow);
            EditPrgLangsView view = new EditPrgLangsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeletePrgLangsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelPrgLangs vm = new ViewModelDelPrgLangs(SelectedRow);
            DelPrgLangsView view = new DelPrgLangsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            return (from plng in dB.PrgLangs
                    select new CommonClass
                    {
                        Prg_Lang = plng.Prg_Lang,
                        Prg_Lang_ID = plng.Prg_Lang_ID
                    }).ToList();
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Prg_Lang_ID, "Prg_Lang_ID");
            var res3 = Cntns(res2, FindRule.Prg_Lang, "Prg_Lang");
            return res3;
        }
    }
}
