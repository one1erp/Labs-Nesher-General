using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class PhraseHeader
    {
        public Dictionary<string, string> PhraseEntriesDescriptionByName
        {
            get
            {
                Dictionary<string, string> phraseEntriesDic = new Dictionary<string, string>();
                foreach (PhraseEntry entry in this.PhraseEntries)
                {
                    phraseEntriesDic.Add(entry.PhraseName, entry.PhraseDescription);
                }
                return phraseEntriesDic;

            }
        }
  
    }
}