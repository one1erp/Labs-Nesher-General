using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using Telerik.WinControls.UI;


namespace Validators
{
    public class MyPhraseComboBoxColumn : GridViewComboBoxColumn
    {
        private PhraseHeader _phraseHeader;
        private IDataLayer dal;

        public MyPhraseComboBoxColumn(string phraseName)
            : base()
        {
            _phraseHeader = GetPhraseByName(phraseName);
            this.DataSource = _phraseHeader.PhraseEntries;
        }

        public MyPhraseComboBoxColumn(string fieldName, string phraseName)
            : base(fieldName)
        {
            _phraseHeader = GetPhraseByName(phraseName);
            this.DataSource = _phraseHeader.PhraseEntries;
        }

        public MyPhraseComboBoxColumn(string uniqueName, string fieldName, string phraseName, IDataLayer dal)
            : base(uniqueName, fieldName)
        {
            this.dal = dal;
            _phraseHeader = GetPhraseByName(phraseName);
            this.DataSource = _phraseHeader.PhraseEntries;
        }


        private PhraseHeader GetPhraseByID(long phraseID)
        {

            var a = dal.GetPhraseByID(phraseID);

            return a;
        }
        private PhraseHeader GetPhraseByName(string phraseName)
        {

            var a = dal.GetPhraseByName(phraseName);
            return a;
        }
    }

}
