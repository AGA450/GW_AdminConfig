using GW_AdminConfig.Models;

namespace GW_AdminConfig.Repository.Interface
{
    public interface IPathConfigSettings
    {
        IEnumerable<PathConfigModel> GetAllSettings(string Category);
        IEnumerable<PathConfigModel> GetSettingsByParams(string Category, string Name);
        int AddConfigSettings(PathConfigModel laneDeviceModel);
        int UpdateSettings(PathConfigModel laneDeviceModel);
        void DeleteSettings(int id);
    }
}
