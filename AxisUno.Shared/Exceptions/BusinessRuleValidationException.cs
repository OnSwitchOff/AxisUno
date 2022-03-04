﻿using AxisUno.BusinessRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.Exceptions
{
    public class BusinessRuleValidationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessRuleValidationException"/> class.
        /// </summary>
        /// <param name="brokenRule">Rule, that had broken the validation.</param>
        public BusinessRuleValidationException(IBusinessRule brokenRule)
            : base(brokenRule.Message)
        {
            BrokenRule = brokenRule;
            Details = brokenRule.Message;
        }

        /// <summary>
        /// Rule, that had broken the validation.
        /// </summary>
        public IBusinessRule BrokenRule { get; }

        /// <summary>
        /// Stores broken rule message.
        /// </summary>
        public string Details { get; }

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{BrokenRule.GetType().FullName}: {BrokenRule.Message}";
        }
    }
}