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
    class ViewModelPrgSystems : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddPrgSystemsCommand { get; set; }
        public ICommand DeletePrgSystemsCommand { get; set; }
        public ICommand EditPrgSystemsCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelPrgSystems() : base()
        {
            AddPrgSystemsCommand = new RelayCommand<object>(arg => AddPrgSystemsCommandExecute());
            DeletePrgSystemsCommand = new RelayCommand<object>(arg => DeletePrgSystemsCommandExecute());
            EditPrgSystemsCommand = new RelayCommand<object>(arg => EditPrgSystemsCommandExecute());
        }

        //МЕТОДЫ
        private void AddPrgSystemsCommandExecute()
        {
            ViewModelAddPrgSystems vm = new Add.ViewModelAddPrgSystems();
            AddPrgSystemsView view = new AddPrgSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditPrgSystemsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditPrgSystems vm = new ViewModelEditPrgSystems(SelectedRow);
            EditPrgSystemsView view = new EditPrgSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeletePrgSystemsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelPrgSystems vm = new ViewModelDelPrgSystems(SelectedRow);
            DelPrgSystemsView view = new DelPrgSystemsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var Ps = (from ps in dB.PrgSystems
                      from prg in dB.PrgLangs

                      where (ps.Prg_Lang_ID == null) | (ps.Prg_Lang_ID == prg.Prg_Lang_ID)

                      select new CommonClass
                      {
                          PS_Name = ps.PS_Name,
                          PS_ID = ps.PS_ID,
                          Prg_Lang_ID = (ps.Prg_Lang_ID == null) ? 0 : prg.Prg_Lang_ID,
                          Prg_Lang = (ps.Prg_Lang_ID == null) ? "" : prg.Prg_Lang
                      }).Distinct().ToList();
            return Ps;
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.PS_ID, "PS_ID");
            var res3 = Cntns(res2, FindRule.PS_Name, "PS_Name");
            var res4 = Cntns(res3, FindRule.Prg_Lang, "Prg_Lang");
            return res4;
        }
    }
}
