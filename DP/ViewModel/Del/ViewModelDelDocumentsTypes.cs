using DP.Model;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelDocumentsTypes : ViewModelBase
    {
        private string _name_document_type;
        private Nullable<int> _document_type_ID;
        
        public string Name_document_type
        {
            get { return _name_document_type; }
            set
            {
                _name_document_type = value;
                OnPropertyChanged("_name_document_type");
            }
        }
        public Nullable<int> Document_type_ID
        {
            get { return _document_type_ID; }
            set
            {
                _document_type_ID = value;
                OnPropertyChanged("_document_type_ID");
            }
        }


        public ViewModelDelDocumentsTypes(CommonClass cc) : base()
        {
            Name_document_type = cc.Name_document_type;
            Document_type_ID = cc.Document_type_ID;
            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();

            try
            {
                DocumentTypes Dti = (from doct in DB.DocumentTypes where doct.Document_type_ID == Document_type_ID select doct).FirstOrDefault();
                DB.DocumentTypes.Remove(Dti);
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
