using AxisUno.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AxisUno.AutofacModules.Helpers
{
    internal static class MediatorAssemblies
    {
        internal static readonly Assembly ApplicationLayer = typeof(MainViewModel).Assembly;
    }
}
