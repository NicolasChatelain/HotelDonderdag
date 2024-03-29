﻿using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Enums;
using Hotel.Persistence.Exceptions;

using System.Data;
using System.Data.SqlClient;


namespace Hotel.Persistence.Repositories
{
    public class OrganizationRepository : IOrganizationRepository
    {
        private readonly string connectionstring;

        public OrganizationRepository(string connectionstring)
        {
            this.connectionstring = connectionstring;
        }

        public List<Organization> GetAllOrganizations()
        {
            string SQLquery = $"select organizationID, name, email, phone, address from Organizations where status = {(int)Status.Active};";
            try
            {
                List<Organization> list = new();

                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {

                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Organization organization = new();
                            organization.Id = reader.GetInt32(0);
                            organization.Name = reader.GetString(1);

                            string email = reader.GetString(2);
                            string phone = reader.GetString(3);

                            Address address = new(reader.GetString(4));

                            ContactInfo contactinfo = new(email, phone, address);
                            organization.Contact = contactinfo;

                            organization.Contact.Address = address;

                            list.Add(organization);

                        }


                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                return list;
            }
            catch (Exception)
            {
                throw new OrganizationRepositoryException("Something went wrong when retrieving the organizations.");
            }
        }

        public int AddOrganization(Organization org)
        {
            try
            {
                string SQLquery = "insert into Organizations (name, email, phone, address, status) output inserted.organizationID values (@name, @email, @phone, @address, @status);";

                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        command.Parameters.AddWithValue("@name", org.Name);
                        command.Parameters.AddWithValue("@email", org.Contact.Email);
                        command.Parameters.AddWithValue("@phone", org.Contact.Phone);
                        command.Parameters.AddWithValue("@address", org.Contact.Address.ToAddressLine());
                        command.Parameters.AddWithValue("@status", Status.Active);

                        int id = Convert.ToInt32(command.ExecuteScalar());
                        return id;
                    }
                    catch (Exception)
                    {
                        throw;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new OrganizationRepositoryException("Something went wrong when adding the organization.", ex);
            }
        }

        public void RemoveOrganization(int ID)
        {
            string SQLquery = $"update Organizations set status = {(int)Status.Inactive} where organizationID = {ID};";

            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand command = connection.CreateCommand();

                connection.Open();
                command.CommandText = SQLquery;
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new OrganizationRepositoryException("Something went wrong when removing this member.", ex);
            }
        }

