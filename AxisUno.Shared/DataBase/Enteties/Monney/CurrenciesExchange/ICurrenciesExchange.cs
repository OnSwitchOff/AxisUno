using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Monney.CurrenciesExchange
{
    public interface ICurrenciesExchange
    {
        List<ConversionRate> GetConversionRates();
    }
}
