using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddDocumentsTypes : ViewModelBase
    {
        private string _name_document_type;
        
        public string Name_document_type
        {
            get { return _name_document_type; }
            set
            {
                _name_document_type = value;
                OnPropertyChanged("_name_document_type");
            }
        }

        public ViewModelAddDocumentsTypes() : base()
        {
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(Name_document_type))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название типа документа");
                return;
            }
            try
            {
                DB.DocumentTypes.Add(new DocumentTypes{ Name_document_type = Name_document_type });
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

