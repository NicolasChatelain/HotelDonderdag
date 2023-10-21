using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections;
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
        private const byte ActiveStatus = 1;

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
                        command.Parameters.AddWithValue("@status", ActiveStatus);

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
                            command.Parameters.AddWithValue("@status", ActiveStatus);
                            command.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw new Exception("Something went wrong when adding this customer, make sure that all members are unique.");
                    }

                }
            }
            catch (Exception)
            {

                throw;
            }
            return id;
        }

        public void RemoveCustomer(int customerID)
        {
            try
            {

                string customerQuery = "update Customer set status = @status where Id = @id;";
                string memberQuery = "update Member set status = @status where CustomerId = @id";

                using SqlConnection connection = new(connectionString);
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    SqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        command.Transaction = transaction;
                        command.CommandText = customerQuery;

                        command.Parameters.AddWithValue("@id", customerID);
                        command.Parameters.AddWithValue("@status", InactiveStatus);
                        command.ExecuteNonQuery();


                        command.Parameters.Clear();

                        command.CommandText = memberQuery;

                        command.Parameters.AddWithValue("@id", customerID);
                        command.Parameters.AddWithValue("@status", InactiveStatus);
                        command.ExecuteNonQuery();



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
                throw new CustomerRepositoryException(ex.Message, ex);
            }
        } // sets user inactive, stays in DB

        public void UpdateCustomer(Customer customer)
        {
            try
            {
                string SQLquery = "update Customer set name = @name, email = @email, phone = @phone, address = @address where id = @id;";
                using (SqlConnection connection = new(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.CommandText = SQLquery;
                        command.Parameters.AddWithValue("@id", customer.Id);
                        command.Parameters.AddWithValue("@name", customer.Name);
                        command.Parameters.AddWithValue("@email", customer.Contact.Email);
                        command.Parameters.AddWithValue("@phone", customer.Contact.Phone);
                        command.Parameters.AddWithValue("@address", customer.Contact.Address.ToAddressLine());

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something went wrong when updating this customer, update aborted", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void AddMember(int id, List<Member> members)
        {
            try
            {
                string SQlquery = "insert into Member (name, birthday, customerId, status) values (@name, @birthday, @customerId, @status);";
                try
                {
                    using SqlConnection connection = new(connectionString);
                    using (SqlCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        foreach (Member member in members)
                        {
                            command.Parameters.Clear();
                            command.CommandText = SQlquery;

                            command.Parameters.AddWithValue("@name", member.Name);
                            command.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));
                            command.Parameters.AddWithValue("@customerId", id);
                            command.Parameters.AddWithValue("@status", ActiveStatus);

                            command.ExecuteNonQuery();
                        }


                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Something went wrong when adding the new members, task aborted", ex);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void RemoveMember(int id, Member member)
        {
            try
            {
                string SQLquery = "delete from Member where customerId = @id and name = @name and birthday = @birthday;";
                using (SqlConnection connection = new(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.CommandText = SQLquery;
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@name", member.Name);
                        command.Parameters.AddWithValue("@birthday", member.Birthday.ToDateTime(TimeOnly.MinValue));

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("something went wrong when removing this member, removal cancelled", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public void UpdateMember(int id, Member memberOriginalState, Member memberUpdatedState)
        {
            try
            {
                string SQlquery = "update Member set name = @newname, birthday = @newbirthday where customerId = @id and name = @originalname and birthday = @originalbirthday;";

                using (SqlConnection connection = new(connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();

                    try
                    {
                        command.CommandText = SQlquery;
                        command.Parameters.AddWithValue("@newname", memberUpdatedState.Name);
                        command.Parameters.AddWithValue("@newbirthday", memberUpdatedState.Birthday.ToDateTime(TimeOnly.MinValue));
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@originalname", memberOriginalState.Name);
                        command.Parameters.AddWithValue("@originalbirthday", memberOriginalState.Birthday.ToDateTime(TimeOnly.MinValue));

                        command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Something went wrong when updating this member, update aborted.", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }
    }
}
