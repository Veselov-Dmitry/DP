using System;

namespace DP.Model.Interfaces
{
    interface IRequestCloseViewModel
    {
        /// <summary>
        /// событие, отвечающее за закрытие окна
        /// </summary>
        event EventHandler RequestClose;
    }
}
