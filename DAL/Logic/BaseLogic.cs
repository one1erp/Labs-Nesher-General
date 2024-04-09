using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Common;
using Oracle.DataAccess.Client;



namespace DAL.Logic
{
    internal class BaseLogic
    {
        internal static ObjectSet<TEntity> GetAllObjectSet<TEntity>(Entities context) where TEntity : EntityObject
        {

            return context.CreateObjectSet<TEntity>();

        }
        public static ObjectQuery<TEntity> GetAllInclude<TEntity>(Entities context, string tableToInclude) where TEntity : EntityObject
        {
            var query = context.CreateObjectSet<TEntity>().Include(tableToInclude);
            return query;
        }
        internal static List<TEntity> GetAll<TEntity>(Entities context) where TEntity : EntityObject
        {

            return context.CreateObjectSet<TEntity>().ToList<TEntity>();
        }

        public static IQueryable<TEntity> FindObjectSet<TEntity>(Entities context, Expression<Func<TEntity, bool>> func) where TEntity : EntityObject
        {
            var query = context.CreateObjectSet<TEntity>().Where(func);
            return query;
        }
        public static List<TEntity> Find<TEntity>(Entities context, Expression<Func<TEntity, bool>> func) where TEntity : EntityObject
        {
            var query = context.CreateObjectSet<TEntity>().Where(func);
            return query.ToList();
        }

        public static IQueryable<TEntity> FindInclude<TEntity>(Entities context, Expression<Func<TEntity, bool>> func, string tableToInclude) where TEntity : EntityObject
        {
            var query = context.CreateObjectSet<TEntity>().Include(tableToInclude).Where(func);
            return query;
        }

        public static bool UpdateRecord(EntityObject record)
        {
            try
            {
                EntityKey key;
                object originalItem;

                using (var ctx = new Entities(DataLayer.ConnectionString))
                {
                    key = ctx.CreateEntityKey(record.EntityKey.EntitySetName, record);

                    if (ctx.TryGetObjectByKey(key, out originalItem))
                    {
                        ctx.ApplyCurrentValues(key.EntitySetName, record);
                    }

                    ctx.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);

                return false;
            }
        }

        public static T GetEntityByIdentity<T>(Entities context, Expression<Func<T, bool>> expression) where T : EntityObject
        {
            try
            {
                return context.CreateObjectSet<T>().SingleOrDefault(expression);
            }
            catch (Exception ex)
            {

                Logger.WriteLogFile(ex);

            }

            return context.CreateObjectSet<T>().SingleOrDefault(expression);
        }

        //old 
        public static long GetNewId(Entities context, string sequenceName)
        {
            try
            {
                MessageBox.Show("GetNewId old");
                var newId = context.ExecuteStoreQuery<long>("select lims." + sequenceName + ".nextval from dual");
                return newId.SingleOrDefault();
            }
            catch (Exception ex)
            {

                Logger.WriteLogFile(ex);
                return 0;
            }
        }

        public static long GetNewId(OracleConnection oracleConnection, string sequenceName)
        {
            try
            {
        

                OracleCommand command = new OracleCommand("select lims." + sequenceName + ".nextval from dual", oracleConnection);

                OracleDataReader reader = command.ExecuteReader();
                string id = null;
                long lid = 0;


                while (reader.Read())
                {
                    id = reader[0].ToString();

                }

                long.TryParse(id, out lid);
                Logger.WriteLogFile(sequenceName + "is " + lid, false);

                return lid;

            }
            catch (Exception ex)
            {

                Logger.WriteLogFile(ex);
                return 0;
            }
        }


        internal static List<ObjDetails> GetObjDetailses(Entities context, string tableName, string condition)
        {
            try
            {


                if (!string.IsNullOrEmpty(condition))
                {
                    condition = "where " + condition;
                }
                var q = context.ExecuteStoreQuery<ObjDetails>("Select " + tableName + "_Id as Id,name from lims_sys." + tableName + "  " + condition, null).ToList();
                return q;

            }
            catch (Exception e)
            {
                Logger.WriteLogFile(e);
                return null;
            }

        }

        public static void SetAuthorize(OracleConnection oracleConnection, string tableName, long ID)
        {
            OracleCommand command = null;
            int res = -99;
            try
            {


                string q = string.Format("update  lims.sys.{0} set status='A' where {0}_id ='{1}'", tableName, ID);
                command = new OracleCommand(q, oracleConnection);

                res = command.ExecuteNonQuery();




            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);
                MessageBox.Show("Error " + res + " " + ex.Message);


            }
            finally
            {
                if (command != null) command.Dispose();
            }
        }
    }


}
