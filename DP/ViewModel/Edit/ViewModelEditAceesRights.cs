using DP.Model;
using DP.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;
using Forms = System.Windows.Forms;

namespace DP.ViewModel.Edit
{
    class ViewModelEditAceesRights : ViewModelBase
    {
        private ObservableCollection<CommonClass> _subSystems;
        private int _selectedIndex;

        public ObservableCollection<CommonClass> SubSystems
        {
            get { return _subSystems; }
            set { _subSystems = value; OnPropertyChanged("SubSystems"); }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("_selectedIndex");
            }
        }
        

        public ViewModelEditAceesRights(CommonClass cc) : base()
        {
            SelectedRow = cc;
            SubSystems = new ObservableCollection<CommonClass>(SelectPSCommandExecute());
            SelectedIndex = SubSystems.IndexOf(SubSystems.Where(w => w.Sub_System_ID == cc.Sub_System_ID).FirstOrDefault());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private List<CommonClass> SelectPSCommandExecute()
        {
            // q1 все используемые в SelectedRow.PS_ID роли кроме выбранной
            var q1 = (from acc in DB.AccesRights
                      where acc.PS_ID == SelectedRow.PS_ID
                      && acc.Sub_System_ID != SelectedRow.Sub_System_ID
                      select acc.Sub_System_ID).Distinct().ToList();

            var Ssys = (from ssys in DB.SubSystems
                        where !q1.Contains(ssys.Sub_System_ID)
                        select new CommonClass
                        {
                            Sub_System_ID = ssys.Sub_System_ID,
                            Sub_System = ssys.Sub_System
                        }).ToList();
            return Ssys;
        }

        private void SaveCommandExecute()
        {
            if (SelectedIndex == -1)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите роль");
                return;
            }
            else
            {
                try
                {
                    AccesRights accr = (from acc in DB.AccesRights
                                        where acc.AccesRightID == SelectedRow.AccesRightID
                                        select acc).First();
                    accr.Sub_System_ID = SubSystems[SelectedIndex].Sub_System_ID;
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
}
