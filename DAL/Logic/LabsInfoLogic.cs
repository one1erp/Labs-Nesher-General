using System.Linq;

namespace DAL
{
    public class LabsInfoLogic
    {
        internal static LabInfo GridLayoutByLab(string name, Entities context)
        {
            return (from item in context.LabInfoes where item.LabLetter == name select item).FirstOrDefault();
        }
    }
}