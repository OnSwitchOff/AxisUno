using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Services.Navigation
{
    public interface IViewsService
    {
        Type GetViewType(string key);
    }
}
