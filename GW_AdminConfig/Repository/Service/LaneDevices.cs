using GW_AdminConfig.DBHelpers;
using GW_AdminConfig.Repository.Interface;
using GW_Business.Core;
using GW_Util.Log;
using System.Collections.Generic;
using System.Data;

namespace GW_AdminConfig.Repository.Service
{
    public class LaneDevices : ILaneDevices
    {
        private readonly IConfiguration _configuration;
        public LaneDevices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void AddLaneDevices(LaneDeviceModel laneDeviceModel)
        {
            throw new NotImplementedException();
        }

        public void DeleteLaneDevices(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<LaneDeviceModel> GetAllLaneDevices()
        {
            try
            {
                DBHelper dBHelper = new DBHelper(_configuration);
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                DataTable dtLaneDevices = dBHelper.GetDataUsingSP("usp_GW_LaneDevices", parameters);
                IEnumerable<LaneDeviceModel> laneDevices = dtLaneDevices.AsEnumerable().Select(row => new LaneDeviceModel
                {
                    DeviceId = Convert.ToInt32(row["DeviceId"]),
                    LaneId = row["LaneId"]?.ToString() ?? string.Empty,
                    Type = row["Type"].ToString() ?? string.Empty,
                    SubType = row["SubType"].ToString() ?? string.Empty,
                    Brand = row["Brand"].ToString() ?? string.Empty,
                    Model = row["Model"].ToString() ?? string.Empty,
                    IPAddress = row["IPAddress"].ToString() ?? string.Empty,
                    Port = Convert.ToInt32(row["Port"]),
                    ExtraField1 = row["ExtraField1"].ToString() ?? string.Empty,
                    ExtraField2 = row["ExtraField2"].ToString() ?? string.Empty,
                });
                return laneDevices;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public IEnumerable<LaneDeviceModel> GetLaneDevicesById(int DeviceId)
        {
            DBHelper dBHelper = new DBHelper(_configuration);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("DeviceId", DeviceId);
            
            DataTable dtLaneDevices = dBHelper.GetDataByParams("usp_GW_GetLaneDevices", parameters);
            DataRow? row = dtLaneDevices.Rows.Count > 0 ? dtLaneDevices.Rows[0] : null;
            IEnumerable<LaneDeviceModel> laneDevices = dtLaneDevices.AsEnumerable().Select(row => new LaneDeviceModel
            {
                DeviceId = Convert.ToInt32(row["DeviceId"]),
                LaneId = row["LaneId"]?.ToString() ?? string.Empty,
                Type = row["Type"].ToString() ?? string.Empty,
                SubType = row["SubType"].ToString() ?? string.Empty,
                Brand = row["Brand"].ToString() ?? string.Empty,
                Model = row["Model"].ToString() ?? string.Empty,
                IPAddress = row["IPAddress"].ToString() ?? string.Empty,
                Port = Convert.ToInt32(row["Port"]),
                ExtraField1 = row["ExtraField1"].ToString() ?? string.Empty,
                ExtraField2 = row["ExtraField2"].ToString() ?? string.Empty,
            });
            return laneDevices;
        }

        public void UpdateLaneDevices(LaneDeviceModel laneDeviceModel)
        {
            throw new NotImplementedException();
        }
    }
}
