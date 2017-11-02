using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.Model;
using System.Reflection;

namespace DP.ViewModel.Del
{
    class ViewModelDelProfessions : ViewModelBase
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

        public ViewModelDelProfessions(CommonClass cc) : base()
        {
            Name_Profession = cc.Name_Profession;
            Profession_ID = cc.Profession_ID;
            DelCommand = new RelayCommand<object>(arg => DelCommandExecute());
        }

        private void DelCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            try
            {
                Professions Pr = (from prof in DB.Professions where prof.Profession_ID == Profession_ID select prof).FirstOrDefault();
                DB.Professions.Remove(Pr);
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
