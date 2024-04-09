using System;
using System.Linq;
using System.IO;
using Common;


namespace DAL.Logic
{
    internal class ClientLogic : BaseLogic
    {


        internal static void AddClient(Entities context, Client client)
        {
            
             context.Clients.AddObject(client);
        }
        internal static Client GetClientById(Entities context, double clientID)
        {
            return context.Clients.SingleOrDefault(x => x.ClientId == clientID);
        }
        internal static Client GetClientByName(Entities context, string clientName)
        {
            return context.Clients.SingleOrDefault(x => x.Name == clientName);
        }


        internal static string GetPathOfContractFiles(Entities context, long clientId)
        {
            try
            {

                var phraseH = PhraseLogic.GetPhraseByName(context, "Location folders");
                var contractPath = phraseH.PhraseEntries.FirstOrDefault(x => x.PhraseDescription == "Client documents");
                var contractFolder = phraseH.PhraseEntries.FirstOrDefault(x => x.PhraseDescription == "Contract documents");
                var clientContractFolderName = context.Clients.SingleOrDefault(x => x.ClientId == clientId).ContractFileName;
                if (contractFolder != null && !string.IsNullOrEmpty(contractFolder.PhraseName) && !string.IsNullOrEmpty(clientContractFolderName))
                {
                    var ClientPath = Path.Combine(contractFolder.PhraseName, clientContractFolderName.MakeSafeFilename(' ').TrimEnd(' '));
                    return ClientPath;
                }
            }
            catch (Exception ex)
            {

                Logger.WriteLogFile(ex);
                return null;

            }
            return null;

        }
    }
}