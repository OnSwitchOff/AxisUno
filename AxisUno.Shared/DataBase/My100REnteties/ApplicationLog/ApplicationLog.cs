using AxisUno.DataBase.Enteties;
using AxisUno.DataBase.My100REnteties.ApplicationLog.Rules;
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
        public int? OperationHeaderID { get; set; }

        public ApplicationLog(int _event, string description, DateTime realTime, int? operationHeaderID)
        {
            Event = _event;
            Description = description;
            RealTime = realTime;
            OperationHeaderID = operationHeaderID;
        }

        public static ApplicationLog Create(int _event, string description, DateTime realTime, int? operationHeaderID)
        {
            CheckRule(new DescriptionMustNotBeEmptyRule(description));

            ApplicationLog applicationLog = new ApplicationLog(_event, description, realTime, operationHeaderID)
            {

            };

            return applicationLog;
        }
    }
}
