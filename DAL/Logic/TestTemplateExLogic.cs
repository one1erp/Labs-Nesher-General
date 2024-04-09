using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Linq.Expressions;


namespace DAL.Logic
{
    class TestTemplateExLogic : BaseLogic
    {

        public static List<TestTemplateEx> GetTestTemplatesForPriceList(Entities context)
        {
            return Find<TestTemplateEx>(context, tt => tt.Charge == "T");
        }

        public static string GetTestTemplatesFullNameByDescrpion(string descrpion, Entities context)
        {
            var firstOrDefault = (from item in context.TestTemplateExes where item.DESCRIPTION == "Challenge 0" select item.Workflow).FirstOrDefault();
            if (firstOrDefault != null)
                return
                    firstOrDefault.Name
                    ;
            return null;
        }

        internal static IQueryable<TestTemplateEx> GetTestTemplatesForPriceListIcludeWorkflow(Entities context)
        {
          return FindInclude<TestTemplateEx>(context, tt => tt.Charge == "T", "Workflow");
         
        }
    }
}
