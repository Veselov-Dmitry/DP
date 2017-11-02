using System;
using System.Windows.Input;
using DP.ViewModel.Interfaces;
using System.ComponentModel;
using DP.Navigation;
using System.Windows;
using System.IO;
using System.Threading;
using System.Windows.Media;
using System.Windows.Controls;
using DP.Print;
using System.Windows.Documents;
using DP.Model;
using DP.ViewModel.ImportDBF;
using DP.View.ImportDBF;
using DP.View;
using DP.View.ImportExportDB;
using DP.ViewModel.ImportExportDB;
using DP.XML;
using DP.Model.XML;

namespace DP.ViewModel
{
    public class MainViewModel : ViewModelBase
    {

        #region Constants

        public static readonly string ViewModelDepartmentsAlias = "DepartmentsVM";
        public static readonly string ViewModelOfficesAlias = "OfficesVM";
        public static readonly string ViewModelProfessionsAlias = "ProfessionsVM";
        public static readonly string ViewModelEmployeesAlias = "EmployeesVM";
        public static readonly string ViewModelComputersAlias = "ComputersVM";
        public static readonly string ViewModelDevelopersAlias = "DevelopersVM";
        public static readonly string ViewModelDocumentsTypesAlias = "DocumentsTypesVM";
        public static readonly string ViewModelDocumentsAlias = "DocumentsVM";
        public static readonly string ViewModelPrgSystemsAlias = "PrgSystemsVM";
        public static readonly string ViewModelPrgLangsAlias = "PrgLangsVM";
        public static readonly string ViewModelRecordHistoriesAlias = "RecordHistoriesVM";
        public static readonly string ViewModelSubSystemsAlias = "SubSystemsVM";
        public static readonly string ViewModelUsersAlias = "UsersVM";
        public static readonly string ViewModelWorkplacesAlias = "WorkplacesVM";
        public static readonly string ViewModelAccesRightsAlias = "AccesRightsVM";
        

        public static readonly string NotFoundPageViewModelAlias = "404VM";

        #endregion

        #region Fields

        /// <summary>
        /// содержит словарь - ключи это кностаты ViewModelsResolver, а значениея инстансы VM для вьюшек
        /// </summary>
        private readonly IViewModelsResolver _resolver;

        private ICommand _goToDepartmentsCommand;
        private ICommand _goToOfficesCommand;
        private ICommand _goToProfessionsCommand;
        private ICommand _goToEmployeesCommand;
        private ICommand _goToComputersCommand;
        private ICommand _goToDevelopersCommand;
        private ICommand _goToDocumentsTypesCommand;
        private ICommand _goToDocumentsCommand;
        private ICommand _goToPrgSystemsCommand;
        private ICommand _goToPrgLangsCommand;
        private ICommand _goToRecordHistoriesCommand;
        private ICommand _goToSubSystemsCommand;
        private ICommand _goToUsersCommand;
        private ICommand _goToWorkplacesCommand;
        private ICommand _goToAccesRightsCommand;
        

        private readonly INotifyPropertyChanged _departmentsViewModel;
        private readonly INotifyPropertyChanged _officesViewModel;
        private readonly INotifyPropertyChanged _professionsViewModel;
        private readonly INotifyPropertyChanged _employeesViewModel;
        private readonly INotifyPropertyChanged _computersViewModel;
        private readonly INotifyPropertyChanged _developersViewModel;
        private readonly INotifyPropertyChanged _documentsTypesViewModel;
        private readonly INotifyPropertyChanged _documentsViewModel;
        private readonly INotifyPropertyChanged _prgSystemsViewModel;
        private readonly INotifyPropertyChanged _prgLangsViewModel;
        private readonly INotifyPropertyChanged _recordHistoriesViewModel;
        private readonly INotifyPropertyChanged _subSystemsViewModel;
        private readonly INotifyPropertyChanged _usersViewModel;
        private readonly INotifyPropertyChanged _workplacesViewModel;
        private readonly INotifyPropertyChanged _accesRightsViewModel;
        
