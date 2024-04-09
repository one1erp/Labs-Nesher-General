using System.Linq;

namespace DAL.Logic
{
     class AliquotLogic:BaseLogic
    {
         internal static Aliquot GetParentAliquot(Entities context, long childId)
         {
             var af = context.ALIQUOT_FORMULATION.Where(x => x.CHILD_ALIQUOT_ID == childId).SingleOrDefault();
             var parent = context.Aliquots.Where(a => a.AliquotId == af.PARENT_ALIQUOT_ID).FirstOrDefault();
             return parent;

         }
    }
}