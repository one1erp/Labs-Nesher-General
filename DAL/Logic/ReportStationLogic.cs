using System;
using System.Linq;
namespace DAL.Logic
{
    public static class ReportStationLogic
    {
        public static ReportStation GetReportStationByWorksAndType(Entities context, string workstation, string type)
        {
//            var reportstation = (context.ReportStations.Where(r => r.WORKSTATION.NAME == workstation && r.U_PRINTOUT_TYPE == type).FirstOrDefault());
            var reportstation = (from item in context.ReportStations
                                 where item.WORKSTATION.NAME == workstation && item.U_PRINTOUT_TYPE == type
                                 select item).FirstOrDefault();
            return reportstation;
        }
    }
}