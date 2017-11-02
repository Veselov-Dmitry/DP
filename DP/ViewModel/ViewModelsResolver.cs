using System;
using System.Collections.Generic;
using System.ComponentModel;
using DP.ViewModel.Interfaces;

namespace DP.ViewModel
{
    public class ViewModelsResolver : IViewModelsResolver
    {

        private readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();
        /// <summary>
        /// конструктор создает словарь с инстансами всех страниц, а ключи это константы глайной VM
        /// </summary>
        public ViewModelsResolver()
        {
            _vmResolvers.Add(MainViewModel.ViewModelDepartmentsAlias, () => new ViewModelDepartments());
            _vmResolvers.Add(MainViewModel.ViewModelOfficesAlias, () => new ViewModelOffices());
            _vmResolvers.Add(MainViewModel.ViewModelProfessionsAlias, () => new ViewModelProfessions());
            _vmResolvers.Add(MainViewModel.ViewModelEmployeesAlias, () => new ViewModelEmployees());
            _vmResolvers.Add(MainViewModel.ViewModelComputersAlias, () => new ViewModelComputers());
            _vmResolvers.Add(MainViewModel.ViewModelDevelopersAlias, () => new ViewModelDevelopers());
            _vmResolvers.Add(MainViewModel.ViewModelDocumentsTypesAlias, () => new ViewModelDocumentsTypes());
            _vmResolvers.Add(MainViewModel.ViewModelDocumentsAlias, () => new ViewModelDocuments());
            _vmResolvers.Add(MainViewModel.ViewModelPrgSystemsAlias, () => new ViewModelPrgSystems());
            _vmResolvers.Add(MainViewModel.ViewModelPrgLangsAlias, () => new ViewModelPrgLangs());
            _vmResolvers.Add(MainViewModel.ViewModelRecordHistoriesAlias, () => new ViewModelRecordHistories());
            _vmResolvers.Add(MainViewModel.ViewModelSubSystemsAlias, () => new ViewModelSubSystems());
            _vmResolvers.Add(MainViewModel.ViewModelUsersAlias, () => new ViewModelUsers());
            _vmResolvers.Add(MainViewModel.ViewModelWorkplacesAlias, () => new ViewModelWorkplaces());
            _vmResolvers.Add(MainViewModel.ViewModelAccesRightsAlias, () => new ViewModelAccesRights());
            


            _vmResolvers.Add(MainViewModel.NotFoundPageViewModelAlias, () => new ViewModelPage404());
        }
        /// <summary>
        /// по ключу возвращает инстанс необходимой VM для страницы,  ключи - константы MainViewModel
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
            if (_vmResolvers.ContainsKey(alias))
            {
                return _vmResolvers[alias]();
            }

            return _vmResolvers[MainViewModel.NotFoundPageViewModelAlias]();
        }
    }
}
