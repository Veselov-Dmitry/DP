using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.Model;
using System.Reflection;

namespace DP.ViewModel.Edit
{
    class ViewModelEditProfessions : ViewModelBase
    {
        private string _name_Profession;
        private int _profession_ID;

        public string Name_Profession
        {
            get { return _name_Profession; }
            set
            {
                _name_Profession = value;
                OnPropertyChanged("_name_Profession");
            }
        }
        public int Profession_ID
        {
            get { return _profession_ID; }
            set
            {
                _profession_ID = value;
                OnPropertyChanged("_profession_ID");
            }
        }

        public ViewModelEditProfessions(CommonClass cc) : base()
        {
            Name_Profession = cc.Name_Profession;
            Profession_ID = cc.Profession_ID;
            SaveCommand = new RelayCommand<object>(arg => SaveCommandExecute());
        }

        private void SaveCommandExecute()
        {
            if (String.IsNullOrWhiteSpace(Name_Profession))
            {
                System.Windows.Forms.MessageBox.Show("Перед сохранением введите название профессии");
                return;
            }
            try
            {
                Professions Pr = (from prof in DB.Professions where prof.Profession_ID == Profession_ID select prof).FirstOrDefault();
                Pr.Name_Profession = Name_Profession;
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
