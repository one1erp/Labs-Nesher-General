using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DAL.Logic;

namespace DAL.Logic
{
    class ResultLogic : BaseLogic
    {
        internal static bool CheckValue(Entities context, string name, decimal value)
        {

            var q = (from item in context.Results
                     where item.Name == name
                     select new { item.Minimum, item.Maximum }).FirstOrDefault();
            return value >= q.Minimum && value <= q.Maximum;


        }

        internal static Result IsGoodResultForEntry(Entities context, int resultId, params char[] goodStatuses)
        {


            var result = (context.Results.Where(item => item.ResultId == resultId)).FirstOrDefault();
            //If result is
            if (result != null)
            {
                //Check if good status result 
                foreach (var status in goodStatuses)
                {
                    if (status.ToString() == result.Status)
                    {
                        return result;
                    }
                }


            }
            return null;

        }

        internal static int GetResultDuplicates(Entities context, long? testId, long? templateId)
        {
            var dup = (from result in context.Results
                       where result.TestId == testId
                             && result.ResultTemplateId == templateId
                       select result).Count();
            return dup;
        }

        internal static Result GetResultByName(Entities context, string resultName)
        {
            return (from item in context.Results where item.Name == resultName select item).FirstOrDefault();

        }

        public static Result GetResultById(Entities context, long resultId)
        {
            return (from item in context.Results where item.ResultId == resultId select item).FirstOrDefault();
        }


        internal static void UpdateDilution(Entities context, int dilution, long resultId)
        {
            string sql = "update lims_sys.result set dilution_factor='" + dilution +
                         "' where   result_id=+'" + resultId + "'";
            
            var ddd =
                context.ExecuteStoreCommand(sql);
   

        }
    }


}
