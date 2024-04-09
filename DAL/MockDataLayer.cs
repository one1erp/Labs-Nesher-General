using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
//using Common;
using System.Linq;
using DAL.Logic;

namespace DAL
{
    public class MockDataLayer : IDataLayer
    {



        private Entities context;

        private DataLayer dal;

        public void Connect()
        {

            //DB גישה ישירה ל 
            //בכדי לאפשר להריץ את התוכנית ללא פתיחת הנאוטילוס
            var tstCS =
                "metadata=res://*/NautilusModel1.csdl|res://*/NautilusModel1.ssdl|res://*/NautilusModel1.msl;provider=Oracle.DataAccess.Client;provider connection string='Data Source=microb;User ID=lims_sys;Password=lims_sys'";

            var neshdevCs =
                "metadata=res://*/NautilusModel1.csdl|res://*/NautilusModel1.ssdl|res://*/NautilusModel1.msl;provider=Oracle.DataAccess.Client;provider connection string='Data Source=neshdev1;User ID=lims_sys;Password=lims_sys'";

            var neshdev1New =
                "metadata=res://*/NautilusModel1.csdl|res://*/NautilusModel1.ssdl|res://*/NautilusModel1.msl;provider=Oracle.DataAccess.Client;provider connection string='DATA SOURCE=neshdev1;PASSWORD=lims;PERSIST SECURITY INFO=True;USER ID=LIMS'";

            var nesherProd =
                 "metadata=res://*/NautilusModel1.csdl|res://*/NautilusModel1.ssdl|res://*/NautilusModel1.msl;provider=Oracle.DataAccess.Client;provider connection string='DATA SOURCE=MICNAUT;PASSWORD=lims;PERSIST SECURITY INFO=True;USER ID=LIMS'";


            //lims_sys בינתיים אנחנו משתמשים בזה עמ לאפשר גישה גם למשתמש שאינו 

            context = new Entities(tstCS);

            dal = new DataLayer();
            dal.context = context;
        }
        public void Connect(string cs)
        {

            context = new Entities(cs);
        }


        public ObjectSet<TEntity> GetAll<TEntity>() where TEntity : EntityObject
        {
            return dal.GetAll<TEntity>();
        }
        public Sdg GetSdgByExternalRef(string externalRef)
        {
            return SdgLogic.GetEntityByIdentity<Sdg>(context, x => x.ExternalReference == externalRef && x.Status == "V");
        }
        public Sdg GetSdgTree(long sdgId)
        {
            return dal.GetSdgTree(sdgId);
        }

        public Sdg GetSdgById(long id)
        {
            return dal.GetSdgById(id);
        }
        public Sdg GetSdgByName(string name)
        {
            return dal.GetSdgByName(name);
        }

        public List<Sdg> GetAothorizeSdgsByDate(DateTime d_from, DateTime d_to)
        {
            return dal.GetAothorizeSdgsByDate(d_from, d_to);
        }

        public List<Sdg> GetSdgs()
        {
            return dal.GetSdgs();
        }

        public ObjectSet<Sdg> GetAllSdgs()
        {

            return dal.GetAllSdgs();
        }

        //public ObjectSet<STUDY> GetAllstudys()
        //{
        //    throw new NotImplementedException();
        //}

        public List<ObjDetails> GetObjDetailses(string tableName, string condition)
        {

            return dal.GetObjDetailses(tableName, condition);

        }



        public Sample GetSampleByName(string name)
        {
            return dal.GetSampleByName(name);
        }
        public List<Sample> GetSamples()
        {
            return dal.GetSamples();
        }
        public List<Test> GetTests()
        {
            return dal.GetTests();
        }
        public List<TestTemplateEx> GetTestTemplatesForPriceList()
        {
            return dal.GetTestTemplatesForPriceList();
        }

        public IQueryable<TestTemplateEx> GetTestTemplatesForPriceListIcludeWorkflow()
        {
            return dal.GetTestTemplatesForPriceListIcludeWorkflow();
        }

        public List<TestTemplateEx> GetAllTestTemplates()
        {
            return dal.GetAllTestTemplates();
        }
        public List<Client> GetClients()
        {
            return dal.GetClients();
        }
        public void UpdateClientName(Client client)
        {
            dal.UpdateClientName(client);
        }
        public List<Client> FindClient()
        {
            return dal.FindClient();
        }
        public Client GetClientByID(double clientID)
        {
            return dal.GetClientByID(clientID);
        }
        public Client GetClientByName(string clientName)
        {
            return dal.GetClientByName(clientName);
        }

        public string GetPathOfContractFiles(long clientID)
        {
            return ClientLogic.GetPathOfContractFiles(context, clientID);
        }