        #endregion

        #region Properties

        #region Commands
        public ICommand GoToDepartmentsCommand
		{
			get { return _goToDepartmentsCommand; }
			set
			{
				_goToDepartmentsCommand = value;
				OnPropertyChanged("GoToDepartmentsCommand");
			}
		}
		public ICommand GoToOfficesCommand
		{
			get
			{
				return _goToOfficesCommand;
			}
			set
			{
				_goToOfficesCommand = value;
				OnPropertyChanged("GoToOfficesCommand");
			}
		}
		public ICommand GoToProfessionsCommand
		{
			get { return _goToProfessionsCommand; }
			set
			{
				_goToProfessionsCommand = value;
				OnPropertyChanged("GoToProfessionsCommand");
			}
		}
		public ICommand GoToEmployeesCommand
        {
            get { return _goToEmployeesCommand; }
            set
            {
                _goToEmployeesCommand = value;
                OnPropertyChanged("GoToEmployeesCommand");
            }
        }
		public ICommand GoToComputersCommand
        {
            get { return _goToComputersCommand; }
            set
            {
                _goToComputersCommand = value;
                OnPropertyChanged("GoToComputersCommand");
            }
        }
		public ICommand GoToDevelopersCommand
        {
            get { return _goToDevelopersCommand; }
            set
            {
                _goToDevelopersCommand = value;
                OnPropertyChanged("GoToDevelopersCommand");
            }
        }
		public ICommand GoToDocumentsTypesCommand
        {
            get { return _goToDocumentsTypesCommand; }
            set
            {
                _goToDocumentsTypesCommand = value;
                OnPropertyChanged("GoToDocumentsTypesCommand");
            }
        }
		public ICommand GoToDocumentsCommand
        {
            get { return _goToDocumentsCommand; }
            set
            {
                _goToDocumentsCommand = value;
                OnPropertyChanged("GoToDocumentsCommand");
            }
        }
		public ICommand GoToPrgSystemsCommand
        {
            get { return _goToPrgSystemsCommand; }
            set
            {
                _goToPrgSystemsCommand = value;
                OnPropertyChanged("GoToPrgSystemsCommand");
            }
        }
		public ICommand GoToPrgLangsCommand
        {
            get { return _goToPrgLangsCommand; }
            set
            {
                _goToPrgLangsCommand = value;
                OnPropertyChanged("GoToPrgLangsCommand");
            }
        }
		public ICommand GoToRecordHistoriesCommand
        {
            get { return _goToRecordHistoriesCommand; }
            set
            {
                _goToRecordHistoriesCommand = value;
                OnPropertyChanged("GoToRecordHistoriesCommand");
            }
        }
		public ICommand GoToSubSystemsCommand
        {
            get { return _goToSubSystemsCommand; }
            set
            {
                _goToSubSystemsCommand = value;
                OnPropertyChanged("GoToSubSystemsCommand");
            }
        }
		public ICommand GoToUsersCommand
        {
            get { return _goToUsersCommand;}
            set
            {
                _goToUsersCommand = value;
                OnPropertyChanged("GoToUsersCommand");
            }
        }
        public ICommand GoToWorkplacesCommand
        {
            get { return _goToWorkplacesCommand; }
            set
            {
                _goToWorkplacesCommand = value;
                OnPropertyChanged("GoToWorkplacesCommand");
            }
        }
        public ICommand GoToAccesRightsCommand
        {
            get { return _goToAccesRightsCommand; }
            set
            {
                _goToAccesRightsCommand = value;
                OnPropertyChanged("GoToAccesRightsCommand");
            }
        }
        //public ICommand FindCommand { get; set; }
        //дебагер говорит что не используется но оно сприязано в вьюшке
        #endregion

