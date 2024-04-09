using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DAL.Logic
{
    internal class PhraseLogic : BaseLogic
    {
        public static PhraseHeader GetPhraseByID(Entities context, long phraseId)
        {

            var p = context.PhraseHeaders.Include("PhraseEntries").Where(x => x.PhraseId == phraseId).FirstOrDefault();
            return p;

        }

        internal static PhraseHeader GetPhraseByName(Entities context, string phraseName)
        {

            var p = context.PhraseHeaders.Include("PhraseEntries").Where(x => x.Name == phraseName).FirstOrDefault();
            return p;
        }


    }
}