        public Extension GetExtensionByName(string ExtensionName)
        {
            return dal.GetExtensionByName(ExtensionName);
        }
        public WorkflowNode GetWorkFlowNodeByID(long WorkFlowNodeID)
        {
            return dal.GetWorkFlowNodeByID(WorkFlowNodeID);
        }
        public Result GetResultByName(string ResultName)
        {
            return dal.GetResultByName(ResultName);
        }
        public Aliquot GetAliquotByName(string aliquotName)
        {
            return dal.GetAliquotByName(aliquotName);
        }
        public Aliquot GetAliquotByNameToLower(string aliquotName)
        {
            return dal.GetAliquotByName(aliquotName);
        }

        public Aliquot GetParentAliquot(long childId)
        {
            return dal.GetParentAliquot(childId);
        }

        public IQueryable<Aliquot> GetAliquotByGroup(long groupId)
        {
            return dal.GetAliquotByGroup(groupId);
        }

        public IQueryable<Sample> GetSampleByGroup(long groupId)
        {
            return dal.GetSampleByGroup(groupId);
        }

        public Test GetTestByName(string testName)
        {
            return dal.GetTestByName(testName);
        }
        public List<Address> GetAddresses(string address_Table_Name, long address_Item_ID)
        {
            return dal.GetAddresses(address_Table_Name, address_Item_ID);
        }

        public List<Address> GetAddressesByTable(string table_Name)
        {
            return dal.GetAddressesByTable(table_Name);
        }

        public List<ResultTemplate> GetResultTemplate()
        {
            throw new NotImplementedException();
        }

        public PhraseHeader GetPhraseByID(long phraseId)
        {
            return dal.GetPhraseByID(phraseId);

        }
        public PhraseHeader GetPhraseByName(string phraseHeader)
        {
            return dal.GetPhraseByName(phraseHeader);
        }
        public List<PhraseHeader> GetAllPhraseHeader()
        {
            return dal.GetAllPhraseHeader();
        }
        public Result IsGoodResultForEntry(int resultId, params char[] goodStatuses)
        {
            return dal.IsGoodResultForEntry(resultId, goodStatuses);
        }
        public List<Product> GetProducts()
        {
            return dal.GetProducts();
        }

        public IQueryable<Product> GetProductsIncludeGroup()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByType(string type)
        {
            return dal.GetProductsByType(type);
        }

        public Product GetProductById(double productId)
        {
            return dal.GetProductById(productId);
        }
        public int GetResultDuplicates(long? testId, long? templateId)
        {
            return dal.GetResultDuplicates(testId, templateId);
        }

        public Aliquot GetAliquotById(long id)
        {
            return dal.GetAliquotById(id);
        }

        public void AddClientData(U_CLIENT_DATA xmlStorage)
        {
            throw new NotImplementedException();
        }
        public U_SESS_OPERATOR HasSessOP(double sid)
        {
            return U_SESS_OPERATORLogic.HasSession(context, sid);

        }
        public void AddSessOPP(U_SESS_OPERATOR sop)
        {
            throw new NotImplementedException();
        }
        public List<Operator> GetOperatorsByRole(string roleName)
        {
            return dal.GetOperatorsByRole(roleName);
        }

        public List<Client> ClinetBySdg(DateTime fromDate, DateTime to, bool isCreatedOn)
        {
            return dal.ClinetBySdg(fromDate, to, isCreatedOn);
        }

        public Contract getContractByClinet(int clinetID)
        {
            return dal.getContractByClinet(clinetID);
        }

        public List<Sdg> GetSdgByClinet(int clinetID)
        {
            return dal.GetSdgByClinet(clinetID);
        }

        public List<Sdg> GetSdgByClinetNotCancele(int clinetID)
        {
            return dal.GetSdgByClinetNotCancele(clinetID);
        }

        public Contract GetContractByValidity(int clinetID, DateTime? createdOn)
        {
            return dal.GetContractByValidity(clinetID, createdOn);
        }

        #region Operators

        public ObjectSet<Operator> GetOperators()
        {
            return dal.GetOperators();
        }

        public Operator GetOPeratorByName(string name)
        {
            return dal.GetOPeratorByName(name);
        }

        public IQueryable<Operator> GetOperatorsIncludeALL()
        {
            return dal.GetOperatorsIncludeALL();
        }



        #endregion
        #region Locations

        public List<Location> GetLocations()
        {
            return dal.GetLocations();
        }

