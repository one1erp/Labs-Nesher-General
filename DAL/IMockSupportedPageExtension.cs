using DAL;

namespace DAL
{
    public interface IMockSupportedPageExtension
    {
        void InitDal(IDataLayer dal);
        void InitUI();
        void SetExternalData(params string[] data);
        void SaveData();
    }
}