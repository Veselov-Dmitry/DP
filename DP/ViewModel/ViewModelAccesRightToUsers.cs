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
    class ViewModelAccesRightToUsers : ViewModelBase
    {
        private ObservableCollection<CommonClass> _accesRightToUsers;
        private ObservableCollection<CommonClass> _sourceComobox;
        private CommonClass _selectedARTU;
        private CommonClass _selectedComobox;
        private Visibility _visible;

        public ICommand AddWrkToCompCommand { get; set; }
        public ICommand DelWrkToCompCommand { get; set; }
        public ICommand AddOKCommand { get; set; }
        public ICommand AddCloseCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<CommonClass> AccesRightToUsers
        {
            get { return _accesRightToUsers; }
            set { _accesRightToUsers = value; OnPropertyChanged("AccesRightToUsers"); }
        }
        public ObservableCollection<CommonClass> SourceComobox
        {
            get { return _sourceComobox; }
            set { _sourceComobox = value; OnPropertyChanged("SourceComobox"); }
        }
        public CommonClass SelectedARTU
        {
            get { return _selectedARTU; }
            set { _selectedARTU = value; OnPropertyChanged("SelectedARTU"); }
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
        public int User_ID { get; set; }

        public ViewModelAccesRightToUsers(CommonClass cc)
        {
            Visible = Visibility.Hidden;
            User_ID = cc.User_ID;
            AccesRightToUsers = new ObservableCollection<CommonClass>(GetARTU4DB());
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

            DB.INSERT_ARTU(User_ID, SelectedComobox.AccesRightID);
            DB.SaveChanges();
            RefreshDGWTC();
        }

        private void DelWrkToCompCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            if (SelectedARTU == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            DB.DELETE_ARTU(default(int?), default(int?), SelectedARTU.AccesRightID, default(int?));
            DB.SaveChanges();
            RefreshDGWTC();
            return;
        }

        private void AddWrkToCompCommandExecute()
        {
            Visible = Visibility.Visible;
            SourceComobox = new ObservableCollection<CommonClass>(GetSourceComobox4DB());

        }

        private List<CommonClass> GetSourceComobox4DB()
        {
            return (from acr in DB.AccesRights
                    from ps in DB.PrgSystems
                    from ss in DB.SubSystems

                    where acr.Sub_System_ID == ss.Sub_System_ID
                    && acr.PS_ID == ps.PS_ID
                    

                    select new CommonClass
                    {
                        AccesRightID = acr.AccesRightID,
                        PS_Name = ps.PS_Name,
                        Sub_System = ss.Sub_System
                    }).Distinct().ToList();
        }

        private List<CommonClass> GetARTU4DB()
        {

            List<SELECT_ARTU_Result> artu = DB.SELECT_ARTU(User_ID, default(int?), default(int?), default(int?)).ToList();
            List<CommonClass> cc = new List<CommonClass>();
            artu.ForEach(x => cc.Add(
                new CommonClass
                {
                    Personnel_Number = x.Personnel_Number,
                    User_ID = (x.User_ID != null) ? x.User_ID.Value : 0,
                    Sub_System = x.Sub_System,
                    AccesRightID = x.AccesRightID,
                    Sub_System_ID = (x.Sub_System_ID != null) ? x.Sub_System_ID.Value : 0,
                    PS_ID = x.PS_ID,
                    Login = x.Login,
                    PS_Name = x.PS_Name
                }));
            return cc;
        }

        private void RefreshDGWTC()
        {
            AccesRightToUsers.Clear();
            var contractcollection = new ObservableCollection<CommonClass>(GetARTU4DB());
            foreach (var item in contractcollection)
                AccesRightToUsers.Add(item);
        }
    }
}
