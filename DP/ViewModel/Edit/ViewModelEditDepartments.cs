using DP.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DP.ViewModel.Edit
{
    class ViewModelEditDepartments : ViewModelBase
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

        public ViewModelEditDepartments(CommonClass cc) :base()
        {
            SelectedDepartment = cc;
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
                Departments Dep = (from dep in DB.Departments where dep.Department_ID == SelectedDepartment.Department_ID select dep).FirstOrDefault();
                Dep.Name_Department = SelectedDepartment.Name_Department;
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
