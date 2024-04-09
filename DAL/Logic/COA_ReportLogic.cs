using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Logic
{
    class COA_ReportLogic : BaseLogic
    {

        internal static void CancelOldCoa(Entities context, string coaName)
        {

            int startIndex = coaName.IndexOf("(") + 1;
            int endIndex = coaName.IndexOf(")");

            var length = endIndex - startIndex;
            var i = Convert.ToInt32(coaName.Substring(startIndex, length));

            //int i = coaName.Substring(coaName.IndexOf("(") + 1, 1));
            if (i > 1)
            {

                var coaToCancelname = coaName.Replace("(" + i + ")", "(" + (i - 1) + ")");

                var oldCoa = GetEntityByIdentity<COA_Report>(context, x => x.Name == coaToCancelname);

                oldCoa.Status = "X";

            }

        }


    }
}
