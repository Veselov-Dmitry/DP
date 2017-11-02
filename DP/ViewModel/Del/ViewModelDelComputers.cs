using DP.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DP.ViewModel.Del
{
    class ViewModelDelComputers : ViewModelBase
    {
        private CommonClass _selectedComputer;
        private static int _inventory_number_static;

        public CommonClass SelectedComputer
        {
            get { return _selectedComputer; }
            set
            {
                _selectedComputer = value;
                OnPropertyChanged("_selectedComputer");
            }
        }

        public ViewModelDelComputers(CommonClass cc) :base()
        {
            SelectedComputer = cc;
            _inventory_number_static = cc.Inventory_number;
            DelCommand = new RelayCommand<object>(arg => DeleteCommandExecute());
        }

        private void DeleteCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            else
                try
                {
                Computers comp = (from cm in DB.Computers where cm.Inventory_number == _inventory_number_static select cm).FirstOrDefault();

                DB.Computers.Remove(comp);
                }
                catch (Exception e)
                {
                    System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
                }
            CloseMethod();
        }
    }
}
