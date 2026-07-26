using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Services
{
    public interface IExcelExportService
    {
        byte[] ExportToExcel<T>(IEnumerable<T> data,string worksheetName,string reportTitle);
    }
}
