using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Interfaces.Services
{
    public interface IPdfExportService
    {
        byte[] Export<T>(
            IEnumerable<T> data,
            string title);
    }
}
