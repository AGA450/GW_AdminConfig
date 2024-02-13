namespace GW_AdminConfig.Repository.Interface
{
    public interface ILaneDevices
    {
        IEnumerable<LaneDeviceModel> GetAllLaneDevices();
        IEnumerable<LaneDeviceModel> GetLaneDevicesById(int DeviceId);
        void AddLaneDevices(LaneDeviceModel laneDeviceModel);
        void UpdateLaneDevices(LaneDeviceModel laneDeviceModel);
        void DeleteLaneDevices(int id);
    }
}
