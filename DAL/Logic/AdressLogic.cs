using System.Collections.Generic;
using System.Linq;


namespace DAL.Logic
{
    class AdressLogic : BaseLogic
    {


        internal static List<Address> GetAddresses(Entities context, string addressTableName, long addressItemId)
        {
            var address = (from item in context.Addresses1
                           where item.AddressTableName == addressTableName && item.AddressItemId == addressItemId
                           select item);
            return address.ToList();
        }
        internal static void AddAddress(Entities context, Address address )
        {

            context.Addresses1.AddObject(address);
        }

        internal static List<Address> GetAddressesByTable(Entities context, string table_Name)
        {
            var address = (from item in context.Addresses1
                           where item.AddressTableName ==table_Name
                           select item);
            return address.ToList();
        }
    }
}
