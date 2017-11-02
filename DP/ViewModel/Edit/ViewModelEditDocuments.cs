using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using DP.Model;

namespace DP.ViewModel.Edit
{
    class ViewModelEditDocuments : ViewModelBase
    {// DocumentsTypes
        //ПОЛЯ
        private int _selectedProgram;
        private int _selectedDocumentType;
        private int _document_ID;
        private string _document_Number;
        private Nullable<DateTime> _document_Date;
        private string _documentFilePath;
        private string _length;
        private string empty = "пусто";
        private string full = "загружен";
        

        //СВОЙСТВА
        public ICommand ClearCommand { get; set; }
        public ICommand BrowseCommand { get; set; }


        public ObservableCollection<CommonClass> Programs { get; set; }
        public ObservableCollection<CommonClass> DocumentTypes { get; set; }

        public int SelectedProgram
        {
            get { return _selectedProgram; }
            set { _selectedProgram = value; OnPropertyChanged("_selectedProgram"); }
        }
        public int SelectedDocumentType
        {
            get { return _selectedDocumentType; }
            set { _selectedDocumentType = value; OnPropertyChanged("_selectedProgram"); }
        }
        public int Document_ID
        {
            get { return _document_ID; }
            set { _document_ID = value; OnPropertyChanged("Document_ID"); }
        }
        public string Document_Number
        {
            get { return _document_Number; }
            set { _document_Number = value; OnPropertyChanged("Document_Number"); }
        }
        public Nullable<DateTime> Document_Date
        {
            get { return _document_Date; }
            set { _document_Date = value; OnPropertyChanged("Document_Date"); }
        }
        public string DocumentFilePath
        {
            get { return _documentFilePath; }
            set { _documentFilePath = value; OnPropertyChanged("DocumentFilePath"); }
        }
        public string Length
        {
            get { return _length; }
            set { _length = value; OnPropertyChanged("Length"); }
        }


        //КОНСТРУКТОР
        public ViewModelEditDocuments(CommonClass cc) : base()
        {            
            DocumentFilePath = (cc.DocumentFile == true)? full : empty;
            Document_Number = cc.Document_Number;
            Document_ID = cc.Document_ID;

            Programs = new ObservableCollection<CommonClass>(GetPrograms4View());
            DocumentTypes = new ObservableCollection<CommonClass>(GetDocumentTypes4View());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
            BrowseCommand = new RelayCommand<object>(arg => BrowseCommandExecute());
            ClearCommand = new RelayCommand<object>(arg => ClearCommandExecute());

            byte[] arr = (from doc in DB.Documents where doc.Document_ID == cc.Document_ID select doc.DocumentFile).FirstOrDefault();
            if (arr != null)
            {
                long length = arr.Length;
                Length = Length = Math.Round(((length * 1.0) / 1048576), 3).ToString() + " МВ  ОК";
            }

            Document_Date = (cc.Document_Date == null)?DateTime.Now:cc.Document_Date;
            SelectedProgram = Programs.IndexOf(Programs.Where(x => x.PS_ID == cc.PS_ID).FirstOrDefault());
            SelectedDocumentType = DocumentTypes.IndexOf(DocumentTypes.Where(x => x.Document_type_ID == cc.Document_type_ID).FirstOrDefault());
        }


        //МЕТОДЫ
        private void SaveCommandExecute()
        {

            if (SelectedProgram == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите программу");
                return;
            }
            else if (SelectedDocumentType == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите тип документа");
                return;
            }
            try
            {
                Documents Doc = (from doc in DB.Documents where doc.Document_ID == Document_ID select doc).FirstOrDefault();

                if (DocumentFilePath == empty)
                {
                    Doc.DocumentFile = null;
                    Doc.File_Name = "";
                } else if (DocumentFilePath != full)
                {
                    Doc.DocumentFile = File.ReadAllBytes(DocumentFilePath);
                    Doc.File_Name = Path.GetFileName(DocumentFilePath);
                }
                Doc.Document_Number = Document_Number;
                Doc.Document_Date = (Document_Date == DateTime.MinValue) ? default(DateTime) : Document_Date;
                Doc.Document_type_ID = DocumentTypes[SelectedDocumentType].Document_type_ID;
                Doc.PS_ID = Programs[SelectedProgram].PS_ID;
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private void ClearCommandExecute()
        {
            DocumentFilePath = empty;
            Length = "";
        }

        private void BrowseCommandExecute()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                DocumentFilePath = openFileDialog.FileName;
                long length = new System.IO.FileInfo(DocumentFilePath).Length;
                if (length > 20971520 && length < 0)
                {
                    System.Windows.Forms.MessageBox.Show("Допускается загружать файлы до 20МБ\n\nТекущий файл " + (length / 1048576).ToString() + " МБ", "Отказ", 0, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    DocumentFilePath = "пусто";
                    Length = "";
                }
                else
                {
                    Length = Math.Round(((length * 1.0) / 1048576), 3).ToString() + " МВ  ОК";
                }
                return;
            }
        }

        private List<CommonClass> GetDocumentTypes4View()
        {
            var DT = (from doct in DB.DocumentTypes
                      select new
                      CommonClass
                      {
                          Document_type_ID = doct.Document_type_ID,
                          Name_document_type = doct.Name_document_type
                      }
                      ).ToList();
            return DT;
        }

        private List<CommonClass> GetPrograms4View()
        {
            var PS = (from ps in DB.PrgSystems select new CommonClass { PS_ID = ps.PS_ID, PS_Name = ps.PS_Name }).ToList();
            return PS;
        }
    }
}
