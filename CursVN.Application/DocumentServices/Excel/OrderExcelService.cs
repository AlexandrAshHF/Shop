using CursVN.Core.Abstractions.Other;
using CursVN.Core.Models;
using Aspose.Cells;
using System.IO;
using MailKit.Search;
using Aspose.Cells.Charts;

namespace CursVN.Application.DocumentServices.Excel
{
    public class OrderExcelService : IDocService<List<Order>>
    {
        private List<Tuple<DateTime, decimal>> GetTableObject(List<Order> model)
        {
            model.Sort((first, second)
                => first.DateOfCreate.CompareTo(second.DateOfCreate));

            List<Tuple<DateTime, decimal>> ordersTable = new List<Tuple<DateTime, decimal>>();

            foreach (var item in model)
            {
                int index = ordersTable.FindIndex(x => x.Item1.Date.Equals(item.DateOfCreate.Date));

                if (index >= 0)
                {
                    Tuple<DateTime, decimal> newValue = new Tuple<DateTime, decimal>
                        (
                            item1: ordersTable[index].Item1,
                            item2: ordersTable[index].Item2 + item.Amount
                        );

                    ordersTable[index] = newValue;
                }
                else
                {
                    ordersTable.Add(new Tuple<DateTime, decimal>(item.DateOfCreate, item.Amount));
                }
            }

            return ordersTable;
        }
        public Task<MemoryStream> CreateDocument(List<Order> model)
        {
            return Task.Run(() =>
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = workbook.Worksheets[0];

                var tableObj = GetTableObject(model);

                for (int i = 0; i < tableObj.Count; i++)
                {
                    worksheet.Cells[i, 0].PutValue(tableObj[i].Item1.ToString("dd-MM-yyyy"));
                    worksheet.Cells[i, 1].PutValue(tableObj[i].Item2);
                }

                Style style = workbook.CreateStyle();
                style.Custom = "dd-mm-yyyy";

                worksheet.Cells.Columns[0].ApplyStyle(style, new StyleFlag() { NumberFormat = true });

                int chartIndex = worksheet.Charts.Add(ChartType.Column, 5, 0, 15, 5);
                Chart chart = worksheet.Charts[chartIndex];

                chart.NSeries.Add("B1:B" + tableObj.Count, true);
                chart.NSeries.CategoryData = "A1:A" + tableObj.Count;

                chart.CategoryAxis.Title.Text = "Date of order";
                chart.ValueAxis.Title.Text = "Amount";

                MemoryStream stream = workbook.SaveToStream();
                stream.Position = 0;

                return stream;
            });
        }
    }
}
