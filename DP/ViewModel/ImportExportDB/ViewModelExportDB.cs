using DP.Model;
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
    class ViewModelExportDB: ViewModelBase
    {
        //ПОЛЯ
        IFileService fileService;
        IDialogService dialogService;
        IGeterModel geterModel;
        //СВОЙСТВА




        //КОНСТРУКТОР
        public ViewModelExportDB( IFileService fileService, IDialogService dialogService, IGeterModel geterModel)
        {
            this.fileService = fileService;
            this.dialogService = dialogService;
            this.geterModel = geterModel;
            
                
            ExitCommand = new RelayCommand<object>(arg => CloseMethod());
                
            if (dialogService.SaveFileDialogXML())
            {
                fileService.Save(dialogService.FilePath, geterModel.GetAllTables());
            }

        }
        
    }
}