        #region PropertyChanged
        /// <summary>
        /// получив инстанс Departments следит за его изменением
        /// </summary>
        public INotifyPropertyChanged ViewModelDepartments
        { get { return _departmentsViewModel; } }
        public INotifyPropertyChanged ViewModelOffices
        { get { return _officesViewModel; } }
        public INotifyPropertyChanged ViewModelProfessions
        {
            get { return _professionsViewModel; }
        }
        public INotifyPropertyChanged ViewModelEmployees
        {
            get { return _employeesViewModel; }
        }
        public INotifyPropertyChanged ViewModelComputers
        {
            get { return _computersViewModel; }
        }
        public INotifyPropertyChanged ViewModelDevelopers
        {
            get { return _developersViewModel; }
        }
        public INotifyPropertyChanged ViewModelDocumentsTypes
        {
            get { return _documentsTypesViewModel; }
        }
        public INotifyPropertyChanged ViewModelDocuments
        {
            get { return _documentsViewModel; }
        }
        public INotifyPropertyChanged ViewModelPrgSystems
        {
            get { return _prgSystemsViewModel; }
        }
        public INotifyPropertyChanged ViewModelPrgLangs
        {
            get { return _prgLangsViewModel; }
        }
        public INotifyPropertyChanged ViewModelRecordHistories
        {
            get { return _recordHistoriesViewModel; }
        }
        public INotifyPropertyChanged ViewModelSubSystems
        {
            get { return _subSystemsViewModel; }
        }
        public INotifyPropertyChanged ViewModelUsers
        {
            get { return _usersViewModel;}
        }
        public INotifyPropertyChanged ViewModelWorkplaces
        {
            get { return _workplacesViewModel; }
        }
        public INotifyPropertyChanged ViewModelAccesRights
        {
            get { return _accesRightsViewModel; }
        }

        #endregion

        #endregion
        
        #region Constructors
        /// <summary>
        /// Конструктор, начало
        /// </summary>
        public MainViewModel(IViewModelsResolver resolver)
        {

            _resolver = resolver;

            //привязка инстансов вьюМоделей
            _departmentsViewModel = _resolver.GetViewModelInstance(ViewModelDepartmentsAlias);
            _officesViewModel = _resolver.GetViewModelInstance(ViewModelOfficesAlias);
            _professionsViewModel = _resolver.GetViewModelInstance(ViewModelProfessionsAlias);
            _employeesViewModel = _resolver.GetViewModelInstance(ViewModelEmployeesAlias);
            _computersViewModel = _resolver.GetViewModelInstance(ViewModelComputersAlias);
            _developersViewModel = _resolver.GetViewModelInstance(ViewModelDevelopersAlias);
            _documentsTypesViewModel = _resolver.GetViewModelInstance(ViewModelDocumentsTypesAlias);
            _documentsViewModel = _resolver.GetViewModelInstance(ViewModelDocumentsAlias);
            _prgSystemsViewModel = _resolver.GetViewModelInstance(ViewModelPrgSystemsAlias);
            _prgLangsViewModel = _resolver.GetViewModelInstance(ViewModelPrgLangsAlias);
            _recordHistoriesViewModel = _resolver.GetViewModelInstance(ViewModelRecordHistoriesAlias);
            _subSystemsViewModel = _resolver.GetViewModelInstance(ViewModelSubSystemsAlias);
            _usersViewModel = _resolver.GetViewModelInstance(ViewModelUsersAlias);
            _workplacesViewModel = _resolver.GetViewModelInstance(ViewModelWorkplacesAlias);
            _accesRightsViewModel = _resolver.GetViewModelInstance(ViewModelAccesRightsAlias);
            
            InitializeCommands();            
        }
        #endregion

