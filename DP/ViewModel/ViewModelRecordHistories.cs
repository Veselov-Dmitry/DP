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
    class ViewModelRecordHistories : ViewModelBase
    {// RecordHistories
        //СВОЙСТВА
        public ICommand AddRecordHistoriesCommand { get; set; }
        public ICommand DeleteRecordHistoriesCommand { get; set; }
        public ICommand EditRecordHistoriesCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelRecordHistories() : base()
        {
            AddRecordHistoriesCommand = new RelayCommand<object>(arg => AddRecordHistoriesCommandExecute());
            DeleteRecordHistoriesCommand = new RelayCommand<object>(arg => DeleteRecordHistoriesCommandExecute());
            EditRecordHistoriesCommand = new RelayCommand<object>(arg => EditRecordHistoriesCommandExecute());
        }

        //МЕТОДЫ
        private void AddRecordHistoriesCommandExecute()
        {
            ViewModelAddRecordHistories vm = new Add.ViewModelAddRecordHistories();
            AddRecordHistoriesView view = new AddRecordHistoriesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditRecordHistoriesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditRecordHistories vm = new ViewModelEditRecordHistories(SelectedRow);
            EditRecordHistoriesView view = new EditRecordHistoriesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteRecordHistoriesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelRecordHistories vm = new ViewModelDelRecordHistories(SelectedRow);
            DelRecordHistoriesView view = new DelRecordHistoriesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var Rh = (from rec in dB.RecordHistories
                    from ps in dB.PrgSystems

                    where rec.PS_ID == ps.PS_ID

                    select new CommonClass
                    {
                        Record_ID = rec.Record_ID,
                        PS_ID = rec.PS_ID,

                        Note = rec.Note,
                        PS_Name = ps.PS_Name
                    }).OrderBy(x => x.Record_ID).ToList();
            return Rh;
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Record_ID, "Record_ID");
            var res3 = Cntns(res2, FindRule.Note, "Note");
            var res4 = Cntns(res3, FindRule.PS_Name, "PS_Name");
            return res4;
        }
    }
}
