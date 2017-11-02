using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddDepartments : ViewModelBase
    {
        private CommonClass _selectedDepartment;

        public CommonClass SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged("_selectedDepartment");
            }
        }

        public ViewModelAddDepartments() : base()
        {
            SelectedDepartment = new CommonClass();
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(SelectedDepartment.Name_Department))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название отдела");
                return;
            }
            try
            {
                Departments Dep = new Departments();
                Dep.Name_Department = SelectedDepartment.Name_Department;
                DB.Departments.Add(Dep);
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
