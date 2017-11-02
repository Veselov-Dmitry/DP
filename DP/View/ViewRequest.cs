using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DP.View;
using DP.ViewModel;
using DP.ViewModel.Add;
using DP.View.Add;
using DP.View.Edit;
using DP.ViewModel.Edit;
using DP.ViewModel.Del;
using DP.View.Del;
using System.Windows;

namespace DP.View
{
    public static class ViewRequest
    {
        internal static void RequestDC(ViewModelBase vm, Window view)
        {
            vm.RequestClose += (s, e) => view.Close();
            view.DataContext = vm;
            view.ShowDialog();
        }
    }
}
