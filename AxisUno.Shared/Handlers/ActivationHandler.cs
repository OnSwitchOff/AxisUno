using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.Handlers
{
    public abstract class ActivationHandler<T> : IActivationHandler
            where T : class
    {
        /// <inheritdoc/>
        public bool CanHandle(object args)
        {
            return args is T && CanHandleInternal(args as T);
        }

        /// <inheritdoc/>
        public async Task HandleAsync(object args)
        {
            await HandleInternalAsync(args as T);
        }

        /// <summary>
        /// Function, that incapsulates <see cref="HandleAsync(object)"/> function, and allows to use typed parameter.
        /// </summary>
        /// <param name="args">Typed argument, that can be used for object activation.</param>
        /// <returns><inheritdoc cref="IActivationHandler.HandleAsync(object)" path="/returns"/></returns>
        protected abstract Task HandleInternalAsync(T? args);

        /// <summary>
        /// Function, that incapsulates <see cref="CanHandle(object)"/> function, and allows to use typed parameter.
        /// </summary>
        /// <param name="args">Typed argument, that can be used for object activation.</param>
        /// <returns><inheritdoc cref="IActivationHandler.CanHandle(object)" path="/returns"/></returns>
        protected virtual bool CanHandleInternal(T? args)
        {
            return true;
        }
    }
}
