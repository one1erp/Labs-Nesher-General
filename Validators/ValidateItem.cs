using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Validators
{


    public static class ValidateItem
    {
        public static bool IsValidEmail(string email)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            return !String.IsNullOrEmpty(email) && re.IsMatch(email);
        }

        public static bool IsValidName(string name)
        {
            return !string.IsNullOrEmpty(name) && char.IsLetter(name[0]) && name.Length > 3;
        }

        public static bool IsValidPhone(string phone)
        {
            string strRegex = @"^[1-9]\d{2}-[1-9]\d{2}-\d{4}$";
            Regex re = new Regex(strRegex);
            return !String.IsNullOrEmpty(phone) && re.IsMatch(phone);
        }

        public static bool? ConvertToBoolean(char b)
        {

            switch (b)
            {
                case 'F':
                    return false;
                case 'T':
                    return true;
                default:
                    return null;

            }

        }
   
       public static char ConvertToNautilsuBoolean(bool b)
        {

            switch (b)
            {
                case true:
                    return 'T';
                case false:
                    return 'F';
             

            }
            return 'F';

        }
    }
}