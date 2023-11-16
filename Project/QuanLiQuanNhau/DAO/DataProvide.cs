using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLiQuanNhau.DAO
{
    public class DataProvide
    {
        private static DataProvide instance;
        private string connectionSTR = @"Data Source = Hoang_NT; Initial Catalog = QuanLyQuanNhau99; Integrated Security = True";

        public static DataProvide Instance 
        {
            get { if (instance == null) instance = new DataProvide(); return DataProvide.instance; }
            private set { DataProvide.instance = value; } 
        }
        private DataProvide() { }

        public DataTable ExcuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(data);

                connection.Close();
            }
            return data;
        }
        public int ExcuteNonQuery(string query, object[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);

                if (parameters != null)
                {
                    SqlParameter[] sqlParameters = new SqlParameter[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        sqlParameters[i] = new SqlParameter($"@p{i}", parameters[i]);
                    }

                    command.Parameters.AddRange(sqlParameters);
                }

                data = command.ExecuteNonQuery();
                connection.Close();
            }

            return data;
        }

    public object ExcuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {

                connection.Open();
                SqlCommand command = new SqlCommand(query, connection);
                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }
                data=command.ExecuteScalar();

                connection.Close();
            }
            return data;
        }

    }
}
