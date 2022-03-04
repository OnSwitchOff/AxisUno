using AxisUno.DataBase.Enteties.Monney.CurrenciesExchange;
using AxisUno.DataBase.Enteties.Monney.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Monney
{
    public record MoneyValue : ValueObject
    {
        public MoneyValue(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public decimal Value { get; }

        public Currency Currency { get; }

        public static MoneyValue operator +(MoneyValue moneyValueLeft, MoneyValue moneyValueRight)
        {
            CheckRule(new MoneyValueOperationMustBePerformedOnTheSameCurrencyRule(moneyValueLeft, moneyValueRight));

            return new MoneyValue(moneyValueLeft.Value + moneyValueRight.Value, moneyValueLeft.Currency);
        }

        public static MoneyValue operator *(decimal coefficient, MoneyValue moneyValue)
        {
            return new MoneyValue(moneyValue.Value * coefficient, moneyValue.Currency);
        }
    }
}
