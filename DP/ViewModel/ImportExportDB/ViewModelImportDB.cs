using DP.Model.Interfaces;
using DP.ViewModel.Interfaces;
using DP.XML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP.ViewModel.ImportExportDB
{
    class ViewModelImportDB:ViewModelBase
    {
        //ПОЛЯ
        IFileService fileService;
        IDialogService dialogService;
        IGeterModel geterModel;
        //СВОЙСТВА




        //КОНСТРУКТОР
        public ViewModelImportDB(IFileService fileService, IDialogService dialogService, IGeterModel geterModel)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
            this.geterModel = geterModel;


            ExitCommand = new RelayCommand<object>(arg => CloseMethod());

            if (dialogService.OpenFileDialogXML())
            {
                geterModel.SetAllTables(fileService.Open(dialogService.FilePath));
            }
        }

    }
}
