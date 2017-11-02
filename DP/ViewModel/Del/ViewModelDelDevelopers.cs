using DP.Model;
using System.Windows.Input;
using System;
using System.Reflection;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace DP.ViewModel.Del
{
    class ViewModelDelDevelopers : ViewModelBase
    {

        public Nullable<System.DateTime> Date_start { get; set; }
        public string PS_Name { get; set; }
        public string Note { get; set; }
        public int Developer_part_ID { get; set; }
        public int Record_ID { get; set; }
        public int Personnel_Number { get; set; }



        public ViewModelDelDevelopers(CommonClass cc) : base()
        {
            Date_start = cc.Date_start;
            Note = cc.Note;
            PS_Name = cc.PS_Name;
            Developer_part_ID = cc.Developer_part_ID;
            Record_ID = cc.Record_ID;
            Personnel_Number = cc.Personnel_Number;

            DelCommand = new RelayCommand<object>(arg => DeleteCommandExecute());
        }
        
        private void DeleteCommandExecute()
        {
            if (System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            
            try
            {
                Developers Dev = (from dev in DB.Developers where dev.Developer_part_ID == Developer_part_ID select dev).FirstOrDefault();
                DB.Developers.Remove(Dev);
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
