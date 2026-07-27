using Finora.Application.Common.Attributes;
using Finora.Application.Interfaces.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Finora.Infrastructure.Services
{
    public class PdfExportService : IPdfExportService
    {
        static PdfExportService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public byte[] ExportToPdf<T>(IEnumerable<T> data,string reportTitle,bool isLandscape = false)
        {

            var properties = typeof(T)
                   .GetProperties()
                   .Where(p => !Attribute
                   .IsDefined(p, typeof(IgnoreColumnAttribute))).ToList();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(isLandscape
                    ? PageSizes.A4.Landscape()
                    : PageSizes.A4);

                    page.Margin(30);

                    page.Header().Column(column =>
                    {
                        column.Item()
                            .Text(reportTitle)
                            .FontSize(18)
                            .Bold();

                        column.Item()
                            .PaddingTop(5)
                            .Text($"Generated On : {DateTime.Now:dd-MMM-yyyy hh:mm tt}")
                            .FontSize(9)
                            .FontColor(Colors.Grey.Darken1);
                    });



                    page.Content().PaddingTop(20).Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            foreach (var property in properties)
                            {
                                columns.RelativeColumn();
                            }
                        });

                        table.Header(header =>
                        {
                            foreach (var property in properties)
                            {
                                header.Cell()
                                .Element(HeaderCellStyle)
                                .AlignCenter()
                                .Text(GetDisplayName(property))
                                .Bold()
                                .FontSize(9);
                            }
                        });

                        foreach (var item in data)
                        {
                            foreach (var property in properties)
                            {
                                var value = property.GetValue(item);

                                table.Cell()
                                .Element(DataCellStyle)
                                .Text(FormatValue(value)).FontSize(8);
                            }
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });

            return document.GeneratePdf();
        }
        private static string GetDisplayName(PropertyInfo property)
        {
            return property
                .GetCustomAttribute<DisplayAttribute>()?.Name
                ?? property.Name;
        }

        private static string FormatValue(object? value)
        {
            if (value == null)
                return string.Empty;



            return value switch
            {
                DateTime date =>
                    date.ToString("yyyy-MM-dd"),

                DateTimeOffset dto =>
                    dto.ToString("yyyy-MM-dd"),

                bool b =>
                    b ? "Yes" : "No",

                decimal number =>
                    number.ToString("#,##0.00"),

                double number =>
                    number.ToString("#,##0.00"),

                float number =>
                    number.ToString("#,##0.00"),

                _ => value.ToString() ?? string.Empty
            };
        }

        private static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Border(1)
                .Padding(5)
                .Background(Colors.Blue.Medium);
        }

        private static IContainer DataCellStyle(IContainer container)
        {
            return container
                .Border(1)
                .Padding(3);
        }
    }
}