        #endregion
        public void SaveChanges()
        {
            dal.SaveChanges();
        }
        public bool HasChanges()
        {
            return dal.HasChanges();
        }
        public void Close()
        {
            dal.Close();
        }
        public void Delete(EntityObject obj)
        {
            dal.Delete(obj);
        }
        public void Refresh(RefreshMode refreshMode, object obj)
        {
            dal.Refresh(refreshMode, obj);
        }
        public T Clone<T>(T source) where T : EntityObject
        {
            return dal.Clone(source);
        }
        public List<Workflow> GetWorkFlows()
        {
            return dal.GetWorkFlows();
        }
        public Test GetTestById(long ResultId)
        {
            return dal.GetTestById(ResultId);
        }
        public string GetTestTemplatesFullNameByDescrpion(string descrpion)
        {
            return dal.GetTestTemplatesFullNameByDescrpion(descrpion);
        }
        public LabInfo GetLabInfoByLeter(string descrpion)
        {
            return dal.GetLabInfoByLeter(descrpion);
        }
        public List<LabInfo> GetLabs()
        {
            return dal.GetLabs();
        }
        public Sample GetSampleByKey(long sampleId)
        {
            return dal.GetSampleByKey(sampleId);
        }
        public List<Sample> GetSamplesByProduct(Product product)
        {
            return dal.GetSamplesByProduct(product);
        }
        public AliquotTemplate GetAliquotTemplateByWorkfloeNode(WorkflowNode workflowNode)
        {
            return dal.GetAliquotTemplateByWorkfloeNode(workflowNode);
        }
        public List<AliquotTemplate> GetAliquotTemplates()
        {
            return dal.GetAliquotTemplates();
        }
        public long GetNewID(string sequenceName)
        {
            return dal.GetNewID(sequenceName);
        }
        public long GetNewContractID()
        {
            return dal.GetNewContractID();
        }
        public long GetNewContractDataID()
        {
            return dal.GetNewContractDataID();
        }
        public Contract GetLastContract(long clientID)
        {
            return dal.GetLastContract(clientID);
        }
        public void RefreshContract(Contract contract)
        {
            dal.RefreshContract(contract);
        }
        public void CancelLastContract(Contract lastContract)
        {
            dal.CancelLastContract(lastContract);
        }
        public void DelteContractData(ContractData contractData)
        {
            dal.DelteContractData(contractData);
        }
        public COA_Report GetCoaReportByName(string coa_name)
        {
            return dal.GetCoaReportByName(coa_name);
        }

        public COA_Report GetCoaReportById(long coaId)
        {
            return dal.GetCoaReportById(coaId);
        }

        public ReportStation getReportStationByWorksAndType(string workstation, string type)
        {
            return dal.getReportStationByWorksAndType(workstation, type);
        }
        public Workstation getWorkStaitionById(long testId)
        {
            return dal.getWorkStaitionById(testId);
        }
        public Result GetResultById(long resultId)
        {
            return dal.GetResultById(resultId);
        }

        public CalculationFactor GetCalculationFactor(string tableName, string recordId, string calculationFactorName)
        {
            throw new NotImplementedException();
        }

        public List<XmlStorage> GetAllXmlStorage()
        {
        return    dal.GetAllXmlStorage();
        }

        public void SaveXml(string XmlStorageTableName, long XmlStorageItemId, string xmlData)
        {
            dal.SaveXml(XmlStorageTableName, XmlStorageItemId, xmlData);
        }

        public XmlStorage GetXmlStorage(string XmlStorageTableName, long XmlStorageItemId, long labId)
        {
            return dal.GetXmlStorage(XmlStorageTableName, XmlStorageItemId, labId);
        }

        public void AddXmlStorage(XmlStorage xmlStorage)
        {
            dal.AddXmlStorage(xmlStorage);
        }

        public void CancelChanges(object entity)
        {
            dal.CancelChanges(entity);
        }
        public List<EntitySpecification> GetSpecification(string tableName, long prodactItemId)
        {
            return dal.GetSpecification(tableName, prodactItemId);
        }



        public void AddClient(Client xmlStorage)
        {

        }

        public void AddAddress(Address address)
        {
            dal.AddAddress(address);
        }

        public U_CLIENT_DATA GetClientData(long clientId, long labId)
        {
            throw new NotImplementedException();
        }


        public List<Challenge> GetChallenge(string standard, string category, string microbe)
        {
            throw new NotImplementedException();
        }

        public List<Challenge> GetChallengeByStandard(string standard, string category)
        {
            throw new NotImplementedException();
        }

        public Worksheet GetWorksheetById(long id)
        {
            return dal.GetWorksheetById(id);
        }

        public RACK GetRackById(long id)
        {
            return null;
        }

        public IQueryable<RACK_ALIQUOT> GetRackAliquots(long rackId)
        {
            throw new NotImplementedException();
        }

        public void CancelOldCoa(string coaName)
        {
            throw new NotImplementedException();
        }



        //For demo only

        public ANALYTE GetAnalyteById(long id)
        {
            return dal.GetAnalyteById(id);
        }

        public void UpdateDilution(int dilution, long resultId)
        {
            dal.UpdateDilution(dilution, resultId);
        }

        public void AuthorizeSample(long p)
        {
          //  throw new NotImplementedException();
        }

        public void AuthorizeSdg(long sdgId)
        {
      //      throw new NotImplementedException();
        }

    }
}