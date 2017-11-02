using System;
using System.Collections.Generic;
using System.Windows.Controls;
using DP.View;
using DP.Navigation.Interfaces;

namespace DP.Navigation
{
    public class PagesResolver : IPageResolver
    {

        private readonly Dictionary<string, Func<Page>> _pagesResolvers = new Dictionary<string, Func<Page>>();
        /// <summary>
        /// конструктор создает словарь с инстансами всех вьюшек для страниц, а ключи это константы NavigationClass
        /// </summary>
        public PagesResolver()
        {
            _pagesResolvers.Add(NavigationClass.PageDepartmentsAlias, () => new DepartmentsView());
            _pagesResolvers.Add(NavigationClass.PageOfficesAlias, () => new OfficesView());
            _pagesResolvers.Add(NavigationClass.PageProfessionsAlias, () => new ProfessionsView());
            _pagesResolvers.Add(NavigationClass.PageEmployeesAlias, () => new EmployeesView());
            _pagesResolvers.Add(NavigationClass.PageComputersAlias, () => new ComputersView());
            _pagesResolvers.Add(NavigationClass.PageDevelopersAlias, () => new DevelopersView());
            _pagesResolvers.Add(NavigationClass.PageDocumentsTypesAlias, () => new DocumentsTypesView());
            _pagesResolvers.Add(NavigationClass.PageDocumentsAlias, () => new DocumentsView());
            _pagesResolvers.Add(NavigationClass.PagePrgSystemsAlias, () => new PrgSystemsView());
            _pagesResolvers.Add(NavigationClass.PagePrgLangsAlias, () => new PrgLangsView());
            _pagesResolvers.Add(NavigationClass.PageRecordHistoriesAlias, () => new RecordHistoriesView());
            _pagesResolvers.Add(NavigationClass.PageSubSystemsAlias, () => new SubSystemsView());
            _pagesResolvers.Add(NavigationClass.PageUsersAlias, () => new UsersView());
            _pagesResolvers.Add(NavigationClass.PageWorkplacesAlias, () => new WorkplacesView());
            _pagesResolvers.Add(NavigationClass.PageAccesRightsAlias, () => new AccesRightsView());
            
        }

        /// <summary>
        /// возвращает по запросу инстанс необходимой вьюшки, ключи храняться в NavigationClass
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        public Page GetPageInstance(string alias)
        {
            if (_pagesResolvers.ContainsKey(alias))
            {
                return _pagesResolvers[alias]();
            }

            return _pagesResolvers[NavigationClass.NotFoundPageAlias]();
        }
    }
}
