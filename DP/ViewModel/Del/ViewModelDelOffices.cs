using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelOffices : ViewModelBase
    {
        private string _name_Department;
        private string _name_Office;
        private int _office_ID;

        public string Name_Department
        {
            get { return _name_Department; }
            set { _name_Department = value; OnPropertyChanged("_name_Department"); }
        }
        public string Name_Office
        {
            get { return _name_Office; }
            set { _name_Office = value; OnPropertyChanged("_name_Office"); }
        }
        public int Office_ID
        {
            get { return _office_ID; }
            set { _office_ID = value; OnPropertyChanged("_office_ID"); }
        }


        public ViewModelDelOffices(CommonClass cc) : base()
        {
            Name_Office = cc.Name_Office;
            Office_ID = cc.Office_ID;
            Name_Department = cc.Name_Department;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Offices Off = (from off in DB.Offices where off.Office_ID == Office_ID select off).FirstOrDefault();

                DB.Offices.Remove(Off);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetDepartments4View()
        {
            var Dep = (from dep in DB.Departments
                       select new CommonClass
                       {
                           Name_Department = dep.Name_Department,
                           Department_ID = dep.Department_ID
                       }).ToList();
            return Dep;
        }
    }
}