        #region GoTo command execute
        private void InitializeCommands()
        {
            //привязка команд и методов
            GoToDepartmentsCommand = new RelayCommand<INotifyPropertyChanged>(GoToDepartmentsCommandExecute);
            GoToOfficesCommand = new RelayCommand<INotifyPropertyChanged>(GoToOfficesCommandExecute);
            GoToProfessionsCommand = new RelayCommand<INotifyPropertyChanged>(GoToProfessionsCommandExecute);
            GoToEmployeesCommand = new RelayCommand<INotifyPropertyChanged>(GoToEmployeesCommandExecute);
            GoToComputersCommand = new RelayCommand<INotifyPropertyChanged>(GoToComputersCommandExecute);
            GoToDevelopersCommand = new RelayCommand<INotifyPropertyChanged>(GoToDevelopersCommandExecute);
            GoToDocumentsTypesCommand = new RelayCommand<INotifyPropertyChanged>(GoToDocumentsTypesCommandExecute);
            GoToDocumentsCommand = new RelayCommand<INotifyPropertyChanged>(GoToDocumentsCommandExecute);
            GoToPrgSystemsCommand = new RelayCommand<INotifyPropertyChanged>(GoToPrgSystemsCommandExecute);
            GoToPrgLangsCommand = new RelayCommand<INotifyPropertyChanged>(GoToPrgLangsCommandExecute);
            GoToRecordHistoriesCommand = new RelayCommand<INotifyPropertyChanged>(GoToRecordHistoriesCommandExecute);
            GoToSubSystemsCommand = new RelayCommand<INotifyPropertyChanged>(GoToSubSystemsCommandExecute);
            GoToUsersCommand = new RelayCommand<INotifyPropertyChanged>(GoToUsersCommandExecute);
            GoToWorkplacesCommand = new RelayCommand<INotifyPropertyChanged>(GoToWorkplacesCommandExecute);
            GoToAccesRightsCommand = new RelayCommand<INotifyPropertyChanged>(GoToAccesRightsCommandExecute);

            ImportDBCommand = new RelayCommand<object>(arg => ImportDBCommandMethod());
            ExportDBCommand = new RelayCommand<object>(arg => ExportDBCommandMethod());
            RefreshDGCommand = new RelayCommand<object> (arg => _RefreshDGMethod(arg));
            InportDBFCommand = new RelayCommand<object>(arg => InportDBFCommandMethod(arg));
            PDFCommand = new RelayCommand<object>(arg => _PDFCommandMethod(arg));
            PrintCommand = new RelayCommand<object>(arg => _PrintCommandMethod(arg));
            FindCommand = new RelayCommand<object>(arg => _FindCommandMethod(arg));
            ExitCommand = new RelayCommand<object>(arg => ExitMethod());

            /*TEST*/
            ImportDBCommandMethod();

        }

        private void ExportDBCommandMethod()
        {
            ExportDBView view = new ExportDBView();
            ViewModelExportDB vm = new ViewModelExportDB(
                new XMLFileService(), 
                new DefaultDialogServiceXML(),
                new GeterModel(DB));
            RefreshDG();
            ViewRequest.RequestDC(vm, view);
        }

        private void ImportDBCommandMethod()
        {
            ImportDBView view = new ImportDBView();
            ViewModelImportDB vm = new ViewModelImportDB(
                new XMLFileService(),
                new DefaultDialogServiceXML(),
                new GeterModel(DB));
            RefreshDG();
            ViewRequest.RequestDC(vm, view);
        }

        // извлекает из страницы команду №4 т.е. Обновить таблицу
        private void _RefreshDGMethod(object obj)
        {
            Frame fr = null;
            Page pg = null;
            KeyBinding com = null;
            if ((fr = obj as Frame) == null) return;
            if ((pg = fr.Content as Page) == null) return;
            if (pg.InputBindings.Count == 0) return;
            com = pg.InputBindings[3] as KeyBinding;

            if (com.Command != null)
                com.Command.Execute(0);
        }

        // окно импорта DBF
        private void InportDBFCommandMethod(object arg)
        {
            ImportViewModel vm = new ImportViewModel();
            ImportView view = new ImportView();
            ViewRequest.RequestDC(vm, view);
            RefreshDG();
        }

