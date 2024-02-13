using GW_AdminConfig.Models;
using System.Data.SqlClient;
using System.Data;
using GW_Communication.Models;
using System.Transactions;
using Newtonsoft.Json.Linq;
using GW_Business.Models;
using GW_Communication.DB;
using System.Collections.Generic;

namespace GW_AdminConfig.DB
{
    public class LaneDevicesLogic
    {
        private readonly IConfiguration _configuration;

        public LaneDevicesLogic(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetDataUsingSP(string storedProcedureName)
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            SqlCommand command = new SqlCommand(storedProcedureName, connection);
            command.CommandType = System.Data.CommandType.StoredProcedure;
            connection.Open();

            DataTable dt = new DataTable();

            dt.Load(command.ExecuteReader());
            return dt;
        }
        public void InsertRow(string tableName, DbRow row)
        {
            string sql = string.Empty;
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            string fields = string.Join(", ", row.Keys);
            string values = string.Join(", ", row.Keys.Select(fieldName => "@" + fieldName));
            sql = $"INSERT INTO {tableName} ({fields}) VALUES ({values})";

            using (SqlCommand cmd = CreateCommand(connection, sql))
            {
                foreach (var fieldName in row.Keys)
                {
                    if (row[fieldName] != null)
                        cmd.Parameters.AddWithValue("@" + fieldName, row[fieldName]);
                    else
                        cmd.Parameters.AddWithValue("@" + fieldName, DBNull.Value);
                }
                int rowsEffected;
                using (TransactionScope scope = new TransactionScope())
                {
                    rowsEffected = cmd.ExecuteNonQuery();
                    scope.Complete();
                }
            }
        }

        private static SqlCommand CreateCommand(SqlConnection connection, string sql, CommandType type = CommandType.Text)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();

            return new SqlCommand
            {
                CommandText = sql,
                CommandType = type,
                Connection = connection,

            };
        }
        public DataTable GetLaneDevices()
        {
            var connString = _configuration.GetConnectionString("DefaultConnection");

            DataSet ds = new DataSet();
            List<LaneDeviceModel> laneDevices = new List<LaneDeviceModel>();
            const string queryString = "select* from[dbo].[gw_LaneDevices] ";
            using (SqlConnection connection = new(connString))
            {
                SqlCommand command = new(queryString, connection);
                command.CommandText = queryString;

                try
                {
                    connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(ds);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                return ds.Tables[0];
            }
        }
        public List<PathConfigModel> SelectRowsFromTable(string Category, string Name)
        {
            List<PathConfigModel> resultList = new List<PathConfigModel>();

            try
            {
                string sql = $"SELECT * FROM gw_Settings WHERE Category=@Category and Name=@Name";
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.Parameters.AddWithValue("@Category", Category);
                    command.Parameters.AddWithValue("@Name", Name);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PathConfigModel entity = new PathConfigModel();

                            // Assuming YourEntity class has properties corresponding to the columns
                            entity.Category = reader.GetString(reader.GetOrdinal("Category"));
                            entity.Name = reader.GetString(reader.GetOrdinal("Name"));
                            entity.Value= reader.GetString(reader.GetOrdinal("Value"));

                            // Handling null for ExtraField1
                            int extraField1Ordinal = reader.GetOrdinal("ExtraField1");
                            int extraField2Ordinal = reader.GetOrdinal("ExtraField2");
                            entity.ExtraField1 = !reader.IsDBNull(extraField1Ordinal) ? reader.GetString(extraField1Ordinal) : string.Empty;
                            entity.ExtraField2 = !reader.IsDBNull(extraField2Ordinal) ? reader.GetString(extraField2Ordinal) : string.Empty;

                            entity.LastUpdatedBy= reader.GetString(reader.GetOrdinal("LastUpdatedBy"));
                            entity.LastUpdateTime=reader.GetDateTime(reader.GetOrdinal("LastUpdateTime"));
                            // Add other properties as needed
                            resultList.Add(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return resultList;
        }

        public void UpdatePathConfig(PathConfigModel pathConfigModel)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    string sqlUpdate = @"
                    UPDATE dbo.gw_Settings
                    SET 
                        Name = @Name,
                        Value = @Value,
                        ExtraField1 = @ExtraField1,
                        ExtraField2 = @ExtraField2,
                        LastUpdatedBy = @LastUpdatedBy,
                        LastUpdateTime = @LastUpdateTime
                    WHERE 
                        Category = @Category";

                    using (SqlCommand command = new SqlCommand(sqlUpdate, connection))
                    {
                        command.Parameters.AddWithValue("@Name", pathConfigModel.Name);
                        command.Parameters.AddWithValue("@Value", pathConfigModel.Value);
                        command.Parameters.AddWithValue("@ExtraField1", pathConfigModel.ExtraField1);
                        command.Parameters.AddWithValue("@ExtraField2", pathConfigModel.ExtraField2);
                        command.Parameters.AddWithValue("@LastUpdatedBy", "Setup");
                        command.Parameters.AddWithValue("@LastUpdateTime", DateTime.Now);
                        command.Parameters.AddWithValue("@Category", pathConfigModel.Category);

                        int rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}

