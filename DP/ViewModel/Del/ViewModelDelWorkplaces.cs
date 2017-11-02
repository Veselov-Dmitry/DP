using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelWorkplaces : ViewModelBase
    {
        private string _name_Office;
        private int _personnel_Number;
        private int _workplace_ID;
        private string _floor;
        private string _name_Department;
        private string _housing;
        private string _telephone;

        public string Name_Office
        {
            get { return _name_Office; }
            set { _name_Office = value; OnPropertyChanged("_name_Office"); }
        }
        public int Personnel_Number
        {
            get { return _personnel_Number; }
            set { _personnel_Number = value; OnPropertyChanged("_personnel_Number"); }
        }
        public int Workplace_ID
        {
            get { return _workplace_ID; }
            set { _workplace_ID = value; OnPropertyChanged("_workplace_ID"); }
        }
        public string Name_Department
        {
            get { return _name_Department; }
            set { _name_Department = value; OnPropertyChanged("_name_Department"); }
        }
        public string Floor
        {
            get { return _floor; }
            set { _floor = value; OnPropertyChanged("_floor"); }
        }
        public string Housing
        {
            get { return _housing; }
            set { _housing = value; OnPropertyChanged("_housing"); }
        }
        public string Telephone
        {
            get { return _telephone; }
            set { _telephone = value; OnPropertyChanged("_telephone"); }
        }


        public ViewModelDelWorkplaces(CommonClass cc) : base()
        {
            Name_Office = cc.Name_Office;
            Name_Department = cc.Name_Department;
            Personnel_Number = cc.Personnel_Number;
            Workplace_ID = cc.Workplace_ID;
            Floor = cc.Floor;
            Housing = cc.Housing;
            Telephone = cc.Telephone;

            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Workplaces Wrk = (from wrk in DB.Workplaces where wrk.Workplace_ID == Workplace_ID select wrk).FirstOrDefault();
                DB.Workplaces.Remove(Wrk);
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
