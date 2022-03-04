using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.Services.Activation
{
    public interface IActivationService
    {
        /// <summary>
        /// Runs activation handlers to perform the program activation.
        /// </summary>
        /// <param name="activationArgs">Stores information about application startup. Usually it's startup arguments.</param>
        /// <returns>Task with process of programm activation.</returns>
        Task ActivateAsync(LaunchActivatedEventArgs activationArgs);
    }
}
