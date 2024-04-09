using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;

namespace DAL
{
    public partial class Aliquot:EntityObject
    {
        public List<Aliquot> Children
        {
            get
            { 
                List<Aliquot> aliquotsList = AliquotChilds.Select(a => a.AliqoutParent).ToList();
                return aliquotsList;
            }
        }
        public List<Aliquot> Parent
        {
            get
            {
                List<Aliquot> aliquotsList = AliquotParent.Select(a => a.AliqoutChild).ToList();
                return aliquotsList;
            }
        }

    }
}
