using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Enums;
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

        public List<Activity> GetAllActivities()
        {
            string SQLquery = "select * from Activities A left join PriceInfo P on A.PriceInfoID = P.PriceInfoID left join Description D on A.DescriptionID = D.DescriptionID where A.Fixture >= @minimumdate;";
            List<Activity> Activities = new();
            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@minimumdate", DateTime.Now);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        PriceInfo priceInfo = new()
                        {
                            ID = reader.GetInt32(7),
                            AdultPrice = reader.GetInt32(8),
                            ChildPrice = reader.GetInt32(9),
                            DiscountPercentage = reader.GetInt32(10),
                            AdultAge = reader.GetInt32(11),
                        };

                        Description description = new()
                        {
                            ID = reader.GetInt32(12),
                            Duration = reader.GetInt32(13),
                            Location = reader.GetString(14),
                            DetailedDescription = reader.GetString(15),
                            Name = reader.GetString(16),
                        };

                        Activity activity = new()
                        {
                            Id = reader.GetInt32(0),
                            Description = description,
                            PriceInfo = priceInfo,
                            Capacity = reader.GetInt32(4),
                            Fixture = reader.GetDateTime(5),
                        };

                        Activities.Add(activity);
                    }

                    reader.Close();
                }

                return Activities;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }

        public List<Member> GetMembersForCustomer(int customerId)
        {
            string SQLquery = "select ID, name, birthday from Member where customerId = @id;";
            List<Member> members = new();

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@id", customerId);

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {

                        Member m = new(reader.GetInt32(0), reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));
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

        public (int RegistrationID, List<Member>) GetSubscribedMembersForActivity(int activityId, int customerId)
        {
            string SQLquery = "select R.ID, M.name, M.birthday from Registrations R left join Activities A on R.ActivityID = A.ActivityId left join Member M on R.MemberID = M.ID where A.ActivityId = @Aid and M.customerId = @Cid;";
            List<Member> members = new();
            int RegistrationID = 0;

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@Aid", activityId);
                    command.Parameters.AddWithValue("@Cid", customerId);

                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        if (RegistrationID == 0)
                        {
                            RegistrationID = reader.GetInt32(0);
                        }
                        Member member = new(reader.GetString(1), DateOnly.FromDateTime(reader.GetDateTime(2)));
                        members.Add(member);
                    }

                    reader.Close();
                }

                return (RegistrationID, members);

            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }

        }

        public Dictionary<int, (string, string)> GetValidLoginPhones()
        {
            string SQLquery = $"select Id, name, phone from Customer where status = {(int)Status.Active}";
            Dictionary<int, (string, string)> phonesDict = new();

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
                        phonesDict.Add(reader.GetInt32(0), (reader.GetString(1), reader.GetString(2)));
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

        public bool MakeRegistration(List<Member> members, Activity activity)
        {
            string SQLquery = "insert into Registrations (ActivityID, MemberID) values (@activityID, @memberID);";
            int result = 0;

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;


                    foreach (Member member in members)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@activityID", activity.Id);
                        command.Parameters.AddWithValue("@memberID", member.ID);
                        result = command.ExecuteNonQuery();
                    }
                }

                return result > 0;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }

    }
}
