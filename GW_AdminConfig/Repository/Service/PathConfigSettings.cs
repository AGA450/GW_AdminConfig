using GW_AdminConfig.DBHelpers;
using GW_AdminConfig.Models;
using GW_AdminConfig.Repository.Interface;
using GW_Business.Models;
using GW_Util.Log;
using NuGet.Configuration;
using System.Data;
using System.Xml.Linq;

namespace GW_AdminConfig.Repository.Service
{
    public class PathConfigSettings : IPathConfigSettings
    {
        private readonly IConfiguration _configuration;
        public PathConfigSettings(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int AddConfigSettings(PathConfigModel pathConfigModel)
        {
            DBHelper dBHelper = new DBHelper(_configuration);
            Dictionary<string, object> inputParams = new Dictionary<string, object>
            {
                { "Category", pathConfigModel.Category },
                { "Name", pathConfigModel.Name },
                { "Value", pathConfigModel.Value},
                { "ExtraField1", pathConfigModel.ExtraField1 },
                { "ExtraField2", pathConfigModel.ExtraField2 },
                { "LastUpdatedBy", pathConfigModel.LastUpdatedBy },
                // Add more input parameters as needed
            };
            Dictionary<string, object> outputParams;
            int rowsAffected = dBHelper.InsertRow("usp_GW_InsertConfigSettings", inputParams, out outputParams);
            return rowsAffected;
        }

        public void DeleteSettings(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PathConfigModel> GetAllSettings(string category)
        {
            try
            {
                DBHelper dBHelper = new DBHelper(_configuration);
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                parameters.Add("Category", category);
                DataTable dtPathConfigSettings = dBHelper.GetDataUsingSP("usp_GW_PathSettings", parameters);
                IEnumerable<PathConfigModel> laneDevices = dtPathConfigSettings.AsEnumerable().Select(row => new PathConfigModel
                {
                    Category = Convert.ToString(row["Category"]) ?? string.Empty,
                    Name = Convert.ToString(row["Name"]) ?? string.Empty,
                    Value = row["Value"].ToString() ?? string.Empty,
                    ExtraField1 = row["ExtraField1"].ToString() ?? string.Empty,
                    ExtraField2 = row["ExtraField2"].ToString() ?? string.Empty,
                    Description = row["Description"].ToString() ?? string.Empty,
                    LastUpdatedBy = row["LastUpdatedBy"].ToString() ?? string.Empty,
                    LastUpdateTime = Convert.ToDateTime(row["LastUpdateTime"]),
                });
                return laneDevices;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public IEnumerable<PathConfigModel> GetSettingsByParams(string Category, string Name)
        {
            DBHelper dBHelper = new DBHelper(_configuration);
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("Category", Category);
            parameters.Add("Name", Name);
            DataTable dtPathConfigSettings = dBHelper.GetDataByParams("usp_GW_GetPathSettings", parameters);
            DataRow? row = dtPathConfigSettings.Rows.Count > 0 ? dtPathConfigSettings.Rows[0] : null;
            IEnumerable<PathConfigModel> laneDevices = dtPathConfigSettings.AsEnumerable().Select(row => new PathConfigModel
            {
                Category = Convert.ToString(row["Category"]) ?? string.Empty,
                Name = Convert.ToString(row["Name"]) ?? string.Empty,
                Value = row["Value"].ToString() ?? string.Empty,
                ExtraField1 = row["ExtraField1"].ToString() ?? string.Empty,
                ExtraField2 = row["ExtraField2"].ToString() ?? string.Empty,
                LastUpdatedBy = row["LastUpdatedBy"].ToString() ?? string.Empty,
                LastUpdateTime = Convert.ToDateTime(row["LastUpdateTime"]),
            });
            return laneDevices;
        }

        public int UpdateSettings(PathConfigModel pathConfigModel)
        {
            DBHelper dBHelper = new DBHelper(_configuration);
            Dictionary<string, object> inputParams = new Dictionary<string, object>
            {
                { "Category", pathConfigModel.Category },
                { "Name", pathConfigModel.Name },
                { "Value", pathConfigModel.Value},
                { "ExtraField1", pathConfigModel.ExtraField1 },
                { "ExtraField2", pathConfigModel.ExtraField2 },
                { "LastUpdatedBy", pathConfigModel.LastUpdatedBy },
                // Add more input parameters as needed
            };
            Dictionary<string, object> outputParams;
            int rowsAffected = dBHelper.UpdateRow("usp_GW_UpdateConfigSettings", inputParams, out outputParams);
            return rowsAffected;
        }
    }
}
