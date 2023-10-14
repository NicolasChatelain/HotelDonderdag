using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string connectionString;
        private const byte InactiveStatus = 0;

        public CustomerRepository(string connectionstring)
        {
            this.connectionString = connectionstring;
        }

        public IReadOnlyList<Customer> GetCustomers(string filter)
        {
            try
            {
                Dictionary<int, Customer> customers = new();
                string sql = "select t1.id, t1.name customername, t1.email, t1.phone, t1.address, t2.name membername, t2.birthday from customer t1 left join (select * from member where status = 1) t2 on t1.id = t2.customerId where t1.status = 1";

                if (!string.IsNullOrWhiteSpace(filter))
                {
                    sql += " and (t1.id like @filter or t1.name like @filter or t1.email like @filter)";
                }

                using (SqlConnection conn = new(connectionString))
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    conn.Open();
                    cmd.CommandText = sql;

                    if (!string.IsNullOrWhiteSpace(filter))
                    {
                        cmd.Parameters.AddWithValue("@filter", $"%{filter}%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["ID"]);
                        if (!customers.ContainsKey(id))
                        {
                            Customer customer = new(id, (string)reader["customername"], new ContactInfo((string)reader["email"], (string)reader["phone"],
                            new Address((string)reader["address"])));
                            customers.Add(id, customer);
                        }
                        if (!reader.IsDBNull(reader.GetOrdinal("membername")))
                        {
                            Member member = new((string)reader["membername"], DateOnly.FromDateTime((DateTime)reader["birthday"]));
                            customers[id].AddMember(member);
                        }



                    }
                }

                return customers.Values.ToList();
            }
            catch (CustomerRepositoryException ex)
            {
                throw new CustomerRepositoryException(ex.Message);
            }
        }

        public int AddCustomer(Customer customer)
        {

            int id;

            try
            {
                string sql = "insert into Customer (name, email, phone, address, status) output inserted.ID values (@name, @email, @phone, @address, @status)";

                using (SqlConnection connection = new(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {


                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        command.Transaction = transaction;

                        command.CommandText = sql;
                        command.Parameters.AddWithValue("@name", customer.Name);
                        command.Parameters.AddWithValue("@email", customer.Contact.Email);
                        command.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        command.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());
                        command.Parameters.AddWithValue("@status", 1);

                        id = Convert.ToInt32(command.ExecuteScalar());
                        customer.Id = id;

                        foreach (Member member in customer.GetMembers())
                        {
                            sql = "insert into Member (customerId, name, birthday, status) values (@customerid, @name, @birthday, @status)";
                            command.CommandText = sql;
                            command.Parameters.Clear();

                            command.Parameters.AddWithValue("@customerid", customer.Id);
                            command.Parameters.AddWithValue("@name", member.Name);
                            command.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                            command.Parameters.AddWithValue("@status", 1);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }

                }
            }
            catch (Exception ex)
            {

                throw new CustomerRepositoryException("addcustomer", ex);
            }
            return id;
        }

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                string query = "update Customer set name = @name, email = @email, phone = @phone, address = @address where id = @id;";
                using SqlConnection connection = new(connectionString);
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@id", customer.Id);
                        command.Parameters.AddWithValue("@name", customer.Name);
                        command.Parameters.AddWithValue("@email", customer.Contact.Email);
                        command.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        command.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());

                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }


            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Something went wrong when updating this customer.", ex);
            }
        }

        public void RemoveCustomer(int customerID)
        {
            try
            {
                string query = "update Customer set status = @status where id = @id;";
                using SqlConnection connection = new(connectionString);
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@id", customerID);
                        command.Parameters.AddWithValue("@status", InactiveStatus);

                        command.ExecuteNonQuery();
                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                throw new CustomerRepositoryException("Something went wrong when removing this customer.", ex);
            }
        }
    }
}
