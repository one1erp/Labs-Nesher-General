using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Logic
{
    class U_SESS_OPERATORLogic : BaseLogic
    {
        internal static void AddSessOPP(Entities context,  U_SESS_OPERATOR sop)
        {

            context.U_SESS_OPERATOR.AddObject(sop);
       
        }

        internal static U_SESS_OPERATOR HasSession(Entities context, double sid)
        {
            decimal s =Convert.ToDecimal(sid);
            var b =context.U_SESS_OPERATOR.FirstOrDefault(item=>item.U_SID.HasValue && item.U_SID.Value == s);
                   return b;
            //item;
        }
    }
}
