using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Persistence.Seed.Common
{
    public class SeedConstants
    {
        public static readonly DateTimeOffset SeedCreatedOn = new(2000, 1, 1, 0, 0, 0, TimeSpan.Zero);
    }
}
