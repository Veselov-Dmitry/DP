using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.ViewModel.Interfaces
{

    interface IDialogService
    {
        int ShowMessage(string message, int typeButton);   // показ сообщения
        string FilePath { get; set; }   // путь к выбранному файлу
        bool OpenFileDialogXML();  // открытие файла
        bool SaveFileDialogXML();  // сохранение файла
    }
}
