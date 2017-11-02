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
using System.Diagnostics;
using System.IO;

namespace DP.ViewModel
{
    class ViewModelDocuments : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddDocumentsCommand { get; set; }
        public ICommand DelDocumentsCommand { get; set; }
        public ICommand EditDocumentsCommand { get; set; }
        public ICommand OpenFileCommand { get; set; }
        
        //КОНСТРУКТОР
        public ViewModelDocuments() : base()
        {
            AddDocumentsCommand = new RelayCommand<object>(arg => AddDocumentsCommandExecute());
            DelDocumentsCommand = new RelayCommand<object>(arg => DeleteDocumentsCommandExecute());
            EditDocumentsCommand = new RelayCommand<object>(arg => EditDocumentsCommandExecute());
            OpenFileCommand = new RelayCommand<object>(arg => OpenFileCommandExecute());
        }
        //МЕТОДЫ
        private void OpenFileCommandExecute()
        {
            if(SelectedRow != null && SelectedRow.DocumentFile == true)
                {
                string path = System.IO.Path.GetTempPath() + SelectedRow.File_Name;
                byte[] file = (from doc in DB.Documents where doc.Document_ID == SelectedRow.Document_ID select doc.DocumentFile).FirstOrDefault();
                System.IO.File.WriteAllBytes(path, file);
                try
                {
                    Process.Start(path);
                }
                catch(Exception exeptionRun)
                {
                    System.Windows.Forms.MessageBox.Show("Ошибка при открытии файла\n" + exeptionRun.Message);
                }
            }else
            {
                System.Windows.Forms.MessageBox.Show("Нет файла или не выбрана строка");
                return;
            }

        }

        private void AddDocumentsCommandExecute()
        {
            ViewModelAddDocuments vm = new Add.ViewModelAddDocuments();
            AddDocumentsView view = new AddDocumentsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditDocumentsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditDocuments vm = new ViewModelEditDocuments(SelectedRow);
            EditDocumentsView view = new EditDocumentsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteDocumentsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelDocuments vm = new ViewModelDelDocuments(SelectedRow);
            DelDocumentsView view = new DelDocumentsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var Doc = (from doc in dB.Documents
                       from psys in dB.PrgSystems
                       from doct in dB.DocumentTypes

                       where doc.Document_type_ID == doct.Document_type_ID
                       && doc.PS_ID == psys.PS_ID

                       select new CommonClass
                       {
                           Document_ID = doc.Document_ID,
                           PS_ID = doc.PS_ID,

                           Document_type_ID = doc.Document_type_ID,

                           Document_Date = doc.Document_Date,
                           Document_Number = doc.Document_Number,
                           PS_Name = psys.PS_Name,
                           Name_document_type = doct.Name_document_type,
                           File_Name = (doc.DocumentFile != null)?doc.File_Name:"",
                           DocumentFile = (doc.DocumentFile != null)

                       }).ToList();
            return Doc;
        }

        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Document_ID, "Document_ID");
            var res3 = Cntns(res2, FindRule.Document_Date, "Document_Date");
            var res4 = Cntns(res3, FindRule.Document_Number, "Document_Number");
            var res5 = Cntns(res4, FindRule.PS_Name, "PS_Name");
            var res6 = Cntns(res5, FindRule.Name_document_type, "Name_document_type");
            var res7 = Cntns(res6, FindRule.File_Name, "File_Name");
            return res7;
        }
    }
}
