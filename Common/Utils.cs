using System;
using System.Drawing;
using System.Linq;
using LSSERVICEPROVIDERLib;

namespace Common
{
    public static class Utils
    {
        private static string _conString;
        private static INautilusDBConnection _ntlsCon;

        public static string ConString
        {
            get { return _conString; }
        }

        public static INautilusDBConnection NautilusDbConnection
        {
            get { return _ntlsCon; }
        }

        public static void CreateConstring(INautilusDBConnection ntlsCon)
        {
            if (ntlsCon != null)
            {
                string ntlsConString = ntlsCon.GetADOConnectionString();
                string[] splited = ntlsConString.Split(';');
                string connectionString = "";

                for (int i = 1; i < splited.Count(); i++)
                    connectionString += splited[i] + ';';

                _conString = connectionString;
                _ntlsCon = ntlsCon;
            }
        }

        public static INautilusProcessXML GetXmlProcessor(INautilusServiceProvider sp)
        {
            if (sp != null)
                return sp.QueryServiceProvider("ProcessXML") as NautilusProcessXML;
            else
                return null;
        }

        public static INautilusDBConnection GetNtlsCon(INautilusServiceProvider sp)
        {
            if (sp != null)

                return sp.QueryServiceProvider("DBConnection") as NautilusDBConnection;
            else
                return null;
        }

        public static NautilusUser GetNautilusUser(INautilusServiceProvider sp)
        {
            if (sp != null)

                return sp.QueryServiceProvider("User") as NautilusUser;
            else
                return null;
        }

        public static INautilusSchema GetSchema(INautilusServiceProvider sp)
        {
            if (sp != null)


                return sp.QueryServiceProvider("Schema") as INautilusSchema;
            else
                return null;
        }

        public static INautilusInternationalise GetInternationalise(INautilusServiceProvider sp)
        {
            if (sp != null)
                return sp.QueryServiceProvider("Internationalise") as LSSERVICEPROVIDERLib.INautilusInternationalise;
            return null;

        }

        public static INautilusPopupMenu GetPopupMenu(INautilusServiceProvider sp)
        {
            if (sp != null)
                return sp.QueryServiceProvider("PopupMenu") as INautilusPopupMenu;

            return null;

        }
        public static INautilusExplorer NautilusExplorer(INautilusServiceProvider sp)
        {
            if (sp != null)
                return sp.QueryServiceProvider("Explorer") as INautilusExplorer;

            return null;

        }

        public static void CreateConstring(object ntlsCon)
        {
            throw new NotImplementedException();
        }
    }

    
}