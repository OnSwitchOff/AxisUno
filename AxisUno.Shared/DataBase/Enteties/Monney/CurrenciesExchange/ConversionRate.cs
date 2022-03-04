using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Monney.CurrenciesExchange
{
    public class ConversionRate
    {
        public ConversionRate(Currency sourceCurrency, Currency targetCurrency, decimal coefficient)
        {
            SourceCurrency = sourceCurrency;
            TargetCurrency = targetCurrency;
            Coefficient = coefficient;
        }

        public Currency SourceCurrency { get; }

        public Currency TargetCurrency { get; }

        public decimal Coefficient { get; }
    }
}