        // извлекает из страницы DataGrid и вызывает сохранения в PDF 
        private void _PDFCommandMethod(object obj)
        {
            Frame fr = null;
            Page pg = null;
            if ((fr = obj as Frame) == null) return;
            if ((pg = fr.Content as Page) == null) return;
            Grid gr = pg.Content as Grid;
            DataGrid dg = gr.Children[1] as DataGrid;
            PDFCommandMethod(dg);
        }

        // извлекает из страницы команду №1 т.е.  Поск
        private void _FindCommandMethod(object obj)
        {
            Frame fr = null;
            Page pg = null;
            //Grid gr1 = null;
            KeyBinding com = null;
            if ((fr = obj as Frame) == null) return;
            if ((pg = fr.Content as Page) == null) return;
            //if ((gr1 = pg.Content as Grid) == null) return;
            if (pg.InputBindings.Count == 0) return;
            com = pg.InputBindings[0] as KeyBinding;
            
            if (com.Command != null)
            {
                com.Command.Execute(0);
            }/**/
        }

        // извлекает из страницы команду №2 т.е.  Печать 
        private void _PrintCommandMethod(object obj)
        {
            Frame fr = null;
            Page pg = null;
            if ((fr = obj as Frame) == null) return;
            if ((pg = fr.Content as Page) == null) return;            
            Grid gr = pg.Content as Grid;
            DataGrid dg = gr.Children[1] as DataGrid;
            PrintCommandMethod(dg);
        }
        
        #region Navigate Methods

        private void GoToDepartmentsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageDepartmentsAlias, ViewModelDepartments);
        }

        private void GoToOfficesCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageOfficesAlias, ViewModelOffices);
        }

        private void GoToProfessionsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageProfessionsAlias, ViewModelProfessions);
        }

        private void GoToEmployeesCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageEmployeesAlias, ViewModelEmployees);
        }

        private void GoToComputersCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageComputersAlias, ViewModelComputers);
        }

        private void GoToDevelopersCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageDevelopersAlias, ViewModelDevelopers);
        }

        private void GoToDocumentsTypesCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageDocumentsTypesAlias, ViewModelDocumentsTypes);
        }

        private void GoToDocumentsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageDocumentsAlias, ViewModelDocuments);
        }

        private void GoToPrgSystemsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PagePrgSystemsAlias, ViewModelPrgSystems);
        }

        private void GoToPrgLangsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PagePrgLangsAlias, ViewModelPrgLangs);
        }

        private void GoToRecordHistoriesCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageRecordHistoriesAlias, ViewModelRecordHistories);
        }

        private void GoToSubSystemsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageSubSystemsAlias, ViewModelSubSystems);
        }

        private void GoToUsersCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageUsersAlias, ViewModelUsers);
        }

        private void GoToWorkplacesCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageWorkplacesAlias, ViewModelWorkplaces);
        }

        private void GoToAccesRightsCommandExecute(INotifyPropertyChanged viewModel)
        {
            NavigationClass.Navigate(NavigationClass.PageAccesRightsAlias, ViewModelAccesRights);
        }
        #endregion

        #endregion


        /// <summary>
        /// перход по строковой константе, хранятся в NavigationClass
        /// </summary>
        /// <param name="alias"></param>        
        private void GoToPathCommandExecute(string alias)
        {
            if (string.IsNullOrWhiteSpace(alias))
                return;
            NavigationClass.Navigate(alias);
        }

        /// <summary>
        /// Метод выхода в ОС
        /// </summary>
        private void ExitMethod()
        {
            App.SceenOn("/Resources/exit.png");
            App.ScreenOff(100);
            Thread.Sleep(100);
            try
            {
                //System.Windows.Forms.MessageBox.Show("Вы уверены что хотите удалить?", "Подтверждение", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
            catch (ObjectDisposedException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
            }
            finally
            {
                Environment.Exit(0);
            }

        }


    }
}
