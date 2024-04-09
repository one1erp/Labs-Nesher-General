using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace DAL.Logic
{
    internal class SdgLogic : BaseLogic
    {
        public static List<Sdg> GetSdgByClinet(Entities context, int clinetID)
        {
            var sdgs = (from item in context.Sdgs
                        where item.SdgClientId == clinetID
                        select item).ToList();
            return sdgs;
        }
        public static List<Sdg> GetSdgByClinetNotCancele(Entities context, int clinetID)
        {
            var sdgs = (from item in context.Sdgs
                        where item.SdgClientId == clinetID & item.Status != "X"
                        select item).ToList();
            return sdgs;
        }
        public static List<Client> ClinetBySdg(Entities context, DateTime fromDate, DateTime to, bool isCreatedOn)
        {
            List<Client> sdgs;
            if (isCreatedOn)
            {
                sdgs = (from item in context.Clients
                        where item.SDG_USER1.Any(s => s.CREATED_ON >= fromDate & s.CREATED_ON <= to)
                        select item).ToList();
            }
            else
            {
                sdgs = (from item in context.Clients
                        where item.SDG_USER1.Any(s => s.AUTHORISED_ON >= fromDate & s.AUTHORISED_ON <= to)
                        select item).ToList();
            }
            return sdgs;
        }
        public static List<Sdg> GetAothorizeSdgsByDate(Entities context, DateTime d_from, DateTime d_to)
        {

            var sdgs = (from item in context.Sdgs
                        where item.AUTHORISED_ON > d_from && item.AUTHORISED_ON < d_to && item.Status == "A"
                        select item).ToList();
            return sdgs;
        }

        internal static Sdg GetSdgTree(Entities context, long sdgId)
        {
           
            var sdg = (from item in context.Sdgs.Include("Samples")
                       .Include("Samples.Aliqouts")
                       .Include("Samples.Aliqouts.Tests")
                       .Include("Samples.Aliqouts.Tests.Results")
                       where item.SdgId == sdgId
                       select item).FirstOrDefault();

            return sdg;
        }
    }
}
