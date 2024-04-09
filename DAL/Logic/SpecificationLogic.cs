using System.Collections.Generic;
using System.Linq;


namespace DAL.Logic
{
    class SpecificationLogic : BaseLogic
    {


        internal static List<EntitySpecification> GetSpecification(Entities context, string tableName, long prodactItemId)
        {
            var es = (from item in context.EntitySpecifications
                      where item.TableName == tableName && item.RecordId == prodactItemId
                      select item);
            return es.ToList();
        }
    }
}
