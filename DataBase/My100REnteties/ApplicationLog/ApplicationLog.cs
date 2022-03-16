using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.ApplicationLog.Rules;
using AxisUno.DataBase.My100REnteties.OperationHeaders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AxisUno.DataBase.My100REnteties.ApplicationLog
{
    public class ApplicationLog : Entity
    {
        public int Id { get; set; }
        public int Event { get; set; }
        public string Description { get; set; } = String.Empty;
        public DateTime RealTime { get; set; }
        public OperationHeader? OperationHeader { get; set; }

        public ApplicationLog()
        {

        }

        public ApplicationLog(int _event, string description, DateTime realTime, OperationHeader? operationHeader)
        {
            Event = _event;
            Description = description;
            RealTime = realTime;
            OperationHeader = operationHeader;
        }

        public static ApplicationLog Create(int _event, string description, DateTime realTime, OperationHeader? operationHeader)
        {
            CheckRule(new DescriptionMustNotBeEmptyRule(description));

            ApplicationLog applicationLog = new ApplicationLog(_event, description, realTime, operationHeader)
            {

            };

            return applicationLog;
        }
    }
}
