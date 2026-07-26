using System;
using System.Collections.Generic;
using System.Text;

namespace Finora.Application.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class IgnoreColumnAttribute : Attribute
    {
    }
}
