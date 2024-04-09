using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using Oracle.DataAccess.Client;

namespace DAL.Logic
{
    class XmlStorageLogic : BaseLogic
    {

        internal static void AddXmlStorage(Entities context, OracleConnection oracle, XmlStorage xmlStorage)
        {

            //     xmlStorage.XmlStorageId   = GetNewId(oracle,"SQ_XML_STORAGE");
            xmlStorage.XmlStorageId = context.XmlStorages.Max(x => x.XmlStorageId) + 1;
            context.XmlStorages.AddObject(xmlStorage);
        }
        internal static XmlStorage GetXmlStorage(Entities context, string XmlStorageTableName, long entityId, long labId)
        {

            var xml = (from item in context.XmlStorages
                       where item.TableName == XmlStorageTableName && item.EntityId == entityId && item.LAB_ID == labId
                       select item).SingleOrDefault();


            return xml;
            //var command =
            //    new OracleCommand("select * from lims.xml_storage where table_name='" + XmlStorageTableName +
            //                      "'  and entity_id='" + XmlStorageItemId + "'");

            //OracleDataReader reader = command.ExecuteReader();
            //if (!reader.HasRows)
            //{
            //    //TODO:Insert into
            //}
            //else
            //{
            //    //TODO:Update xml
            //}
            //OracleCommand commandSQ = new OracleCommand("select lims." + "sequenceName" + ".nextval from dual", oracleConnection);

            //decimal newId = 0;

            //while (reader.Read())
            //{
            //    newId = (decimal)reader[0];
            //}
            //return (long)newId;

            //try
            //{



            //    OracleCommand command = new OracleCommand("select lims." + sequenceName + ".nextval from dual", oracleConnection);

            //    reader = command.ExecuteReader();
            //    decimal newId = 0;

            //    while (reader.Read())
            //    {
            //        newId = (decimal)reader[0];
            //    }
            //    return (long)newId;

            //}
            //catch (Exception ex)
            //{

            //    Logger.WriteLogFile(ex);
            //    return 0;
            //}
            //var xml = (from item in context.XmlStorages
            //           where item.TableName == XmlStorageTableName && item.EntityId == XmlStorageItemId
            //           select item).FirstOrDefault();
            //if (xml == null)
            //{
            //    var xs = new XmlStorage() { EntityId = XmlStorageItemId, TableName = XmlStorageTableName, XmlData = GetBytes(xmlData) };

            //    context.XmlStorages.AddObject(xs);
            //}


        }
        //internal static XmlStorage GetXmlStorage(Entities context, OracleConnection oracleConnection, string XmlStorageTableName, long XmlStorageItemId)
        //{

        //    var xsss = context.XmlStorages.FirstOrDefault();
        //    var command = new OracleCommand("select * from lims.xml_storage where table_name='" + XmlStorageTableName +
        //                           "'  and entity_id='" + XmlStorageItemId + "'", oracleConnection);
        //    var reader = command.ExecuteScalar();

        //    var x = reader.ToString();

        //    return null;
        //}




        static string GetString(byte[] bytes)
        {
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }



      
    }
}
