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

        public Registration? GetRegistration(Activity activity)
        {
            string SQLquery = "select * from Registrations R left join Activities A on R.ActivityID = A.ActivityID left join PriceInfo P on A.PriceInfoId = P.PriceInfoId left join Description D on A.DescriptionID = D.DescriptionID left join Member M on R.MemberID = M.ID where D.Name = @name and A.Fixture = @date;";
            Registration? registration = null;
            int customerId = 0;

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@name", activity.Description.Name);
                    command.Parameters.AddWithValue("@date", activity.Fixture);

                    Activity? registeredActivity = null;
                    List<Member> subscribedMembers = new();
                    int registrationID = 0;

                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        if (registeredActivity is null)
                        {
                            PriceInfo priceInfo = new()
                            {
                                AdultPrice = reader.GetInt32(12),
                                ChildPrice = reader.GetInt32(13),
                                DiscountPercentage = reader.GetInt32(14),
                                AdultAge = reader.GetInt32(15)
                            };

                            Description description = new()
                            {
                                Duration = reader.GetInt32(17),
                                Location = reader.GetString(18),
                                DetailedDescription = reader.GetString(19),
                                Name = reader.GetString(20),
                            };

                            registeredActivity = new()
                            {
                                Id = reader.GetInt32(4),
                                Capacity = reader.GetInt32(8),
                                Fixture = reader.GetDateTime(9),
                                Description = description,
                                PriceInfo = priceInfo
                            };
                        }

                        customerId = reader.GetInt32(24);

                        Member member = new(reader.GetInt32(21), reader.GetString(22), DateOnly.FromDateTime(reader.GetDateTime(23)), new Customer(customerId));
                        subscribedMembers.Add(member);

                        registrationID = reader.GetInt32(3);
                    }

                    reader.Close();

                    if (registeredActivity is not null)
                    {
                        registration = new()
                        {
                            Id = registrationID,
                            Activity = registeredActivity,
                        };

                        registration.Subscribe(subscribedMembers);
                    }
                }

                return registration;
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }

        }

        public List<Member> GetSubscribedMembersForActivity(int activityId, int customerId)
        {
            string SQLquery = "select R.ID, M.ID, M.name, M.birthday from Registrations R left join Activities A on R.ActivityID = A.ActivityId left join Member M on R.MemberID = M.ID where A.ActivityId = @Aid and M.customerId = @Cid;";
            List<Member> members = new();

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
                        Member member = new(reader.GetInt32(1), reader.GetString(2), DateOnly.FromDateTime(reader.GetDateTime(3)));
                        members.Add(member);
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

        public bool MakeRegistration(Registration registration)
        {
            string SQLquery = "insert into Registrations (ActivityID, MemberID, RegistrationID) values (@activityID, @memberID, @registrationID);";
            int result = 0;
            int registrationID = GetMaxID() + 1; // de hoogste id wordt opgehaald zodat de nieuwe registratie de highest + 1 kan hebben

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;


                    foreach (Member member in registration.Subscribers)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@activityID", registration.Activity.Id);
                        command.Parameters.AddWithValue("@memberID", member.ID);
                        command.Parameters.AddWithValue("@registrationID", registrationID);
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

        public bool UpdateRegistration(Registration registration)
        {
            string DeleteQuery = "delete from Registrations where RegistrationID = @RegID";
            string InsertQuery = "insert into Registrations (ActivityID, MemberID, RegistrationID) values (@activityID, @memberID, @registrationID)";

            try
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    using (SqlTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (SqlCommand deleteCommand = new(DeleteQuery, connection, transaction))
                            {
                                deleteCommand.Parameters.AddWithValue("@RegID", registration.Id);
                                deleteCommand.ExecuteNonQuery();
                            }

                            using (SqlCommand insertCommand = new(InsertQuery, connection, transaction))
                            {
                                foreach (Member member in registration.Subscribers)
                                {
                                    insertCommand.Parameters.Clear();
                                    insertCommand.Parameters.AddWithValue("@activityID", registration.Activity.Id);
                                    insertCommand.Parameters.AddWithValue("@memberID", member.ID);
                                    insertCommand.Parameters.AddWithValue("@registrationID", registration.Id);
                                    insertCommand.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }

        private int GetMaxID() // gets the id of the latest registration
        {
            string SQLquery = "select max(RegistrationID) from Registrations;";

            try
            {
                using (SqlConnection connection = new(_connectionString))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    object result = command.ExecuteScalar();

                    if (result != DBNull.Value)
                    {
                        int value = Convert.ToInt32(result);
                        return value;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new RegistrationRepositoryException(ex.Message);
            }
        }

    }
}
