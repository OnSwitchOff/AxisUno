using FluentValidation;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AxisUno.PipeLines.Validation
{
    internal sealed class ValidationRequestPreProcessor<TRequest> : IRequestPreProcessor<TRequest>
        where TRequest : notnull
    {
        private readonly IList<IValidator<TRequest>> _validators;

        public ValidationRequestPreProcessor(IList<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(error => error != null)
                .ToList();

            if (errors.Any())
            {
                var errorBuilder = new StringBuilder();

                errorBuilder.AppendLine("Invalid command, reason: ");

                errors.ForEach(error => errorBuilder.AppendLine(error.ErrorMessage));

                throw new InvalidCommandException(errorBuilder.ToString());
            }

            return Task.CompletedTask;
        }
    }
}
