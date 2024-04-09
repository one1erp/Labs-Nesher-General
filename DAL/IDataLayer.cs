using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;
using DAL.Logic;


namespace DAL
{
    public interface IDataLayer
    {


        ObjectSet<TEntity> GetAll<TEntity>() where TEntity : EntityObject;
        Sdg GetSdgTree(long sdgId);

        Sdg GetSdgByExternalRef(string externalRefId);
        Sdg GetSdgById(long id);
        Sdg GetSdgByName(string name);
        List<Sdg> GetAothorizeSdgsByDate(DateTime d_from, DateTime d_to);
        List<Sdg> GetSdgs();
        ObjectSet<Sdg> GetAllSdgs();
     //   ObjectSet<STUDY> GetAllstudys();
        List<ObjDetails> GetObjDetailses(string tableName, string condition);

        Sample GetSampleByName(string name);
        List<Sample> GetSamples();
        Sample GetSampleByKey(long sampleId);
        List<Test> GetTests();
        List<TestTemplateEx> GetTestTemplatesForPriceList();
        IQueryable<TestTemplateEx> GetTestTemplatesForPriceListIcludeWorkflow();
        List<TestTemplateEx> GetAllTestTemplates();
        ObjectSet<Operator> GetOperators();
        Operator GetOPeratorByName(string name);
        IQueryable<Operator> GetOperatorsIncludeALL();
        

        List<Location> GetLocations();
        List<Client> GetClients();
        void UpdateClientName(Client client);
        List<Client> FindClient();
        U_SESS_OPERATOR HasSessOP(double sid);
        Client GetClientByID(double clientID);
        Client GetClientByName(string clientName);
        string GetPathOfContractFiles(long clientID);
        WorkflowNode GetWorkFlowNodeByID(long WorkFlowNodeID);
        Extension GetExtensionByName(string ExtensionName);
        Result GetResultByName(string ResultName);
        Aliquot GetAliquotByName(string aliquotName);
        Aliquot GetAliquotByNameToLower(string lowerCaseAliquotName);
        Aliquot GetParentAliquot(long childId);
        IQueryable<Aliquot> GetAliquotByGroup(long groupId);
        IQueryable<Sample> GetSampleByGroup(long groupId);
        Test GetTestByName(string testName);
        List<Address> GetAddresses(string address_Table_Name, long address_Item_ID);
        List<Address> GetAddressesByTable(string table_Name);
        Result IsGoodResultForEntry(int resultId, params char[] goodStatuses);
        List<Product> GetProducts();
       

        List<Product> GetProductsByType(string type);
        Product GetProductById(double productId);
        int GetResultDuplicates(long? testId, long? templateId);
        Aliquot GetAliquotById(long id);



        #region Common
        void SaveChanges();
        bool HasChanges();
        void Close();
        void Delete(EntityObject obj);
        void Refresh(RefreshMode refreshMode, object obj);
        T Clone<T>(T source) where T : EntityObject;
        void CancelChanges(object entity);
        void Connect();
        void Connect(string cs);
        #endregion

        List<Workflow> GetWorkFlows();
        Test GetTestById(long ResultId);
        string GetTestTemplatesFullNameByDescrpion(string descrpion);
        LabInfo GetLabInfoByLeter(string relevantLab);
        List<LabInfo> GetLabs();
        List<Sample> GetSamplesByProduct(Product product);
        AliquotTemplate GetAliquotTemplateByWorkfloeNode(WorkflowNode workflowNode);
        List<AliquotTemplate> GetAliquotTemplates();
  //      List<ResultTemplate> GetResultTemplate();

        #region Phrases

        PhraseHeader GetPhraseByID(long phraseId);
        PhraseHeader GetPhraseByName(string phraseHeader);
        List<PhraseHeader> GetAllPhraseHeader();

        #endregion

        #region סקר חוזה
        long GetNewID(string sequenceName);
        long GetNewContractID();
        long GetNewContractDataID();
        Contract GetLastContract(long clientID);
        void RefreshContract(Contract contract);
        void CancelLastContract(Contract lastContract);
        void DelteContractData(ContractData contractData);
        #endregion

        #region COA

        COA_Report GetCoaReportByName(string coa_name);
        COA_Report GetCoaReportById(long coaId);



        #endregion
        List<EntitySpecification> GetSpecification(string tableName, long prodactItemId);
        ReportStation getReportStationByWorksAndType(string workstation, string type);
        Workstation getWorkStaitionById(long WorkStaitionId);
        Result GetResultById(long resultId);
        CalculationFactor GetCalculationFactor(string tableName, string recordId, string calculationFactorName);
        #region Xml Storage


        List<XmlStorage> GetAllXmlStorage();
        void SaveXml(string XmlStorageTableName, long XmlStorageItemId, string xmlData);
        XmlStorage GetXmlStorage(string XmlStorageTableName, long XmlStorageItemId, long labId);
        void AddXmlStorage(XmlStorage xmlStorage);
        void AddClient(Client client);
        void AddAddress(Address address);


        U_CLIENT_DATA GetClientData(long clientId, long labId);
        void AddClientData(U_CLIENT_DATA cd);
        #endregion
        void AddSessOPP(U_SESS_OPERATOR sop);

        List<Operator> GetOperatorsByRole(string roleName);
        List<Client> ClinetBySdg(DateTime fromDate, DateTime to, bool isCreatedOn);

        Contract getContractByClinet(int clinetID);
        List<Sdg> GetSdgByClinet(int clinetID);
        List<Sdg> GetSdgByClinetNotCancele(int clinetID);
        Contract GetContractByValidity(int clinetID, DateTime? createdOn);

        List<Challenge> GetChallenge(string standard, string category, string microbe);
        List<Challenge> GetChallengeByStandard(string standard, string category);

        Worksheet GetWorksheetById(long id);
        RACK GetRackById(long id);
        IQueryable<RACK_ALIQUOT> GetRackAliquots(long rackId);
        void CancelOldCoa(string coaName);

        //For demo only
        ANALYTE GetAnalyteById(long id);


        void UpdateDilution(int dilution, long resultId);

        void AuthorizeSample(long sampleId);

        void AuthorizeSdg(long sdgId);
    }
}
