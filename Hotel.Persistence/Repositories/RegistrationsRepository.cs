using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Exceptions;
using System.Data.SqlClient;

namespace Hotel.Persistence.Repositories
{
    public class RegistrationsRepository : IRegistrationsRepository
    {
        private readonly string _connectionString;

        public RegistrationsRepository(string connectionstring)
        {
            _connectionString = connectionstring;
        }

        public List<Member> GetMembersForCustomer(int customerId)
        {
            string SQLquery = "select name, birthday from Member where customerId = @id;";
            List<Member> members = new();

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@id", customerId);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        Member m = new(reader.GetString(0), DateOnly.FromDateTime(reader.GetDateTime(1)));
                        members.Add(m);
                    }

                    reader.Close();
                }
                return members;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }

        public Dictionary<int, string> GetValidLoginPhones()
        {
            string SQLquery = "select Id, phone from Customer";
            Dictionary<int, string> phonesDict = new();

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        phonesDict.Add(reader.GetInt32(0), reader.GetString(1));
                    }

                    reader.Close();
                }

                return phonesDict;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }



    }
}
