using System.Linq;

namespace DAL
{
    public class ExtansionLogic
    {
        public static Extension GetExtensionByName(Entities context, string extensionName)
        {

            return (from item in context.Extensions where item.Name == extensionName select item).FirstOrDefault();
        }
    }
}