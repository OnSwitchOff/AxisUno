using AxisUno.DataBase.Enteties.Operations;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.Enteties.OperationHeaders
{
    public interface IAcctProvider
    {
        int GetNextAcct(OperationType operationType);
    }
}
