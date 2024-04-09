using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Logic
{
    class CalculationFactorLogic
    {
        internal static CalculationFactor GetCalculationFactor(Entities context, string tableName, string recordId, string calculationFactorName)
        {

            long record = long.Parse(recordId);
            var calculationFactor = (from item in context.CalculationFactors
                                     where item.TableName == tableName && item.RecordId == record && item.Name == calculationFactorName
                                     select item).FirstOrDefault();
            return calculationFactor;
        }
    }
}
