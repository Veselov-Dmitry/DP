using System.ComponentModel;

namespace DP.ViewModel.Interfaces
{
    public interface IViewModelsResolver
    {
        /// <summary>
        /// в реализации по ключу возвращает инстанс необходимой VM для страницы,  ключи - константы MainViewModel
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        INotifyPropertyChanged GetViewModelInstance(string alias);
    }
}
