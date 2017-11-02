using System;
using System.Windows.Navigation;
using System.Windows.Controls;
using DP.Navigation.Interfaces;

namespace DP.Navigation
{
    public sealed class NavigationClass
    {
        #region Constants
        /// <summary>
        /// константы для переходов и для словаря инстансов
        /// </summary>
        public static readonly string PageDepartmentsAlias = "Departments";
        public static readonly string PageOfficesAlias = "Offices";
        public static readonly string PageProfessionsAlias = "Professions";
        public static readonly string PageEmployeesAlias = "Employees";
        public static readonly string PageComputersAlias = "Computers";
        public static readonly string PageDevelopersAlias = "Developers";
        public static readonly string PageDocumentsTypesAlias = "DocumentsTypes";
        public static readonly string PageDocumentsAlias = "Documents";
        public static readonly string PagePrgSystemsAlias = "PrgSystems";
        public static readonly string PagePrgLangsAlias = "PrgLangs";
        public static readonly string PageRecordHistoriesAlias = "RecordHistories";
        public static readonly string PageSubSystemsAlias = "SubSystems";
        public static readonly string PageUsersAlias = "Users";
        public static readonly string PageWorkplacesAlias = "Workplaces";
        public static readonly string PageAccesRightsAlias = "AccesRights";

        public static readonly string NotFoundPageAlias = "404";

        #endregion


        #region Fields

        private NavigationService _navService;
        /// <summary>
        /// создан в синглтоне и имеет словарь с инстансами вьюшек
        /// </summary>
        private readonly IPageResolver _resolver;

        #endregion


        #region Properties
        /// <summary>
        /// свойство объекта для привязки собственных событий навигации
        /// </summary>
        public static NavigationService Service
        {
            get { return Instance._navService; }
            set
            {
                if (Instance._navService != null)
                {
                    //отписаться от события Navigated - (Процесс навигации начался, но целевая страница еще не извлечена)
                    Instance._navService.Navigated -= Instance._navService_Navigated;
                }

                Instance._navService = value;
                Instance._navService.Navigated += Instance._navService_Navigated;
            }
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// позволяет переходить на страницу по ее URI 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="context"></param>
        public static void Navigate(Page page, object context)
        {
            if (Instance._navService == null || page == null)
            {
                return;
            }
            //отправка в NavigationService
            Instance._navService.Navigate(page, context);
        }

        /// <summary>
        /// заглушка для пустого значения object context
        /// </summary>
        /// <param name="page"></param>
        public static void Navigate(Page page)
        {
            Navigate(page, null);
        }

        /// <summary>
        /// по константе из NavigationClass получает инстанс виюшки и передает в Navigate для прехода 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="context"></param>
        public static void Navigate(string uri, object context)
        {
            if (Instance._navService == null || uri == null)
            {
                return;
            }

            var page = Instance._resolver.GetPageInstance(uri);

            Navigate(page, context);
        }

        /// <summary>
        /// заглушка для пустого значения object context
        /// </summary>
        /// <param name="page"></param>
        public static void Navigate(string uri)
        {
            Navigate(uri, null);
        }

        #endregion


        #region Private Methods
        /// <summary>
        /// записывает дополнительные параметры
        /// </summary>
        /// <param name="sender">приводится к типу Page</param>
        /// <param name="e">извлекается ExtraData</param>
        void _navService_Navigated(object sender, NavigationEventArgs e)
        {
            //ExtraData - в качестве параметра принимает дополнительный объект
            var page = e.Content as Page;

            if (page == null)
            {
                return;
            }

            page.DataContext = e.ExtraData;//Возвращает объект, необязательные пользовательские данные
        }

        #endregion


        #region Singleton
        /// <summary>
        /// инстанс тепущего класса по синглтону
        /// </summary>
        private static volatile NavigationClass _instance;
        private static readonly object SyncRoot = new Object();

        /// <summary>
        /// конструктор создает словарь с инстансами всех вьюшек страниц
        /// </summary>
        private NavigationClass()
        {
            _resolver = new PagesResolver();
        }

        /// <summary>
        /// свойство инстанса текущего класса
        /// </summary>
        private static NavigationClass Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new NavigationClass();
                    }
                }

                return _instance;
            }
        }
        #endregion
    }
}
