using DP.ViewModel.Interfaces;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DP
{

    class DefaultDialogServiceXML : IDialogService
    {
        /// <summary>
        /// диалоговое окно открытия файла 
        /// </summary>
        public string FilePath { get; set; }

        public bool OpenFileDialogXML()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files | *.xml";
            openFileDialog.DefaultExt = "xml";
            if (openFileDialog.ShowDialog() == true)
            {
                FilePath = openFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// диалоговое окно сохранения файла
        /// </summary>
        /// <returns>путь к файлу</returns>
        public bool SaveFileDialogXML()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files | *.xml";
            saveFileDialog.DefaultExt = "xml";
            if (saveFileDialog.ShowDialog() == true)
            {
                FilePath = saveFileDialog.FileName;
                return true;
            }
            return false;
        }
        /// <summary>
        /// окно сообщений
        /// </summary>
        /// <param name="message">some string</param>
        /// <param name="type">0 - MessageBoxResult.YesNo, !=0 MessageBoxButton.OK</param>
        /// <returns>0 - MessageBoxResult.Yes</returns>
        public int ShowMessage(string message, int typeButton = 1)
        {
            MessageBoxButton t = MessageBoxButton.OK;
            switch (typeButton)
            {
                case 0:
                    {
                        t = MessageBoxButton.YesNo;
                        return (MessageBox.Show(message, "", t) == MessageBoxResult.Yes) ? 0 : -1;
                    }
                case 1:
                    {
                        t = MessageBoxButton.OK;
                        return (MessageBox.Show(message, "", t) == MessageBoxResult.OK) ? 0 : -1;
                    }
                default:
                    {
                        t = MessageBoxButton.OK;
                        return (MessageBox.Show(message, "", t) == MessageBoxResult.OK) ? 0 : -1;
                    }
            };
        }
    }
}
