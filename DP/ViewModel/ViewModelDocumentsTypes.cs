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
    class ViewModelDocumentsTypes : ViewModelBase
    {        // DocumentsTypes
        public ICommand AddDocumentsTypesCommand { get; set; }
        public ICommand DelDocumentsTypesCommand { get; set; }
        public ICommand EditDocumentsTypesCommand { get; set; }

        public ViewModelDocumentsTypes() : base()
        {
            AddDocumentsTypesCommand = new RelayCommand<object>(arg => AddDocumentsTypesCommandExecute());
            DelDocumentsTypesCommand = new RelayCommand<object>(arg => DeleteDocumentsTypesCommandExecute());
            EditDocumentsTypesCommand = new RelayCommand<object>(arg => EditDocumentsTypesCommandExecute());
        }

        private void AddDocumentsTypesCommandExecute()
        {
            ViewModelAddDocumentsTypes vm = new Add.ViewModelAddDocumentsTypes();
            AddDocumentsTypesView view = new AddDocumentsTypesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditDocumentsTypesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditDocumentsTypes vm = new ViewModelEditDocumentsTypes(SelectedRow);
            EditDocumentsTypesView view = new EditDocumentsTypesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteDocumentsTypesCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelDocumentsTypes vm = new ViewModelDelDocumentsTypes(SelectedRow);
            DelDocumentsTypesView view = new DelDocumentsTypesView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var DocT = (from doct in dB.DocumentTypes
                    select new CommonClass
                    {
                        Document_type_ID = doct.Document_type_ID,
                        Name_document_type = doct.Name_document_type
                    }).ToList();
            return DocT;
        }


        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Document_type_ID, "Document_type_ID");
            var res3 = Cntns(res2, FindRule.Name_document_type, "Name_document_type");
            return res3;
        }
    }
}
