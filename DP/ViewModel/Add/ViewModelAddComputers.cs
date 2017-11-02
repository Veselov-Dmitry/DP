using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddComputers : ViewModelBase
    {
        private string _net_Name;
        private int _inventory_number;

        public string Net_Name
        {
            get { return _net_Name; }
            set
            {
                _net_Name = value;
                OnPropertyChanged("_net_Name");
            }
        }
        public int Inventory_number
        {
            get { return _inventory_number; }
            set
            {
                _inventory_number = value;
                OnPropertyChanged("_selectedIndex");
            }
        }

        public ViewModelAddComputers() : base()
        {

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (Inventory_number == 0)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите Инв. №");
                return;
            }
            else if (String.IsNullOrWhiteSpace(Net_Name))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите cетевое имя");
                return;
            }
            try
            {
                Computers comp = new Computers();

                comp.Net_Name = Net_Name;
                comp.Inventory_number = Inventory_number;
                DB.Computers.Add(comp);
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
