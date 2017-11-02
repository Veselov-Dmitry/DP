using DP.Model;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System;
using System.Reflection;

namespace DP.ViewModel.Add
{
    class ViewModelAddAceesRights : ViewModelBase
    {
        private ObservableCollection<CommonClass> _programs;
        private CommonClass _selectedProgram;
        private ObservableCollection<CommonClass> _subSystems;
        private CommonClass _selectedSubSystem;

        public ICommand SelectPSCommand { get; set; }

        public ObservableCollection<CommonClass> Programs
        {
            get { return _programs; }
            set { _programs = value; OnPropertyChanged("Programs"); }
        }
        public CommonClass SelectedProgram
        {
            get { return _selectedProgram; }
            set { _selectedProgram = value; OnPropertyChanged("_selectedProgram"); }
        }

        public ObservableCollection<CommonClass> SubSystems
        {
            get { return _subSystems; }
            set { _subSystems = value; OnPropertyChanged("SubSystems"); }
        }
        public CommonClass SelectedSubSystem
        {
            get { return _selectedSubSystem; }
            set { _selectedSubSystem = value; OnPropertyChanged("_selectedSubSystem"); }
        }


        public ViewModelAddAceesRights() : base()
        {
            Programs = new ObservableCollection<CommonClass>(GetPrograms4View());
            SubSystems = new ObservableCollection<CommonClass>(GetSubSystems4View());

            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
            SelectPSCommand = new RelayCommand<object>(arg => SelectPSCommandExecute());
        }

        private void SelectPSCommandExecute()
        {
            // q1 все используемые в SelectedProgram.PS_ID роли
            var q1 = (from acc in DB.AccesRights
                      where acc.PS_ID == SelectedProgram.PS_ID
                      select acc.Sub_System_ID).ToList();

            var Ssys = (from ssys in DB.SubSystems
                        where !q1.Contains(ssys.Sub_System_ID)
                        select new CommonClass
                        {
                            Sub_System_ID = ssys.Sub_System_ID,
                            Sub_System = ssys.Sub_System
                        }).ToList();
            SubSystems = new ObservableCollection<CommonClass>(Ssys);
        }

        private void SaveCommandExecute()
        {

            if (SelectedProgram == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите программу");
                return;
            }
            else if (SelectedSubSystem == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением выберите роль");
                return;
            }
            try
            {
                AccesRights accr = new AccesRights();
                accr.PS_ID = SelectedProgram.PS_ID.GetValueOrDefault(); 
                accr.Sub_System_ID = SelectedSubSystem.Sub_System_ID;
                DB.AccesRights.Add(accr);
                DB.SaveChanges();
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show("В " + MethodInfo.GetCurrentMethod().Name + " произошла ошибка: \n" + e.Message, "", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation, System.Windows.Forms.MessageBoxDefaultButton.Button1, System.Windows.Forms.MessageBoxOptions.DefaultDesktopOnly);
            }
            CloseMethod();
        }

        private List<CommonClass> GetPrograms4View()
        {
            var Ps = (from ps in DB.PrgSystems
                      select new CommonClass
                      {
                          PS_ID = ps.PS_ID,
                          PS_Name = ps.PS_Name
                      }).ToList();
            return Ps;
        }
        private List<CommonClass> GetSubSystems4View()
        {
            var Ssys = (from ssys in DB.SubSystems

                      select new CommonClass
                      {
                          Sub_System_ID = ssys.Sub_System_ID,
                          Sub_System = ssys.Sub_System
                      }).ToList();
            return Ssys;
        }

    }
}
