using AxisUno.BusinessRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.My100REnteties.ApplicationLog.Rules
{
    internal class DescriptionMustNotBeEmptyRule : IBusinessRule
    {
        private readonly string _description;

        public DescriptionMustNotBeEmptyRule(string description)
        {
            _description = description;
        }

        /// <inheritdoc/>
        public string Message => "Description description can't be empty";

        /// <summary>
        /// Checks, description, that can't be empty
        /// </summary>
        /// <returns>True, if description is null or empty</returns>
        public bool BrokenWhen()
        {
            return string.IsNullOrEmpty(this._description);
        }
    }
}
