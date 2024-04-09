using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public partial class Sample
    {

        //שמות המשתנים במחלקה זו לא ניתנים לשינוי
        //תלוי בהם פונקציונליות במסך הזמנה ובהפקת תעודה
        public string ProductName
        {
            get
            {
                return
                   this.Product.Name;
            }
        }
        public string ProductDescription
        {
            get
            {
                return
                   this.Product.Description;
            }
        }
        public string SampledByOperatorName
        {
            get
            {
                var sampledByOperator = this.SampledByOperator;
                if (sampledByOperator != null)
                    return
                        sampledByOperator
                        .Name;
                else
                {
                    return "";
                }
            }
        }
    }
}
