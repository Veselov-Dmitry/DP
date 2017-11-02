using System;
using System.Windows.Documents;
using System.Windows.Controls;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using Tables = MigraDoc.DocumentObjectModel.Tables  ;
using PdfSharp;
using DP.Model;
using System.Diagnostics;
using System.Windows;

namespace DP.PDF
{
    public class PDFClass
    {
        private string filename = "";

        public double PrintableAreaHeight { get; internal set; }
        public double PrintableAreaWidth { get; internal set; }
        

        public bool ShowDialog(string FileName)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = FileName; // Default file name
            dlg.DefaultExt = ".pdf"; // Default file extension
            dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

            // Show save file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process save file dialog box results
            if (result == true)
            {
                // Save document
                filename = dlg.FileName;
                return true;
            }
            else return false;
        }



        public void SaveDocument(DataGrid dg)
        {
            //http://pdfsharp.net/wiki/HelloMigraDoc-sample.ashx
            Document document = new Document();
            MigraDoc.DocumentObjectModel.Section section = document.AddSection();


            //таблица PDF
            Tables.Table table = new Tables.Table();
            table.Borders.Width = 0.75;

            int margin = 10;
            Rect r = new Rect { Height = 297, Width = 210 };
            r.X = margin;
            r.Y = margin;
            r.Width = r.Width - r.X * 2;
            r.Height = r.Height - r.Y * 2;

            section.PageSetup.LeftMargin = Unit.FromMillimeter(r.X);
            section.PageSetup.RightMargin = Unit.FromMillimeter(r.Y);

            #region DefineContentSection 
            section.PageSetup.OddAndEvenPagesHeaderFooter = true;
            section.PageSetup.StartingNumber = 1;

            //HeaderFooter header = section.Headers.Primary;
            //header.AddParagraph("\tЭкспорт");

            // Create a paragraph with centered page number. See definition of style "Footer".
            MigraDoc.DocumentObjectModel.Paragraph paragraph = new MigraDoc.DocumentObjectModel.Paragraph();
            paragraph.AddTab();
            paragraph.AddPageField();

            // Add paragraph to footer for odd pages.
            section.Footers.Primary.Add(paragraph);
            // Add clone of paragraph to footer for odd pages. Cloning is necessary because an object must
            // not belong to more than one other object. If you forget cloning an exception is thrown.
            section.Footers.EvenPage.Add(paragraph.Clone());
            #endregion



            Tables.Row row = null;
            Tables.Cell cell = null;
            int counterCell = 0;

            bool first = true;
            foreach (DataGridColumn op in dg.Columns)
            {
                Tables.Column col = null;
                //создать колонку в PDF
                if (first)
                {
                    col = table.AddColumn(Unit.FromMillimeter(10));
                    first = false;
                }
                else
                    col = table.AddColumn(Unit.FromMillimeter((r.Width - 10 )/ (dg.Columns.Count - 1)));
                col.Format.Alignment = ParagraphAlignment.Center;
            }

            //создать строку
            row = table.AddRow();
            row.Shading.Color = Colors.PaleGoldenrod;

            // проход по всем колонкам в видимой таблице
            foreach (DataGridColumn op in dg.Columns)
            { 
                //выбор ячейки в таблице
                cell = row.Cells[counterCell];
                // заполнить значение ячейки параграфом
                cell.AddParagraph(op.Header.ToString());
                counterCell++;
            }

            // проход по строкам видимой таблицы
            foreach (CommonClass cc in dg.Items)
            {
                //добавить строку в PDF таблицу
                row = table.AddRow();
                counterCell = 0;
                //в текущей строке пройти по колонке
                foreach (DataGridColumn op in dg.Columns)
                {
                    //позиция в видимой таблице
                    int col = op.DisplayIndex;
                    //имя текущего свойства привязанного к датагрид
                    string path = dg.Columns[col].SortMemberPath;
                    //собственный метод для возвращения по имени свойства
                    string value = cc.NameOf(path).ToString();

                    //выбор ячейки в таблице
                    cell = row.Cells[counterCell];
                    // заполнить значение ячейки параграфом
                    cell.AddParagraph(value);
                    counterCell++;
                }
            }
            //обводка результатов таблицы
            table.SetEdge(0, 1/*смещение обводки для хедера*/, dg.Columns.Count, dg.Items.Count, Tables.Edge.Box, BorderStyle.Single, 1.5, Colors.Black);
            document.LastSection.Add(table);

            
            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = document;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(filename);

            Process.Start(filename);
        }
    }
}
