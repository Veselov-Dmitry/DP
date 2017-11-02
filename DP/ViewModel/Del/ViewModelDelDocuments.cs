using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System;
using System.Reflection;
using Microsoft.Win32;
using System.IO;
using DP.Model;

namespace DP.ViewModel.Del
{
    class ViewModelDelDocuments : ViewModelBase
    {// DocumentsTypes
        //ПОЛЯ
        private string _pS_Name;
        private string _name_document_type;
        private int _document_ID;
        private string _document_Number;
        private Nullable<DateTime> _document_Date;
        private string _documentFilePath;
        private string _length;
        private string empty = "пусто";
        private string full = "загружен";


        //СВОЙСТВА
        public string PS_Name
        {
            get { return _pS_Name; }
            set { _pS_Name = value; OnPropertyChanged("PS_Name"); }
        }
        public string Name_document_type
        {
            get { return _name_document_type; }
            set { _name_document_type = value; OnPropertyChanged("Name_document_type"); }
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
        public ViewModelDelDocuments(CommonClass cc) : base()
        {
            DocumentFilePath = (cc.DocumentFile == true) ? full : empty;
            Document_Number = cc.Document_Number;
            Document_ID = cc.Document_ID;

            PS_Name = cc.PS_Name;
            Name_document_type = cc.Name_document_type;
            byte[] arr = (from doc in DB.Documents where doc.Document_ID == cc.Document_ID select doc.DocumentFile).FirstOrDefault();
            if (arr != null)
            {
                long length = arr.Length;
                   Length = Length = Math.Round(((length * 1.0) / 1048576), 3).ToString() + " МВ  ОК";
            }
            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());

            Document_Date = (cc.Document_Date == null) ? default(DateTime) : cc.Document_Date;
        }

        //МЕТОДЫ
        private void DelCommandExecute()
        {
            if(System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Documents Doc = (from doc in DB.Documents where doc.Document_ID == Document_ID select doc).FirstOrDefault();
                DB.Documents.Remove(Doc);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }
    }
}
