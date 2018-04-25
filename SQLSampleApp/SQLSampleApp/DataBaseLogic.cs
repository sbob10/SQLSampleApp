using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace SQLSampleApp
{
    public class DataBaseLogic
    {

        private SqlConnection _sqlConnection;
        private String _connectionString = "Server=PC04\\SQLEXPRESS;Database=CustomerDB;Trusted_Connection=true";

        public DataBaseLogic()
        {
            _sqlConnection = new SqlConnection(_connectionString);
        }

        public Result AddCustomer(Customer customer)
        {
            try
            {
                _sqlConnection.Open();

                SqlCommand insertCommand = new SqlCommand("INSERT INTO Customer VALUES ('" + customer.Name + "','" + customer.Mail + "','" + customer.PhoneNumber + "');", _sqlConnection);
                insertCommand.ExecuteNonQuery();

                return new Result(Message.DB_ENTRY_ADD, Status.Success);
            }
            catch (Exception e)
            {
                return new Result(Message.DB_ENTRY_ADD, Status.Error, exception: e);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public Result<List<Customer>> GetCustomer(String name)
        {
            try
            {
                List<Customer> customers = new List<Customer>();

                _sqlConnection.Open();

                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Customer WHERE customerName LIKE '%" + name + "%';", _sqlConnection);

                SqlDataReader dataReader = selectCommand.ExecuteReader();

                while(dataReader.Read())
                {
                    Customer tempCustomer = new Customer();
                    tempCustomer.ID = Int32.Parse(dataReader["ID"].ToString());
                    tempCustomer.Name = dataReader["customerName"].ToString();
                    tempCustomer.Mail = dataReader["Mail"].ToString();
                    tempCustomer.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    customers.Add(tempCustomer);
                }

                return new Result<List<Customer>>(data: customers, status: Status.Success, message: Message.DB_ENTRIES_LOAD);
            }
            catch (Exception e)
            {
                return new Result<List<Customer>>(data: null, message : Message.DB_ENTRIES_LOAD, status : Status.Error, exception: e);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

        public Result<Fortune> GetFortuneForCustomer(int ID)
        {
            try
            {
                Fortune fortune = new Fortune();

                _sqlConnection.Open();

                SqlCommand selectCommand = new SqlCommand("SELECT * FROM Fortune WHERE customerID = " + ID.ToString() + ";", _sqlConnection);

                SqlDataReader dataReader = selectCommand.ExecuteReader();

                while (dataReader.Read())
                {
                    fortune.ID = Int32.Parse(dataReader["ID"].ToString());
                    fortune.customerID = Int32.Parse(dataReader["customerID"].ToString());
                    fortune.Shares = Decimal.Parse(dataReader["Shares"].ToString());
                    fortune.Fonds = Decimal.Parse(dataReader["Fonds"].ToString());
                    fortune.Pensions = Decimal.Parse(dataReader["Pensions"].ToString());
                }

                return new Result<Fortune>(data: fortune, status: Status.Success, message: Message.DB_ENTRIES_LOAD);
            }
            catch (Exception e)
            {
                return new Result<Fortune>(data: null, message: Message.DB_ENTRIES_LOAD, status: Status.Error, exception: e);
            }
            finally
            {
                _sqlConnection.Close();
            }
        }

    }
}
