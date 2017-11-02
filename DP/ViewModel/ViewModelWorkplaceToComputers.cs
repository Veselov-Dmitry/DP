using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;

namespace DP.ViewModel
{
    class ViewModelWorkplaceToComputers : ViewModelBase
    {
        private ObservableCollection<CommonClass> _workplaceToComputers;
        private ObservableCollection<CommonClass> _sourceComobox;
        private CommonClass _selectedWTC;
        private CommonClass _selectedComobox;
        private Visibility _visible;

        public ICommand AddWrkToCompCommand { get; set; }
        public ICommand DelWrkToCompCommand { get; set; }
        public ICommand AddOKCommand { get; set; }        
        public ICommand AddCloseCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<CommonClass> WorkplaceToComputers
        {
            get { return _workplaceToComputers; }
            set { _workplaceToComputers = value; OnPropertyChanged("WorkplaceToComputers"); }
        }
        public ObservableCollection<CommonClass> SourceComobox
        {
            get { return _sourceComobox; }
            set { _sourceComobox = value; OnPropertyChanged("SourceComobox"); }
        }
        public CommonClass SelectedWTC
        {
            get { return _selectedWTC; }
            set { _selectedWTC = value; OnPropertyChanged("SelectedWTC"); }
        }
        public CommonClass SelectedComobox
        {
            get { return _selectedComobox; }
            set { _selectedComobox = value; OnPropertyChanged("SelectedComobox"); }
        }
        public Visibility Visible
        {
            get { return _visible; }
            set { _visible = value; OnPropertyChanged("Visible"); }
        }
        public int Workplace_ID { get; set; }

        public ViewModelWorkplaceToComputers(CommonClass cc)
        {
            Visible = Visibility.Hidden;
            Workplace_ID = cc.Workplace_ID;
            WorkplaceToComputers = new ObservableCollection<CommonClass>(GetWrkCom4DB());
            AddWrkToCompCommand = new RelayCommand<object>(arg => AddWrkToCompCommandExecute());
            DelWrkToCompCommand = new RelayCommand<object>(arg => DelWrkToCompCommandExecute());
            AddOKCommand = new RelayCommand<object>(arg => AddOKCommandExecute());
            AddCloseCommand = new RelayCommand<object>(arg => AddCloseCommandExecute());
            CloseCommand = new RelayCommand<object>(arg => CloseMethod());
        }

        private void AddCloseCommandExecute()
        {
            Visible = Visibility.Hidden;
        }

        private void AddOKCommandExecute()
        {
            if (SelectedComobox.Net_Name == "(пусто)")
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите компьютер");
                return;
            }
            Visible = Visibility.Hidden;
            int c_id = SelectedComobox.Computer_ID;
            DB.INSERT_WrkToComp(Workplace_ID, SelectedComobox.Computer_ID);
            DB.SaveChanges();
            RefreshDGWTC();
        }

        private void DelWrkToCompCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            if (SelectedWTC == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            DB.DELETE_WrkToComp(SelectedWTC.Workplace_ID, SelectedWTC.Computer_ID);
            DB.SaveChanges();
            RefreshDGWTC();
            return;
        }

        private void AddWrkToCompCommandExecute()
        {
            Visible = Visibility.Visible;
            SourceComobox = new ObservableCollection<CommonClass>(GetSourceComobox4DB());
            SourceComobox.Insert(0, new CommonClass { Net_Name = "(пусто)" });

        }

        private List<CommonClass> GetSourceComobox4DB()
        {
            return (from com in DB.Computers
                    select new CommonClass
                    {
                        Computer_ID = com.Computer_ID,
                        Net_Name = com.Net_Name,
                        Inventory_number = com.Inventory_number
                    }).ToList();
        }

        private List<CommonClass> GetWrkCom4DB()
        {

            List<SELECT_WrkToComp_Result> wtc = DB.SELECT_WrkToComp(Workplace_ID, default(int?)).ToList();
            List<CommonClass> cc = new List<CommonClass>();
            wtc.ForEach(x => cc.Add(
                new CommonClass
                {
                    Net_Name = x.Net_Name,
                    Workplace_ID = (x.Workplace_ID != null) ? x.Workplace_ID.Value : 0,
                    Computer_ID = (x.Computer_ID != null) ? x.Computer_ID.Value : 0,
                    Inventory_number = x.Inventory_number,
                    Telephone = x.Telephone,
                    Housing = x.Housing,
                    Floor = x.Floor,
                    Employee_ID = (x.Employee_ID != null) ? x.Employee_ID.Value : 0,
                    Office_ID = (x.Office_ID != null) ? x.Office_ID.Value : 0
                }));
            return cc;
        }

        private void RefreshDGWTC()
        {
            WorkplaceToComputers.Clear();
            var contractcollection = new ObservableCollection<CommonClass>(GetWrkCom4DB());
            foreach (var item in contractcollection)
                WorkplaceToComputers.Add(item);
        }
    }
}
