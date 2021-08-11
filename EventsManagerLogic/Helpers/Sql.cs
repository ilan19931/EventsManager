using EventsManager.Classes;
using EventsManagerLogic.Classes;
using EventsManagerLogic.Events;
using EventsManagerLogic.Validators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsManagerLogic.Helpers
{
    public class Sql
    {
        private static string m_ConnectionString = "Data Source=DESKTOP-NQK6RD1\\SQLSERVER; Initial Catalog=EventsManager; User id=sa; Password=1234;";
        private Security security = new Security();
        private TextValidator textValidator = new TextValidator();


        public DataRowCollection SelectQuery(string query)
        {
            DataTable dataTable = null;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);

                }
                catch (Exception e)
                {

                }
            }

            return dataTable.Rows;
        }
        public DataRow SelectOneItemQuery(string query)
        {
            DataTable dataTable = null;
            DataRow data = null;
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                try
                {
                    connection.Open();

                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, connection);
                    dataTable = new DataTable();
                    sqlDataAdapter.Fill(dataTable);
                    data = dataTable.Rows[0];

                }
                catch (Exception e)
                {

                }
            }

            return data;
        }
        public void DoQuery(string query)
        {
            using (SqlConnection connection = new SqlConnection(m_ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CloseEvent(int i_Id)
        {
            string query = $"SELECT * FROM events WHERE id = {i_Id} AND isClosed = 0";
            DataRow data = SelectOneItemQuery(query);

            if (data != null)
            {
                query = $"UPDATE events SET isClosed = 1 WHERE id = {i_Id}";
                this.DoQuery(query);
            }
        }

    }
}