        public void UpdateOrganization(int id, Organization org)
        {
            string SQLquery = "update Organizations set name = @name, email = @email, phone = @phone, address = @address where organizationID = @id";

            try
            {
                using SqlConnection connection = new(connectionstring);
                using SqlCommand command = connection.CreateCommand();

                connection.Open();
                command.CommandText = SQLquery;

                try
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", org.Name);
                    command.Parameters.AddWithValue("@email", org.Contact.Email);
                    command.Parameters.AddWithValue("@phone", org.Contact.Phone);
                    command.Parameters.AddWithValue("@address", org.Contact.Address.ToAddressLine());

                    command.ExecuteNonQuery();

                }
                catch (Exception)
                {
                    throw new OrganizationRepositoryException("Something went wrong when updating this organization.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Activity> GetAllActivitiesByOrganization(int id, bool onlyActives, string? filter)
        {
            List<Activity> list = new();
            string SQLquery = "select A.IsActive, A.ActivityID, A.Fixture, A.Capacity, D.Duration, D.Location, D.Description, D.Name, PriceInfo.Adultprice, PriceInfo.Childprice, PriceInfo.Discount, PriceInfo.Adultage from Activities A left join Description D on A.DescriptionID = D.DescriptionID left join PriceInfo on A.PriceInfoID = PriceInfo.PriceInfoID where A.OrganizationID = @id";

            if (onlyActives)
            {
                SQLquery += $" AND A.Fixture > GETDATE() AND A.IsActive = 1";
            }

            if (filter is not null)
            {
                SQLquery += $" AND (D.Name LIKE @filter OR A.Fixture LIKE @filter OR D.Location LIKE @filter);";
            }

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@id", id);
                    if (filter is not null)
                    {
                        command.Parameters.AddWithValue("@filter", "%" + filter + "%");
                    }
                    IDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {

                            Description description = new();
                            PriceInfo priceinfo = new();
                            Activity activity = new();

                            if (reader.GetBoolean(0))
                            {
                                activity.IsUpcoming = reader.GetBoolean(0);
                            }

                            activity.Id = reader.GetInt32(1);
                            activity.Fixture = reader.GetDateTime(2);
                            activity.Capacity = reader.GetInt32(3);

                            description.Duration = reader.GetInt32(4);
                            description.Location = reader.GetString(5);
                            description.DetailedDescription = reader.GetString(6);
                            description.Name = reader.GetString(7);

                            priceinfo.AdultPrice = reader.GetInt32(8);
                            priceinfo.ChildPrice = reader.GetInt32(9);
                            priceinfo.DiscountPercentage = reader.GetInt32(10);
                            priceinfo.AdultAge = reader.GetInt32(11);

                            activity.Description = description;
                            activity.PriceInfo = priceinfo;

                            list.Add(activity);
                        }

                        reader.Close();
                        return list;
                    }
                    catch (Exception)
                    {
                        throw new OrganizationRepositoryException("Something went wrong when retrieving all the activities for this organization.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void RemoveActivity(int id)
        {
            string SQLquery = "update Activities set IsActive = @cancelled where ActivityID = @id";

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        command.Parameters.AddWithValue("@cancelled", (int)Status.Inactive);
                        command.Parameters.AddWithValue("@id", id);

                        command.ExecuteNonQuery();

                    }
                    catch (Exception)
                    {
                        throw new OrganizationRepositoryException("Something went wrong when removing this activity.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddActivty(Activity activity, int orgID)
        {
            string priceinfo_insert = "insert into PriceInfo (Adultprice, Childprice, Discount, Adultage) output inserted.PriceInfoID values (@adultprice, @childprice, @discount, @adultage);";
            string description_insert = "insert into Description (Duration, Location, Description, Name) output inserted.DescriptionID values (@duration, @location, @description, @name);";
            string activity_insert = "insert into Activities (PriceInfoID, DescriptionID, OrganizationID, Capacity, Fixture, IsActive) values (@priceinfoID, @descriptionID, @orgID, @capacity, @fixture, @isactive);";


            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {

                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();
                    command.Transaction = transaction;

                    try
                    {
                        command.CommandText = priceinfo_insert;

                        command.Parameters.AddWithValue("@adultprice", activity.PriceInfo.AdultPrice);
                        command.Parameters.AddWithValue("@childprice", activity.PriceInfo.ChildPrice);
                        command.Parameters.AddWithValue("@discount", activity.PriceInfo.DiscountPercentage);
                        command.Parameters.AddWithValue("@adultage", activity.PriceInfo.AdultAge);

                        int priceinfoID = (int)command.ExecuteScalar();

                        command.CommandText = description_insert;

                        command.Parameters.AddWithValue("@duration", activity.Description.Duration);
                        command.Parameters.AddWithValue("@location", activity.Description.Location);
                        command.Parameters.AddWithValue("@description", activity.Description.DetailedDescription);
                        command.Parameters.AddWithValue("@name", activity.Description.Name);

                        int descriptionID = (int)command.ExecuteScalar();

                        command.CommandText = activity_insert;

                        command.Parameters.AddWithValue("@priceinfoID", priceinfoID);
                        command.Parameters.AddWithValue("@descriptionID", descriptionID);
                        command.Parameters.AddWithValue("@orgID", orgID);
                        command.Parameters.AddWithValue("@capacity", activity.Capacity);
                        command.Parameters.AddWithValue("@fixture", activity.Fixture);
                        command.Parameters.AddWithValue("@isactive", (int)Status.Active);

                        command.ExecuteNonQuery();

                        transaction.Commit();


                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new OrganizationRepositoryException(ex.Message);
                    }
                }
            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception("Someting went wrong in the database.");
            }



        }

        public List<Description> GetAllDescriptions(int orgID)
        {
            string SQLquery = "select d.DescriptionID, d.Name, d.Duration, d.Location, d.Description from Description d inner join Activities a on d.DescriptionID = a.DescriptionID where a.OrganizationID = @id;";

            List<Description> descriptions = new();


            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        command.Parameters.AddWithValue("@id", orgID);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            Description d = new()
                            {
                                ID = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Duration = reader.GetInt32(2),
                                Location = reader.GetString(3),
                                DetailedDescription = reader.GetString(4)
                            };

                            descriptions.Add(d);
                        }
                        reader.Close();

                        return descriptions;
                    }
                    catch (Exception)
                    {
                        throw new OrganizationRepositoryException("Something went wrong when getting the existing activities");
                    }
                }

            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new OrganizationRepositoryException("Something went wrong with the database.");
            }
        }

        public List<PriceInfo> GetAllPrices(int orgID)
        {
            string SQLquery = "select distinct p.PriceInfoID, p.Adultprice, p.Childprice, p.Discount, p.Adultage from PriceInfo p inner join Activities a on p.PriceInfoID = a.PriceInfoID where a.OrganizationID = @id;";

            List<PriceInfo> prices = new();


            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        command.Parameters.AddWithValue("@id", orgID);
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PriceInfo p = new()
                            {
                                ID = reader.GetInt32(0),
                                AdultPrice = reader.GetInt32(1),
                                ChildPrice = reader.GetInt32(2),
                                DiscountPercentage = reader.GetInt32(3),
                                AdultAge = reader.GetInt32(4)
                            };

                            prices.Add(p);
                        }

                        reader.Close();

                        return prices;
                    }
                    catch (Exception)
                    {
                        throw new OrganizationRepositoryException("Something went wrong when getting the existing prices");
                    }
                }

            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new OrganizationRepositoryException("Something went wrong with the database.");
            }
        }

        public int PlanExistingActivity(int priceinfoID, DateTime fixture, string capacity, int descriptionID, int orgID)
        {
            string SQLquery = "insert into Activities (PriceInfoID, DescriptionID, OrganizationID, Capacity, Fixture, IsActive) output inserted.ActivityId values (@price, @description, @org, @capacity, @fixture, @isactive);";

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;

                    try
                    {
                        command.Parameters.AddWithValue("@price", priceinfoID);
                        command.Parameters.AddWithValue("@description", descriptionID);
                        command.Parameters.AddWithValue("@org", orgID);
                        command.Parameters.AddWithValue("@capacity", capacity);
                        command.Parameters.AddWithValue("@fixture", fixture);
                        command.Parameters.AddWithValue("@isactive", (int)Status.Active);

                        int ID = (int)command.ExecuteScalar();
                        return ID;
                    }
                    catch (Exception ex)
                    {
                        throw new OrganizationRepositoryException(ex.Message);
                    }
                }
            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch
            {
                throw new OrganizationRepositoryException("Something went wrong in the database.");
            }
        }

        public bool ApplyDiscount(double discount, int id)
        {
            string SQLquery = "update PriceInfo set PriceInfo.Discount = @Discount from Activities inner join PriceInfo P on Activities.PriceInfoID = P.PriceInfoID where Activities.ActivityId = @activityID;";

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@Discount", discount);
                    command.Parameters.AddWithValue("@activityID", id);

                    try
                    {
                       return 0 < command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new OrganizationRepositoryException(ex.Message);
                    }
                }
            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch
            {
                throw new OrganizationRepositoryException("Something went wrong in the database.");
            }
        }

        public bool UpdateFixture(DateTime updatedFixture, int id)
        {
            string SQLquery = "update Activities set Fixture = @Fixture where ActivityId = @activityID;";

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@Fixture", updatedFixture);
                    command.Parameters.AddWithValue("@activityID", id);

                    try
                    {
                        return 0 < command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new OrganizationRepositoryException(ex.Message);
                    }
                }
            }
            catch (OrganizationRepositoryException)
            {
                throw;
            }
            catch
            {
                throw new OrganizationRepositoryException("Something went wrong in the database.");
            }
        }
    }
}
