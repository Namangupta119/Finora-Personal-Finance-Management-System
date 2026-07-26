using ClosedXML.Excel;
using Finora.Application.Common.Attributes;
using Finora.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Finora.Infrastructure.Services
{
    public class ExcelExportService : IExcelExportService
    {
        public byte[] ExportToExcel<T>(IEnumerable<T> data,string worksheetName,string reportTitle)
        {
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add(worksheetName);

            var properties = typeof(T)
                .GetProperties()
                .Where(p => !Attribute.IsDefined(p, typeof(IgnoreColumnAttribute)))
                .ToList();

            // ============================================
            // Report Title
            // ============================================

            worksheet.Cell(1, 1).Value = reportTitle;

            var titleRange = worksheet.Range(1, 1, 1, properties.Count);

            titleRange.Merge();

            titleRange.Style.Font.Bold = true;
            titleRange.Style.Font.FontSize = 16;
            titleRange.Style.Font.FontColor = XLColor.White;
            titleRange.Style.Fill.BackgroundColor = XLColor.DarkBlue;
            titleRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            titleRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

            worksheet.Row(1).Height = 30;

            // ============================================
            // Column Headers
            // ============================================

            for (int column = 0; column < properties.Count; column++)
            {
                var property = properties[column];

                var displayName =
                    property.GetCustomAttribute<DisplayAttribute>()?.Name
                    ?? property.Name;

                worksheet.Cell(2, column + 1).Value = displayName;
            }

            var headerRange = worksheet.Range(2, 1, 2, properties.Count);

            headerRange.Style.Font.Bold = true;
            headerRange.Style.Font.FontColor = XLColor.White;
            headerRange.Style.Fill.BackgroundColor = XLColor.CornflowerBlue;
            headerRange.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            headerRange.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
            headerRange.Style.Alignment.WrapText = true;

            headerRange.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            headerRange.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

            worksheet.Row(2).Height = 25;

            // ============================================
            // Data
            // ============================================

            int row = 3;

            foreach (var item in data)
            {
                for (int column = 0; column < properties.Count; column++)
                {
                    var property = properties[column];

                    var value = property.GetValue(item);

                    var cell = worksheet.Cell(row, column + 1);

                    switch (value)
                    {
                        case null:
                            cell.Value = string.Empty;
                            break;

                        case DateTime date:
                            cell.Value = date;
                            cell.Style.DateFormat.Format = "yyyy-MM-dd";
                            break;

                        case DateTimeOffset dateTimeOffset:
                            cell.Value = dateTimeOffset.DateTime;
                            cell.Style.DateFormat.Format = "yyyy-MM-dd";
                            break;

                        case decimal number:
                            cell.Value = number;

                            if (IsCurrencyColumn(property.Name))
                            {
                                cell.Style.NumberFormat.Format = "#,##0.00";
                            }

                            break;

                        case double number:
                            cell.Value = number;
                            cell.Style.NumberFormat.Format = "#,##0.00";
                            break;

                        case float number:
                            cell.Value = number;
                            cell.Style.NumberFormat.Format = "#,##0.00";
                            break;

                        case int number:
                            cell.Value = number;
                            break;

                        case long number:
                            cell.Value = number;
                            break;

                        case bool boolValue:
                            cell.Value = boolValue ? "Yes" : "No";
                            break;

                        default:
                            cell.Value = value?.ToString();
                            break;
                    }

                    if (property.PropertyType == typeof(decimal) ||
                        property.PropertyType == typeof(double) ||
                        property.PropertyType == typeof(float) ||
                        property.PropertyType == typeof(int) ||
                        property.PropertyType == typeof(long))
                    {
                        cell.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Right;
                    }
                    else
                    {
                        cell.Style.Alignment.Horizontal =
                            XLAlignmentHorizontalValues.Left;
                    }
                }

                // Zebra Rows

                if (row % 2 != 0)
                {
                    worksheet.Range(row, 1, row, properties.Count)
                        .Style.Fill.BackgroundColor = XLColor.AliceBlue;
                }

                row++;
            }

            // ============================================
            // Worksheet Formatting
            // ============================================

            worksheet.Columns().AdjustToContents();

            worksheet.SheetView.FreezeRows(2);

            worksheet.Range(2, 1, 2, properties.Count).SetAutoFilter();

            var usedRange = worksheet.RangeUsed();

            if (usedRange != null)
            {
                usedRange.Style.Border.OutsideBorder =
                    XLBorderStyleValues.Thin;

                usedRange.Style.Border.InsideBorder =
                    XLBorderStyleValues.Thin;
            }

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            return stream.ToArray();
        }

        private static bool IsCurrencyColumn(string propertyName)
        {
            string[] currencyColumns =
            {
                "Amount",
                "Price",
                "Value",
                "Profit",
                "Expense",
                "Income",
                "Balance",
                "Total"
            };

            return currencyColumns.Any(x =>
                propertyName.Contains(
                    x,
                    StringComparison.OrdinalIgnoreCase));
        }
    }
}
