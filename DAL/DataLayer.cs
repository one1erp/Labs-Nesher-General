using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Data.Objects.DataClasses;

using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using Common;
using DAL.Logic;

using Oracle.DataAccess.Client;

namespace DAL
{
    public class DataLayer : IDataLayer
    {
        #region Ctor

        internal Entities context;
        private OracleConnection oracleConnection;
        private ConnectionsGenerator generator;

        public static string ConnectionString { get; set; }

        public void Connect()
        {

            generator = new ConnectionsGenerator(Utils.ConString, Utils.NautilusDbConnection);

            //Not used
            // oracleConnection = GetOracleConnection();
            ConnectionString = generator.GetEfConString();
            context = new Entities(ConnectionString);


        }
        public void Connect(string cs)
        {

            context = new Entities(cs);
        }


        /// <summary>
        /// using oracle connection
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetOracleConnection()
        {

            return generator.GetAdoConnection();

        }

        public List<T> FetchDataFromDB<T>(string query, Func<OracleDataReader, T> mapFunc)
        {
            List<T> results = new List<T>();

            #region  Sample calling format

            //    List<SpecificSdgLogRow> rowsFromDB = _dal.FetchDataFromDB(query, reader =>
            //    {
            //        return new SpecificSdgLogRow
            //        {
            //            SdgId = Convert.ToInt32(reader[0]),
            //            Time = Convert.ToDateTime(reader[1]),
            //            Description = reader[2].ToString(),
            //            Info = reader[3].ToString()
            //        };
            //    });

            #endregion

            try
            {
                var oraCon = GetOracleConnection();

                using (OracleCommand command = oraCon.CreateCommand())
                {
                    command.CommandText = query;

                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T item = mapFunc(reader);
                            results.Add(item);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("DB ERROR " + ex.Message);
            }
            return results;

        }

        //For demo only
        public ANALYTE GetAnalyteById(long id)
        {
            var an = BaseLogic.GetEntityByIdentity<ANALYTE>(context, x => x.ANALYTE_ID == id);
            return an;
        }

        public void UpdateDilution(int dilution, long resultId)
        {
            ResultLogic.UpdateDilution(context, dilution, resultId);
        }

        public void AuthorizeSample(long sampleId)
        {
            if (oracleConnection != null)// && oracleConnection.State != ConnectionState.Open)
            {
                oracleConnection = GetOracleConnection();
            }
            BaseLogic.SetAuthorize(oracleConnection, "SAMPLE", sampleId);

        }



        public void AuthorizeSdg(long sdgId)
        {
            if (oracleConnection != null)// || oracleConnection.State != ConnectionState.Open)
            {
                oracleConnection = GetOracleConnection();

            }
            BaseLogic.SetAuthorize(oracleConnection, "SDG", sdgId);
        }

        #endregion

        #region Sdg


        public Sdg GetSdgByName(string name)
        {
            return SdgLogic.GetEntityByIdentity<Sdg>(context, x => x.Name == name);


        }

        public Sdg GetSdgByExternalRef(string externalRef)
        {
            return SdgLogic.GetEntityByIdentity<Sdg>(context, x => x.ExternalReference == externalRef && x.Status == "V");
        }


        public List<Sdg> GetAothorizeSdgsByDate(DateTime d_from, DateTime d_to)
        {
            return SdgLogic.GetAothorizeSdgsByDate(context, d_from, d_to);
        }

        public List<Sdg> GetSdgs()
        {
            return SdgLogic.GetAll<Sdg>(context);
        }

        public ObjectSet<Sdg> GetAllSdgs()
        {
            return SdgLogic.GetAllObjectSet<Sdg>(context);

        }

        //public ObjectSet<STUDY> GetAllstudys()
        //{
        //    return BaseLogic.GetAllObjectSet<STUDY>(context);
        //}

        public List<ObjDetails> GetObjDetailses(string tableName, string condition)
        {
            return BaseLogic.GetObjDetailses(context, tableName, condition);
        }


        public ObjectSet<TEntity> GetAll<TEntity>() where TEntity : EntityObject
        {
            return BaseLogic.GetAllObjectSet<TEntity>(context);
        }

        public Sdg GetSdgTree(long sdgId)
        {
            return SdgLogic.GetSdgTree(context, sdgId);
        }

        public Sdg GetSdgById(long id)
        {

            return SdgLogic.GetEntityByIdentity<Sdg>(context, x => x.SdgId == id);

        }

        #endregion

        #region Sample

        public Sample GetSampleByName(string name)
        {
            return SampleLogic.GetEntityByIdentity<Sample>(context, x => x.Name == name);
        }
        public Workstation getWorkStaitionById(long id)
        {
            return WorkstationLogic.GetEntityByIdentity<Workstation>(context, w => w.WorkstationId == id);
        }
        public List<Sample> GetSamples()
        {
            return BaseLogic.GetAll<Sample>(context);
        }

        public Sample GetSampleByKey(long sampleId)
        {
            return SampleLogic.GetEntityByIdentity<Sample>(context, x => x.SampleId == sampleId);
        }

        #endregion

        #region Test

        public List<Test> GetTests()
        {
            return BaseLogic.GetAll<Test>(context);
        }

        #endregion

        #region Test template logic

        public List<TestTemplateEx> GetTestTemplatesForPriceList()
        {
            return TestTemplateExLogic.GetTestTemplatesForPriceList(context);
        }

        public IQueryable<TestTemplateEx> GetTestTemplatesForPriceListIcludeWorkflow()
        {
            return TestTemplateExLogic.GetTestTemplatesForPriceListIcludeWorkflow(context);
        }

        public List<TestTemplateEx> GetAllTestTemplates()
        {
            return BaseLogic.GetAll<TestTemplateEx>(context);
        }

        #endregion

        #region Client logic

        public List<Client> GetClients()
        {
            return BaseLogic.GetAll<Client>(context);
        }

        public void UpdateClientName(Client client)
        {
            BaseLogic.UpdateRecord(client); // clientLogic = new ClientLogic();
        }

        public List<Client> FindClient()
        {
            return BaseLogic.Find<Client>(context, client => client.Name.StartsWith("a"));
        }
        public U_SESS_OPERATOR HasSessOP(double sid)
        {
            return U_SESS_OPERATORLogic.HasSession(context, sid);

        }
        public Client GetClientByID(double clientID)
        {
            return ClientLogic.GetClientById(context, clientID);
        }
        public Client GetClientByName(string clientName)
        {
            return ClientLogic.GetClientByName(context, clientName);
        }

        public string GetPathOfContractFiles(long clientID)
        {
            return ClientLogic.GetPathOfContractFiles(context, clientID);

        }

        #endregion

        #region Adress

        public List<Address> GetAddresses(string address_Table_Name, long address_Item_ID)
        {
            return AdressLogic.GetAddresses(context, address_Table_Name, address_Item_ID);
        }

        public List<Address> GetAddressesByTable(string table_Name)
        {
            return AdressLogic.GetAddressesByTable(context, table_Name);
        }

        #endregion

        #region סקר חוזה

        public long GetNewContractID()
        {
            //if (oracleConnection != null)
            //{ oracleConnection = GetOracleConnection(); }
            //return ContractLogic.GetNewId(oracleConnection, "SQ_U_CONTRACT");

            return
            GetNewId("SQ_U_CONTRACT");

        }

        public long GetNewContractDataID()
        {

            //if (oracleConnection != null)
            //{ oracleConnection = GetOracleConnection(); }
            //return ContractLogic.GetNewId(oracleConnection, "SQ_U_CONTRACT_DATA");
            return
            GetNewId("SQ_U_CONTRACT_DATA");

        }

        public Contract GetLastContract(long clientID)
        {
            return ContractLogic.GetLastContract(context, clientID);
        }

        public void RefreshContract(Contract contract)
        {

            ContractLogic.Refresh(context, contract);
        }

        public void CancelLastContract(Contract lastContract)
        {
            ContractLogic.CancelLastContract(context, lastContract);
        }

        public void DelteContractData(ContractData contractData)
        {
            ContractLogic.Delte(context, contractData);
        }

        public COA_Report GetCoaReportByName(string coa_name)
        {
            return COA_ReportLogic.GetEntityByIdentity<COA_Report>(context, x => x.Name == coa_name);
        }

        public COA_Report GetCoaReportById(long coaId)
        {
            return COA_ReportLogic.GetEntityByIdentity<COA_Report>(context, x => x.COAReportId == coaId);
        }
        public List<Client> ClinetBySdg(DateTime fromDate, DateTime to, bool isCreatedOn)
        {
            return SdgLogic.ClinetBySdg(context, fromDate, to, isCreatedOn);
        }

        #endregion

        #region phrases



        public PhraseHeader GetPhraseByID(long phraseId)
        {
            return PhraseLogic.GetPhraseByID(context, phraseId);
        }

        public PhraseHeader GetPhraseByName(string phraseHeader)
        {
            return PhraseLogic.GetPhraseByName(context, phraseHeader);
        }

        public List<PhraseHeader> GetAllPhraseHeader()
        {
            return BaseLogic.GetAll<PhraseHeader>(context);
        }

        #endregion

        #region result logic

        public Result IsGoodResultForEntry(int resultId, params char[] goodStatuses)
        {
            return ResultLogic.IsGoodResultForEntry(context, resultId, goodStatuses);
        }



        public int GetResultDuplicates(long? testId, long? templateId)
        {
            return ResultLogic.GetResultDuplicates(context, testId, templateId);
        }

        public Aliquot GetAliquotById(long id)
        {
            return BaseLogic.GetEntityByIdentity<Aliquot>(context, x => x.AliquotId == id);

        }


        #endregion

        #region product logic
        public Product GetProductById(double productId)
        {
            return BaseLogic.Find<Product>(context, p => p.ProductId == productId).FirstOrDefault();
        }
        public List<Product> GetProducts()
        {
            return BaseLogic.GetAll<Product>(context);
        }

        public IQueryable<Product> GetProductsIncludeGroup()
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByType(string type)
        {
            return BaseLogic.Find<Product>(context, p => p.ProductType == type);
        }

        #endregion

        #region Operators

        public ObjectSet<Operator> GetOperators()
        {
            return OperatorLogic.GetAllObjectSet<Operator>(context);
        }

        public Operator GetOPeratorByName(string name)
        {
            return OperatorLogic.GetEntityByIdentity<Operator>(context, x => x.Name == name);
        }

        public IQueryable<Operator> GetOperatorsIncludeALL()
        {
            return OperatorLogic.GetOperatorsIncludeAll(context);
        }



        #endregion

        #region Locations

        public List<Location> GetLocations()
        {
            return BaseLogic.GetAll<Location>(context).ToList();
        }

        #endregion

        public void CancelChanges(object obj)
        {
            context.Refresh(RefreshMode.StoreWins, obj);

        }

        public void SaveChanges()
        {
            if (context != null) context.SaveChanges();
        }

        public bool HasChanges()
        {
            bool changesMade = context.
                ObjectStateManager.
                GetObjectStateEntries(EntityState.Added |
                                      EntityState.Deleted |
                                      EntityState.Modified
                ).Any();
            return changesMade;
        }

        public void Close()
        {
            if (context != null) context.Connection.Close();
        }

        public void Delete(EntityObject obj)
        {
            context.DeleteObject(obj);
        }

        public T Clone<T>(T source) where T : EntityObject
        {
            var obj = new DataContractSerializer(typeof(T));
            using (var stream = new MemoryStream())
            {
                obj.WriteObject(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)obj.ReadObject(stream);
            }
        }

        public void Refresh(RefreshMode refreshMode, object obj)
        {
            context.Refresh(refreshMode, obj);
        }

        public Extension GetExtensionByName(string extensionName)
        {

            return ExtansionLogic.GetExtensionByName(context, extensionName);
        }

        public WorkflowNode GetWorkFlowNodeByID(long workFlowNodeId)
        {
            return WorkflowNodeLogic.GetEntityByIdentity<WorkflowNode>(context, x => x.WorkflowNodeId == workFlowNodeId);
        }

        public Result GetResultByName(string resultName)
        {
            return ResultLogic.GetResultByName(context, resultName);
        }

        public Aliquot GetAliquotByName(string aliquotName)
        {
            return AliquotLogic.GetEntityByIdentity<Aliquot>(context, x => x.Name == aliquotName);
        }

        public Aliquot GetAliquotByNameToLower(string aliquotName)
        {
            return AliquotLogic.GetEntityByIdentity<Aliquot>(context, x => x.Name.ToLower() == aliquotName.ToLower());
        }

        public Aliquot GetParentAliquot(long childId)
        {
            return AliquotLogic.GetParentAliquot(context, childId);
        }

        public IQueryable<Aliquot> GetAliquotByGroup(long groupId)
        {
            return AliquotLogic.FindObjectSet<Aliquot>(context, x => x.GroupId == groupId);
        }

        public IQueryable<Sample> GetSampleByGroup(long groupId)
        {
            return SampleLogic.FindObjectSet<Sample>(context, x => x.GroupId == groupId);

        }

        public Test GetTestByName(string testName)
        {
            return TestLogic.GetTestByName(context, testName);
        }

        public List<Workflow> GetWorkFlows()
        {
            return WorkflowLogic.GetAll<Workflow>(context).ToList();
        }

        public Test GetTestById(long testId)
        {
            return TestLogic.GetTestById(context, testId);
        }

        public string GetTestTemplatesFullNameByDescrpion(string descrpion)
        {
            return TestTemplateExLogic.GetTestTemplatesFullNameByDescrpion(descrpion, context);
        }


        public AliquotTemplate GetAliquotTemplateByWorkfloeNode(WorkflowNode workflowNode)
        {
            return TemplatesLogic.GetAliquotTemplateByWorkfloeNode(context, workflowNode);

        }

        public List<AliquotTemplate> GetAliquotTemplates()
        {
            return BaseLogic.GetAll<AliquotTemplate>(context);
        }

        public long GetNewID(string sequenceName)
        {
            // oracleConnection = generator.GetAdoConnection();
            return BaseLogic.GetNewId(context, sequenceName);
        }

        public LabInfo GetLabInfoByLeter(string descrpion)
        {
            return LabsInfoLogic.GridLayoutByLab(descrpion, context);
        }

        public List<LabInfo> GetLabs()
        {
            return BaseLogic.GetAll<LabInfo>(context);
        }

        public List<Sample> GetSamplesByProduct(Product product)
        {
            return SampleLogic.Find<Sample>(context, s => s.Product.ProductId == product.ProductId);
        }

        public List<Address> TestAddress(long clientId)
        {
            return Logic.AdressLogic.GetAddresses(context, "CLIENT", clientId);
        }

        internal long GetNewId(string sequenceName)
        {
            try
            {
               // Logger.WriteLogFile("enter GetNewId v2", false);


                var res = context.ExecuteStoreQuery<long>("select lims." + sequenceName + ".nextval from dual");

                var neid = res.FirstOrDefault();

                Logger.WriteLogFile(res.ToString(), false);
                return neid;

            }
            catch (Exception ex)
            {
                Logger.WriteLogFile(ex);
                return 0;

            }
        }


        public ReportStation getReportStationByWorksAndType(string workstation, string type)
        {
            return Logic.ReportStationLogic.GetReportStationByWorksAndType(context, workstation, type);
        }

        public Result GetResultById(long resultId)
        {
            return Logic.ResultLogic.GetResultById(context, resultId);
        }

        public CalculationFactor GetCalculationFactor(string tableName, string recordId, string calculationFactorName)
        {
            return CalculationFactorLogic.GetCalculationFactor(context, tableName, recordId, calculationFactorName);
        }

        public List<XmlStorage> GetAllXmlStorage()
        {
            return BaseLogic.GetAll<XmlStorage>(context).Where(x => x.TableName == "CLIENT_SAMPLE_DETAILS" && x.XmlData != null).ToList();

        }

        public void SaveXml(string XmlStorageTableName, long XmlStorageItemId, string xmlData)
        {

            //oracleConnection = GetOracleConnection();
            // XmlStorageLogic.sa(oracleConnection, XmlStorageTableName, XmlStorageItemId, xmlData);
            //            XmlStorageLogic.SaveXml(context, XmlStorageTableName, XmlStorageItemId, xmlData);
        }

        public void AddClientData(U_CLIENT_DATA xmlStorage)
        {
            oracleConnection = GetOracleConnection();
            ClientDataLogic.AddClientData(context, oracleConnection, xmlStorage);
        }

        public List<Operator> GetOperatorsByRole(string roleName)
        {
            return OperatorLogic.GetOperatorsByRole(context, roleName);
        }
        public XmlStorage GetXmlStorage(string XmlStorageTableName, long entityId, long labId)
        {

            return XmlStorageLogic.GetXmlStorage(context, XmlStorageTableName, entityId, labId);
        }

        public void AddXmlStorage(XmlStorage xmlStorage)
        {
            //  oracleConnection = GetOracleConnection();
            XmlStorageLogic.AddXmlStorage(context, oracleConnection, xmlStorage);
        }
        public List<EntitySpecification> GetSpecification(string tableName, long prodactItemId)
        {
            return Logic.SpecificationLogic.GetSpecification(context, tableName, prodactItemId);
        }

        public Contract getContractByClinet(int clinetID)
        {
            return ContractLogic.GetContractByClinet(context, clinetID);
        }

        public List<Sdg> GetSdgByClinet(int clinetID)
        {
            return SdgLogic.GetSdgByClinet(context, clinetID);
        }
        public List<Sdg> GetSdgByClinetNotCancele(int clinetID)
        {
            return SdgLogic.GetSdgByClinetNotCancele(context, clinetID);
        }

        public Contract GetContractByValidity(int clinetID, DateTime? createdOn)
        {
            return ContractLogic.GetContractByValidity(context, clinetID, (DateTime)createdOn);
        }


        public void AddClient(Client client)
        {
            ClientLogic.AddClient(context, client);
        }

        public void AddAddress(Address address)
        {
            AdressLogic.AddAddress(context, address);
        }

        public U_CLIENT_DATA GetClientData(long clientId, long labId)
        {
            return ClientDataLogic.GetClientData(context, clientId, labId);

        }

        public List<Challenge> GetChallenge(string standard, string category, string microbe)
        {
            return ChallengeLogic.GetChallenge(context, standard, category, microbe);
        }

        public List<Challenge> GetChallengeByStandard(string standard, string category)
        {
            return ChallengeLogic.GetChallengeByStandard(context, standard, category);
        }

        public Worksheet GetWorksheetById(long id)
        {

            return BaseLogic.GetEntityByIdentity<Worksheet>(context, x => x.WORKSHEET_ID == id);

        }

        public IQueryable<RACK_ALIQUOT> GetRackAliquots(long rackId)
        {
            return BaseLogic.GetAllObjectSet<RACK_ALIQUOT>(context).Where(x => x.RACK_USAGE_ID == rackId);
        }

        public void CancelOldCoa(string coaName)
        {
            COA_ReportLogic.CancelOldCoa(context, coaName);
        }

        public RACK GetRackById(long id)
        {
            return BaseLogic.GetEntityByIdentity<RACK>(context, x => x.RACK_ID == id);

        }


        public void AddSessOPP(U_SESS_OPERATOR sop)
        {
          //  oracleConnection = GetOracleConnection();
            sop.U_SESS_OPERATOR_ID= GetNewId("SQ_U_SESS_OPERATOR");
            U_SESS_OPERATORLogic.AddSessOPP(context, sop);
        }


      
    }
}

