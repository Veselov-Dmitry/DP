using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace DP.Print
{
    public class HeaderedFlowDocumentPaginator : DocumentPaginator
    {
        // Реальный класс разбиения на страницы (выполняющий всю работу по разбиению)
        private DocumentPaginator flowDocumentPaginator;

        // Сохранить класс разбиения на страницы FlowDocument из заданного документа
        public HeaderedFlowDocumentPaginator(FlowDocument document)
        {
            flowDocumentPaginator = ((IDocumentPaginatorSource)document).DocumentPaginator;
        }

        public override bool IsPageCountValid
        {
            get { return flowDocumentPaginator.IsPageCountValid; }
        }

        public override int PageCount
        {
            get { return flowDocumentPaginator.PageCount; }
        }

        public override Size PageSize
        {
            get { return flowDocumentPaginator.PageSize; }
            set { flowDocumentPaginator.PageSize = value; }
        }

        public override IDocumentPaginatorSource Source
        {
            get { return flowDocumentPaginator.Source; }
        }

        public override DocumentPage GetPage(int pageNumber)
        {
            // Получить запрошенную страницу
            DocumentPage page = flowDocumentPaginator.GetPage(pageNumber);

            // Поместить страницу в объект Visual. После этого можно 
            // будет применять трансформации и добавлять другие элементы
            ContainerVisual newVisual = new ContainerVisual();
            newVisual.Children.Add(page.Visual);

            // Создать заголовок
            DrawingVisual header = new DrawingVisual();
            using (DrawingContext dc = header.RenderOpen())
            {
                Typeface typeface = new Typeface("Times New Roman");
                FormattedText text = new FormattedText("Страница " +
                    (pageNumber + 1).ToString(), System.Globalization.CultureInfo.CurrentCulture,
                    FlowDirection.LeftToRight, typeface, 14, Brushes.Black);

                // Оставить четверть дюйма пространства между краем страницы и текстом
                dc.DrawText(text, new Point(96 * 0.25, 96 * 0.25));
            }

            // Добавить заголовок к объекту Visual
            newVisual.Children.Add(header);

            // Поместить объект Visual в новую страницу
            DocumentPage newPage = new DocumentPage(newVisual);
            return newPage;
        }

    }
}
