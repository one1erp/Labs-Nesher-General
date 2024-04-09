using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oracle.DataAccess.Client;

namespace DAL.Logic
{
    class ClientDataLogic : BaseLogic
    {
        internal static void AddClientData(Entities context, OracleConnection oracleConnection, U_CLIENT_DATA clientData)
        {

            clientData.U_CLIENT_DATA_ID = GetNewId(oracleConnection, "SQ_U_CLIENT_DATA");
            context.U_CLIENT_DATA.AddObject(clientData);
        }

        internal static U_CLIENT_DATA GetClientData(Entities context, long clientId, long labId)
        {
            var xml = (from item in context.U_CLIENT_DATA
                       where item.U_CLIENT_ID == clientId && item.U_LAB_ID == labId
                       select item).SingleOrDefault();


            return xml;
        }
    }
}
