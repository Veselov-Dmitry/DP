using DP.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DP.View.Autorisation;
using DP.ViewModel.Autorisation;
using System.Threading;

namespace DP.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //if (!Autorisation()) Environment.Exit(0);
            App.SceenOn();
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            SourceInitialized += MainWindow_SourceInitialized;
        }

        private void MainWindow_SourceInitialized(object sender, EventArgs e)
        {
            Visibility = Visibility.Visible;
            App.ScreenOff();
        }

        private bool Autorisation()
        {
            ViewModelAutorisation vm = new ViewModelAutorisation();
            ViewRequest.RequestDC(vm, new AutorisationView());
            return vm.DResult;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Navigation.NavigationClass.Service = MainFrame.NavigationService;

            DataContext = new MainViewModel(new ViewModelsResolver());

        }
    }
}
