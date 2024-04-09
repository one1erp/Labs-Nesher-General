using System.Collections.Generic;
using System.Linq;


namespace DAL.Logic
{
    class ChallengeLogic : BaseLogic
    {


        internal static List<Challenge> GetChallenge(Entities context, string standard, string category, string microbe)
        {
            var challenges = (from item in context.Challenges
                              where item.Standard == standard && item.Category == category && item.Microbe == microbe
                              select item);
            return challenges.ToList();
        }
        internal static List<Challenge> GetChallengeByStandard(Entities context, string standard, string category)
        {
            var challenges = (from item in context.Challenges
                              where item.Standard == standard && item.Category == category
                              select item);
            return challenges.ToList();
        }
    }
}
