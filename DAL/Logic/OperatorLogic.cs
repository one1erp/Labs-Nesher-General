using System.Collections.Generic;
using System.Linq;

namespace DAL.Logic
{
    internal class OperatorLogic : BaseLogic
    {

        public static List<Operator> GetOperatorsByRole(Entities context, string roleName)
        {
            var lr = context.LimsRoles.FirstOrDefault(x => x.Name == roleName);
            if (lr != null)
            {
                return lr.OPERATORs1.ToList();
            }
            return null;
        }
        public static IQueryable<Operator> GetOperatorsIncludeAll(Entities context)
        {

            
            var q = (from item in context.Operators.Include("OperatorRole").Include("OperatorGroups") select item);

            // var q = from item in context.OperatorGroups where groupIde == item.GroupId select item.Operator;
            return q;
        }
        public static IQueryable<Operator> GetOperatorsByGroup(Entities context, long groupIde)
        {


            var q = from item in context.OperatorGroups where groupIde == item.GroupId select item.Operator;
            return q;
        }
    }
}