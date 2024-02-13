using System.Data.SqlClient;
using System.Data;
using GW_Util.Log;
using System.Xml.Linq;

namespace GW_AdminConfig.DBHelpers
{
    public class DBHelper
    {
        private  readonly IConfiguration _configuration;
        public DBHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  DataTable GetDataUsingSP(string storedProcedureName, Dictionary<string, object> inputParameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
                SqlCommand command = new SqlCommand(storedProcedureName, connection);
                command.CommandType = CommandType.StoredProcedure;
                // Add input parameters
                foreach (var inputParam in inputParameters)
                {
                    command.Parameters.AddWithValue("@" + inputParam.Key, inputParam.Value ?? DBNull.Value);
                }
                connection.Open();

                DataTable dt = new DataTable();
                dt.Load(command.ExecuteReader());
                return dt;
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public DataTable ExecuteStoredProcedure(string storedProcedureName, Dictionary<string, object> inputParameters, out Dictionary<string, object> outputParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters
                        foreach (var inputParam in inputParameters)
                        {
                            command.Parameters.AddWithValue("@" + inputParam.Key, inputParam.Value ?? DBNull.Value);
                        }

                        // Add output parameters
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                param.Value = DBNull.Value;
                            }
                        }

                        // Execute the stored procedure
                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            dataAdapter.Fill(dataTable);

                            // Retrieve output parameters
                            outputParameters = new Dictionary<string, object>();
                            foreach (SqlParameter param in command.Parameters)
                            {
                                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                                {
                                    outputParameters.Add(param.ParameterName, param.Value);
                                }
                            }

                            return dataTable;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }
        public DataTable GetDataByParams(string storedProcedureName, Dictionary<string, object> parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        foreach (var kv in parameters)
                        {
                            command.Parameters.AddWithValue("@" + kv.Key, kv.Value);
                        }
                        DataTable dt = new DataTable();
                        dt.Load(command.ExecuteReader());
                        return dt;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int InsertRow(string storedProcedureName, Dictionary<string, object> inputParameters, out Dictionary<string, object> outputParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters for insertion
                        foreach (var inputParam in inputParameters)
                        {
                            command.Parameters.AddWithValue("@" + inputParam.Key, inputParam.Value ?? DBNull.Value);
                        }

                        // Add output parameters
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                param.Value = DBNull.Value;
                            }
                        }

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();

                        // Retrieve output parameters
                        outputParameters = new Dictionary<string, object>();
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                outputParameters.Add(param.ParameterName, param.Value);
                            }
                        }
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int UpdateRow(string storedProcedureName, Dictionary<string, object> inputParameters, out Dictionary<string, object> outputParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters for update
                        foreach (var inputParam in inputParameters)
                        {
                            command.Parameters.AddWithValue("@" + inputParam.Key, inputParam.Value ?? DBNull.Value);
                        }

                        // Add output parameters
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                param.Value = DBNull.Value;
                            }
                        }

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();

                        // Retrieve output parameters
                        outputParameters = new Dictionary<string, object>();
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                outputParameters.Add(param.ParameterName, param.Value);
                            }
                        }
                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

        public int DeleteRow(string storedProcedureName, Dictionary<string, object> inputParameters, out Dictionary<string, object> outputParameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add input parameters for deletion
                        foreach (var inputParam in inputParameters)
                        {
                            command.Parameters.AddWithValue("@" + inputParam.Key, inputParam.Value ?? DBNull.Value);
                        }

                        // Add output parameters
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                param.Value = DBNull.Value;
                            }
                        }

                        // Execute the stored procedure
                        int rowsAffected = command.ExecuteNonQuery();

                        // Retrieve output parameters
                        outputParameters = new Dictionary<string, object>();
                        foreach (SqlParameter param in command.Parameters)
                        {
                            if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                            {
                                outputParameters.Add(param.ParameterName, param.Value);
                            }
                        }

                        return rowsAffected;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.LogError(ex);
                throw;
            }
        }

    }
}
