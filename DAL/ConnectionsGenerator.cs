using System;
using System.Collections.Generic;
using System.Data.EntityClient;

using System.Data.SqlClient;
using System.Linq;
using System.Text;
using LSSERVICEPROVIDERLib;
using Oracle.DataAccess.Client;

//using Oracle.DataAccess.Client;

namespace DAL
{
    public class ConnectionsGenerator
    {
        private readonly string _adoConString;
        private readonly INautilusDBConnection _nautilusDbConnection;

        public ConnectionsGenerator(string adoConString, INautilusDBConnection nautilusDbConnection)
        {
            _adoConString = adoConString;
            _nautilusDbConnection = nautilusDbConnection;
        }

        public OracleConnection GetAdoConnection()
        {
            //Create the connection
            OracleConnection connection = new OracleConnection(_adoConString);

            // Open the connection
            connection.Open();

            // Get lims user password
            string limsUserPassword = _nautilusDbConnection.GetLimsUserPwd();

            // Set role lims user
            string roleCommand;
            if (limsUserPassword == "")
            {
                // LIMS_USER is not password protected
                roleCommand = "set role lims_user";
            }
            else
            {
                // LIMS_USER is password protected.
                roleCommand = "set role lims_user identified by " + limsUserPassword;
            }

            // set the Oracle user for this connecition
            OracleCommand command = new OracleCommand(roleCommand, connection);

            // Try/Catch block
            try
            {
                // Execute the command
                command.ExecuteNonQuery();
            }
            catch (Exception f)
            {
                // Throw the exception
                throw new Exception("Inconsistent role Security : " + f.Message);
            }

            // Get the session id
            double sessionId = _nautilusDbConnection.GetSessionId();

            // Connect to the same session
            string sSql = string.Format("call lims.lims_env.connect_same_session({0})", sessionId);

            // Build the command
            command = new OracleCommand(sSql, connection);

            // Execute the command
            command.ExecuteNonQuery();

            return connection;
        }

        public string GetEfConString()
        {
            string providerName = "Oracle.DataAccess.Client";


            // Initialize the connection string builder for the
            // underlying provider.
            SqlConnectionStringBuilder sqlBuilder =
            new SqlConnectionStringBuilder();


            sqlBuilder.ConnectionString = _adoConString;

            // Build the SqlConnection connection string.
            string providerString = sqlBuilder.ToString();

            // Initialize the EntityConnectionStringBuilder.
            EntityConnectionStringBuilder entityBuilder =
            new EntityConnectionStringBuilder();

            //Set the provider name.
            entityBuilder.Provider = providerName;

            // Set the provider-specific connection string.
            entityBuilder.ProviderConnectionString =SetLimsSys(providerString);

            // Set the Metadata location.
            entityBuilder.Metadata = @"res://*/NautilusModel1.csdl|" +
                        "res://*/NautilusModel1.ssdl|" +
                        "res://*/NautilusModel1.msl";

            return entityBuilder.ToString();
        }

        private string SetLimsSys(string providerString)
        {
            string removeUserId = providerString.Remove(providerString.IndexOf("User ID"));
            string conStr = removeUserId + "User ID=lims_sys;Password=lims_sys"; 
            return conStr;
        }

     
    }
}
