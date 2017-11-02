using DP.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DP.ViewModel.Edit;
using DP.ViewModel.Add;
using DP.ViewModel.Del;
using DP.View;
using DP.View.Add;
using DP.View.Edit;
using DP.View.Del;
using System;

namespace DP.ViewModel
{
    public class ViewModelAccesRights : ViewModelBase
    {
        public ICommand AddAccesRightsCommand {get;set;}
        public ICommand DeleteAccesRightsCommand { get; set; }
        public ICommand EditAccesRightsCommand { get; set; }

        public ViewModelAccesRights() :base()
        {
            AddAccesRightsCommand = new RelayCommand<object>(arg => AddAccesRightsCommandExecute());
            DeleteAccesRightsCommand = new RelayCommand<object>(arg => DeleteAccesRightsCommandExecute());
            EditAccesRightsCommand = new RelayCommand<object>(arg => EditAccesRightsCommandExecute());
        }
        
        private void AddAccesRightsCommandExecute()
        {
            ViewModelAddAceesRights vm = new Add.ViewModelAddAceesRights();
            AddAceesRightsView view = new AddAceesRightsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void EditAccesRightsCommandExecute()
        {
            if(SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditAceesRights vm = new ViewModelEditAceesRights(SelectedRow);
            EditAceesRightsView view = new EditAceesRightsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        private void DeleteAccesRightsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelAceesRights vm = new ViewModelDelAceesRights(SelectedRow);
            DelAceesRightsView view = new DelAceesRightsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var Acc = (from acc in dB.AccesRights
                    from subs in dB.SubSystems
                    from psys in dB.PrgSystems

                    where subs.Sub_System_ID == acc.Sub_System_ID
                    && acc.PS_ID == psys.PS_ID
                    select new CommonClass
                    {
                        AccesRightID = acc.AccesRightID,
                        Sub_System_ID = subs.Sub_System_ID,
                        PS_ID = psys.PS_ID,

                        PS_Name = psys.PS_Name,
                        Sub_System = subs.Sub_System
                    }).ToList();
            return Acc;
        }

        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = (FindRule.AccesRightID == 0) ? res1 : res1.Where(x => x.AccesRightID == FindRule.AccesRightID).ToList();
            var res3 = (String.IsNullOrEmpty(FindRule.PS_Name)) ? res2 : res2.Where(x => x.PS_Name.ToLower().Contains(FindRule.PS_Name.ToLower())).ToList();
            var res4 = (String.IsNullOrEmpty(FindRule.Sub_System)) ? res3 : res3.Where(x => x.Sub_System.ToLower().Contains(FindRule.Sub_System.ToLower())).ToList();
           
            return res4;
        }

    }
}
