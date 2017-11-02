using DP.Model;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Documents;
using System.Data;
using System.Windows.Controls.Primitives;
using DP.Print;
using DP.PDF;
using Ninject.Parameters;
using DP.Services;

namespace DP.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //ПОЛЯ
        private OASUEntity _db;
        private ObservableCollection<CommonClass> _db0;
        private CommonClass _selectedRow;
        private Visibility _visibleFind;
        private Visibility _visibleFindMinimize;
        private string _findMinimizeMark;
        private CommonClass _findRule;
        //private IKernel ninjectKernel;


        protected BackgroundWorker bwSave;


        //СВОЙСТВА
        public ICommand ExitCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DelCommand { get; set; }
        public ICommand FindCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand ExitFilterCommand { get; set; }
        public ICommand MinimizeFilterCommand { get; set; }
        public ICommand PrintCommand { get; set; }
        public ICommand PDFCommand { get; set; }
        public ICommand InportDBFCommand { get; set; }
        public ICommand RefreshDGCommand { get; set; }
        public ICommand ImportDBCommand { get; set; }
        public ICommand ExportDBCommand { get; set; }
        public ICommand ExportCommand { get; set; }
        

        public Visibility VisibleFind
        {
            get { return _visibleFind; }
            set { _visibleFind = value; OnPropertyChanged("VisibleFind"); }
        }
        public Visibility VisibleFindMinimize
        {
            get { return _visibleFindMinimize; }
            set { _visibleFindMinimize = value; OnPropertyChanged("VisibleFindMinimize"); }
        }
        public string FindMinimizeMark
        {
            get { return _findMinimizeMark; }
            set { _findMinimizeMark = value; OnPropertyChanged("FindMinimizeMark"); }
        }
        public CommonClass FindRule
        {
            get { return _findRule; }
            set { _findRule = value; OnPropertyChanged("FindRule"); }
        }

        public OASUEntity DB
        {
            get { return _db; }
            set { _db = value; OnPropertyChanged("DB"); }
        }        
        public ObservableCollection<CommonClass> DB0
        {
            get { return _db0; }
            set { _db0 = value; OnPropertyChanged("DB0"); }
        }        
        public CommonClass SelectedRow
        {
            get { return _selectedRow; }
            set { _selectedRow = value; OnPropertyChanged("_selectedRow"); }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler RequestClose;



        //КОНСТРУКТОР
        public ViewModelBase()            
        {
            bwSave = new BackgroundWorker();
            FindRule = new CommonClass();
            VisibleFind = Visibility.Hidden;
            VisibleFindMinimize = Visibility.Hidden;
            FindMinimizeMark = "_";
            var kernel = NinjectCommon.CreateKernel(SingleConnection.ConString);
            var serv = new NinjectDependencyResolver(kernel);
            DB = serv.GetService(typeof(OASUEntity)) as OASUEntity;/**/
            DB0 = new ObservableCollection<CommonClass>(GetDB4View(DB));

            ExitCommand = new RelayCommand<object>(arg => CloseMethod());
            FindCommand = new RelayCommand<object>(arg => FindCommandMethod());
            FilterCommand = new RelayCommand<object>(arg => FilterCommandMethod());
            MinimizeFilterCommand = new RelayCommand<object>(arg => MinimizeFilterCommandMethod());



            RefreshDGCommand = new RelayCommand<object>(arg => RefreshDG());
            PrintCommand = new RelayCommand<Visual>(arg => PrintCommandMethod(arg));
            PDFCommand = new RelayCommand<Visual>(arg => PDFCommandMethod(arg));
            ExitFilterCommand = new RelayCommand<object>(arg => ExitFilterCommandMethod()); 
                
        }

        protected void PDFCommandMethod(Visual viewModel)
        {
                PDFClass pdf = new PDFClass();
                if (pdf.ShowDialog("A Flow Document") == true)
                {
                    pdf.SaveDocument(viewModel as DataGrid);
                }
        }

        protected void PrintCommandMethod(Visual viewModel)
        {
                PrintDialog prinDlg = new PrintDialog();
                if (prinDlg.ShowDialog() == true)
                {
                    //создание таблицы для печати
                    FlowDocument fd = new FlowDocument();
                    Table tb = new Table();
                    fd.Blocks.Add(tb);
                    var rowGroup = new TableRowGroup();
                    tb.RowGroups.Add(rowGroup);
                    var header = new TableRow();
                    rowGroup.Rows.Add(header);

                    //заполняем заголовки
                    foreach (DataGridColumn op in (viewModel as DataGrid).Columns)
                    {
                        var tableColumn = new TableColumn();
                        tb.Columns.Add(tableColumn);
                        var cell = new TableCell(new Paragraph(new Run(op.Header.ToString())));
                        header.Cells.Add(cell);
                    }
                    //наполнение таблицы для печати
                    for (int i = 0; i < (viewModel as DataGrid).Items.Count; i++)
                    {

                        var tableRow = new TableRow();
                        rowGroup.Rows.Add(tableRow);

                        var cc = (viewModel as DataGrid).Items[i] as CommonClass;
                        foreach (DataGridColumn op in (viewModel as DataGrid).Columns)
                        {
                            int col = op.DisplayIndex;
                            string path = (viewModel as DataGrid).Columns[col].SortMemberPath;
                            string value = cc.NameOf(path).ToString();

                            var cell = new TableCell(new Paragraph(new Run(value)));
                            tableRow.Cells.Add(cell);
                        }
                        //нарисовать клеточки
                        for (int ii = 0; ii < tableRow.Cells.Count; ii++)
                        {
                            tableRow.Cells[ii].BorderThickness = new Thickness(0, 1, 1, 0);
                            tableRow.Cells[ii].BorderBrush = Brushes.Gray;
                        }
                    }



                    // Привести страницу FlowDocument в соответствие с печатной страницей
                    fd.PageHeight = prinDlg.PrintableAreaHeight;
                    fd.PageWidth = prinDlg.PrintableAreaWidth;
                    fd.PagePadding = new Thickness(15);
                    // Использовать n столбцов
                    int n = 1;
                    fd.ColumnGap = 25;
                    fd.ColumnWidth = (fd.PageWidth - fd.ColumnGap
                        - fd.PagePadding.Left - fd.PagePadding.Right) / n;

                    fd.PagePadding = new Thickness(96 * 0.25, 96 * 0.75, 96 * 0.25, 96 * 0.25);

                    HeaderedFlowDocumentPaginator paginator =
                        new HeaderedFlowDocumentPaginator(fd);

                    prinDlg.PrintDocument(paginator, "A Flow Document");

                }
        }


        //минимизирует или раскрывает окно поиска
        private void MinimizeFilterCommandMethod()
        {
            if (VisibleFindMinimize == Visibility.Collapsed)
            {
                VisibleFindMinimize = Visibility.Visible;
                FindMinimizeMark = "_";
            }
            else
            {
                VisibleFindMinimize = Visibility.Collapsed;
                FindMinimizeMark = (Convert.ToChar(0x039B)).ToString();
            }
        }
        //реализация поиска
        private void FilterCommandMethod()
        {
            DB0 = new ObservableCollection<CommonClass>(GetFilterResult()); 
        }
        //вызов окна поиска
        public void FindCommandMethod()
        {
            VisibleFind = Visibility.Visible;
            VisibleFindMinimize = Visibility.Visible;
        }

        public virtual List<CommonClass> GetFilterResult()
        {
            return new List<CommonClass>();
        }
        //закрывает окно поиска
        private void ExitFilterCommandMethod()
        {
            VisibleFind = Visibility.Hidden;
            DB0 = new ObservableCollection<CommonClass>(GetDB4View(DB));
        }
        
        //МЕТОДЫ
        public virtual void OnPropertyChanged(string propertyName)
        {
            //дебаггер зря ругается, без проверки- Null reff. exept.
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        //базовый метод для страниц вью получает данные для вью
        public virtual List<CommonClass> GetDB4View(OASUEntity dB)
        {
            return new List<CommonClass>();
        }
        //обновление данных на странице
        protected void RefreshDG()
        {
            DB0.Clear();
            var kernel = NinjectCommon.CreateKernel(SingleConnection.ConString);
            var serv = new NinjectDependencyResolver(kernel);
            DB = serv.GetService(typeof(OASUEntity)) as OASUEntity;
            var contractcollection = GetDB4View(DB);
            foreach (var item in contractcollection)
                DB0.Add(item);
        }
        //закрытие окна
        protected void CloseMethod()
        {
            try
            {
                RequestClose(this, new EventArgs());
            }
            catch { Environment.Exit(0); }
        }
        protected List<CommonClass> Cntns(List<CommonClass> listSourse, string filter, string nameParametr)
        {
            return (string.IsNullOrEmpty(filter)) ? listSourse : listSourse.Where(x => x.NameOf(nameParametr).ToString().ToLower().Contains(filter.ToLower())).ToList();
        }
        protected List<CommonClass> Cntns(List<CommonClass> listSourse, DateTime? filter, string nameParametr)
        {
            return (filter == default(DateTime?)) ? listSourse : listSourse.Where(x => ((DateTime)x.NameOf(nameParametr)).Date == ((DateTime)filter).Date ).ToList();
        }
        protected List<CommonClass> Cntns(List<CommonClass> listSourse, int? filter, string nameParametr)
        {
            return (filter == 0) ? listSourse : listSourse.Where(x => x.NameOf(nameParametr).ToString().Contains(filter.ToString())).ToList();
        }
    }
}