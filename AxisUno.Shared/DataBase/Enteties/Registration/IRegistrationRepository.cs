using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AxisUno.DataBase.Enteties.Registration
{
    public interface IRegistrationRepository
    {
        Task<Registration> GetRegistrationAsync();

        Task UpdateRegistrationAsync(Registration registration);
    }
}
