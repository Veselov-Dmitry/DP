using DP.Model;
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DP.ViewModel.Edit
{
    class ViewModelEditComputers : ViewModelBase
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

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="cc"></param>
        public ViewModelEditComputers(CommonClass cc) : base()
        {
            SelectedComputer = cc;
            _inventory_number_static = cc.Inventory_number;
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (SelectedComputer.Inventory_number == 0)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите Инв. №");
                return;
            }
            else if (String.IsNullOrWhiteSpace(SelectedComputer.Net_Name))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите cетевое имя");
                return;
            }
            try
            {
                Computers comp = (from cm in DB.Computers where cm.Inventory_number == _inventory_number_static select cm).FirstOrDefault();

                comp.Net_Name = SelectedComputer.Net_Name;
                comp.Inventory_number = SelectedComputer.Inventory_number;
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
