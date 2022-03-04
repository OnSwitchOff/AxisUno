using AxisUno.BusinessRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.Products.Rules
{
    internal class NameMustNotBeEmptyRule : IBusinessRule
    {
        private readonly string _name;

        public NameMustNotBeEmptyRule(string name)
        {
            _name = name;
        }

        /// <inheritdoc/>
        public string Message => "Product name can't be empty";

        /// <summary>
        /// Checks, new product name, that can't be empty
        /// </summary>
        /// <returns>True, if name is null or empty</returns>
        public bool BrokenWhen()
        {
            return string.IsNullOrEmpty(this._name);
        }
    }
}
