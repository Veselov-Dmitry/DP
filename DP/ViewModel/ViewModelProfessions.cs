using DP.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using DP.ViewModel.Edit;
using DP.ViewModel.Add;
using DP.ViewModel.Del;
using DP.View.Add;
using DP.View.Edit;
using DP.View.Del;
using DP.View;

namespace DP.ViewModel
{
    public class ViewModelProfessions : ViewModelBase
    {
        //СВОЙСТВА
        public ICommand AddProfessionsCommand { get; set; }
        public ICommand DelProfessionsCommand { get; set; }
        public ICommand EditProfessionsCommand { get; set; }

        //КОНСТРУКТОР
        public ViewModelProfessions() : base()
        {
            AddProfessionsCommand = new RelayCommand<object>(arg => AddProfessionsCommandExecute());
            DelProfessionsCommand = new RelayCommand<object>(arg => DeleteProfessionsCommandExecute());
            EditProfessionsCommand = new RelayCommand<object>(arg => EditProfessionsCommandExecute());
        }

        //МЕТОДЫ
        private void AddProfessionsCommandExecute()
        {
            ViewModelAddProfessions vm = new Add.ViewModelAddProfessions();
            AddProfessionsView view = new AddProfessionsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        private void EditProfessionsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед редактированием выберите строку");
                return;
            }
            ViewModelEditProfessions vm = new ViewModelEditProfessions(SelectedRow);
            EditProfessionsView view = new EditProfessionsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        private void DeleteProfessionsCommandExecute()
        {
            if (SelectedRow == null)
            {
                System.Windows.Forms.MessageBox.Show("Перед удалением выберите строку");
                return;
            }
            ViewModelDelProfessions vm = new ViewModelDelProfessions(SelectedRow);
            DelProfessionsView view = new DelProfessionsView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }
        public override List<CommonClass> GetDB4View(OASUEntity dB)
        {
            var of = from prof in dB.Professions
                     select new CommonClass
                     {
                         Profession_ID = prof.Profession_ID,
                         Name_Profession = prof.Name_Profession
                     };
            return of.ToList();
        }
        public override List<CommonClass> GetFilterResult()
        {
            var res1 = GetDB4View(DB);

            var res2 = Cntns(res1, FindRule.Profession_ID, "Profession_ID");
            var res3 = Cntns(res2, FindRule.Name_Profession, "Name_Profession");
            return res3;
        }
    }
}
