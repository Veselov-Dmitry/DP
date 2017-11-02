using System.Windows.Controls;

namespace DP.Navigation.Interfaces
{
    public interface IPageResolver
    {
        /// <summary>
        /// в реализации возвращает по запросу инстанс необходимой вьюшки, ключи храняться в NavigationClass
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        Page GetPageInstance(string alias);
    }
}
