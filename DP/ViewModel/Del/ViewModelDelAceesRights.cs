using DP.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Windows.Input;

namespace DP.ViewModel.Del
{
    class ViewModelDelAceesRights : ViewModelBase
    {
        public ViewModelDelAceesRights(CommonClass cc) : base()
        {
            SelectedRow = cc;

            DelCommand = new RelayCommand<object>(arg => DeleteCommandExecute());
        }

        private void DeleteCommandExecute()
        {
            if(System.Windows.Forms.DialogResult.No == System.Windows.Forms.MessageBox.Show("Вы уверены, что хотите удалить?","Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation))
                CloseMethod();
            else
            try
            {
                AccesRights accr = (from acc in DB.AccesRights
                                    where acc.AccesRightID == SelectedRow.AccesRightID
                                    select acc).First();
                DB.AccesRights.Remove(accr);
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
