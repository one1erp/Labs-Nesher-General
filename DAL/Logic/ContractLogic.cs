using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Linq.Expressions;

//using Oracle.DataAccess.Client;

namespace DAL.Logic
{
    internal class ContractLogic : BaseLogic
    {

  
        internal static Contract GetLastContract(Entities context, long clientId)
        {
            Contract lastContract = (from item in context.Contracts.Include
                                         ("ContractDatas").OrderByDescending(x => x.ConfirmDate)
                                     where item.ClientId == clientId
                                     select item).FirstOrDefault();
            return lastContract;
        }
        internal static Contract GetContractByValidity(Entities context, long clientId,DateTime ? sdgDate)
        {
            var lastContract = (from item in context.Contracts.Include
                                    ("ContractDatas").OrderByDescending(x => x.ContractId)
                                where item.ClientId == clientId
                                select item).ToList();
            foreach (Contract contract in lastContract)
            {
                if (sdgDate >= contract.ConfirmDate) return contract;
            }
            return null;
        }
        internal static void Detach(Entities context, EntityObject obj)
        {
            context.DeleteObject(obj);
        }

        internal static void Refresh(Entities context, Contract contract)
        {
            context.Refresh(RefreshMode.StoreWins, contract.ContractDatas);
   
        }

        internal static void CancelLastContract(Entities context, Contract lastContract)
        {
            //הפונקציה חייבת להיות בסדר הזה


            //Restore contract datas
            foreach (ContractData contractData in lastContract.ContractDatas.ToList())
            {
                context.ObjectStateManager.ChangeObjectState(contractData,
                                                             contractData.EntityState == EntityState.Added
                                                                 ? EntityState.Detached
                                                                 : EntityState.Unchanged);
            }
            //restore contract
            switch (lastContract.EntityState)
            {
                case EntityState.Added:
                    context.ObjectStateManager.ChangeObjectState(lastContract, EntityState.Detached);
                    break;
                case EntityState.Modified:
                    context.ObjectStateManager.ChangeObjectState(lastContract, EntityState.Unchanged);
                    break;
            }

            //Restore deleted object
            IEnumerable<ObjectStateEntry> deletdObject =
                context.ObjectStateManager.GetObjectStateEntries(EntityState.Deleted);
            foreach (ObjectStateEntry objectStateEntry in deletdObject)
            {
                objectStateEntry.ChangeState(EntityState.Unchanged);
            }
        }

        internal static void Delte(Entities context, ContractData cd)
        {
         
            context.ObjectStateManager.ChangeObjectState(cd, EntityState.Deleted);
        }

        public static Contract GetContractByClinet(Entities context, int clinetID)
        {
            Contract contract = (from item in context.Contracts
                                 where item.ClientId == clinetID
                                 select item).FirstOrDefault();
            return contract;
        }
    }
}