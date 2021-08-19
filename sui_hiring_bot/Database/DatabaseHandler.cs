using System;
using System.Collections.Generic;
using MySqlConnector;

namespace sui_hiring_bot
{
    public class DatabaseHandler
    {
        public static bool ExecuteNonQuery(string sqlCommand)
        {
            MySqlConnection client = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"));
            client.Open();
            var command = new MySqlCommand(sqlCommand, client);
            try
            {
                var result = command.ExecuteNonQuery();
                if (result >= 1)
                {
                    return true;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            finally
            {
                client.Close();
            }
        }
        public static string ExecuteScalar(string sqlCommand)
        {
            {
                MySqlConnection client = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"));
                client.Open();
                var command = new MySqlCommand(sqlCommand, client);
                try
                {
                    var value = command.ExecuteScalar();
                    if (value != null)
                    {
                        return Convert.ToString(command.ExecuteScalar());
                    }

                    return "";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Close();
                }
            }
        }
        public static List<Tuple<string, string>> ExecuteReaderStringString(string sqlCommand)
        {
            {
                MySqlConnection client = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"));
                client.Open();
                var returnData = new List<Tuple<string, string>>();
                var command = new MySqlCommand(sqlCommand, client);
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnData.Add(new Tuple<string, string>(reader.GetString(0), reader.GetString(1)));
                        }
                    }
                    reader.Close();
                    return returnData;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Close();
                }
            }
        }
        public static List<Tuple<string, string, string, int>> ExecuteReaderStringStringStringInt(string sqlCommand)
        {
            {
                MySqlConnection client = new MySqlConnection(Environment.GetEnvironmentVariable("MYSQL_CONNECTION_STRING"));
                client.Open();
                var returnData = new List<Tuple<string, string, string, int>>();
                var command = new MySqlCommand(sqlCommand, client);
                try
                {
                    var reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            returnData.Add(new Tuple<string, string, string, int>(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3)));
                        }
                    }
                    reader.Close();
                    return returnData;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                finally
                {
                    client.Close();
                }
            }
        }
    }
    
}