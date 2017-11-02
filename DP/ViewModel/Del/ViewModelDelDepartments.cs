using DP.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DP.ViewModel.Del
{
    class ViewModelDelDepartments : ViewModelBase
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
        
        public ViewModelDelDepartments(CommonClass cc) : base()
        {
            SelectedDepartment = cc;
            DelCommand = new RelayCommand<object>(arg => DeleteCommandExecute());
        }

        private void DeleteCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            else
                try
                {
                    Departments Dep = (from dep in DB.Departments where dep.Department_ID == SelectedDepartment.Department_ID select dep).FirstOrDefault();

                    DB.Departments.Remove(Dep);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                }
            CloseMethod();
        }
    }
}
