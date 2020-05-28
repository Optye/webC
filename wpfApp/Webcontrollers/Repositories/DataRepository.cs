using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webcontrollers.Models;

namespace Webcontrollers.Repositories
{
    public class DataRepository : IDataRepository
    {
        private const string connectionString = "datasource=127.0.0.1;port=3306;username=webcontroller;password=web;database=web_con_db;";
        private MySqlConnection connection;

        private IList<Data> dataList = new List<Data>();
        
        public DataRepository()
        {
            
            connection = new MySqlConnection(connectionString);
            //connection.Open();

            string query = "SELECT* FROM data";

            // Prepare the connection
            MySqlCommand commandDatabase = new MySqlCommand(query, connection);
            commandDatabase.CommandTimeout = 60;
            MySqlDataReader reader;

            // Let's do it !
            // Open the database
            connection.Open();

            // Execute the query
            reader = commandDatabase.ExecuteReader();

            // All succesfully executed, now do something

            // IMPORTANT : 
            // If your query returns result, use the following processor :

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    dataList.Add(new Data
                    {
                        Id = Convert.ToInt32(reader[0]),
                        A = Convert.ToInt32(reader[1]),
                        B = Convert.ToInt32(reader[2])
                    });
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }

            // Finally close the connection
            connection.Close();

        }

        public IList<Data> GetAll()
        {
            return dataList;
        }

        public Data GetOne(int id)
        {
            foreach (Data data in dataList)
            {
                if(data.Id == id)
                {
                    return data;
                }
            }
            return null;
        }


    }
}
