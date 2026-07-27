using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Services
{
    public interface IPdfExportService
    {
        byte[] ExportToPdf<T>(
            IEnumerable<T> data,
            string reportTitle,
            bool isLandscape = false);
    }
}
