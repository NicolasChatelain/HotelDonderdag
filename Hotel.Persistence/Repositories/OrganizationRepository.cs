using Hotel.Domain.Interfaces;
using Hotel.Domain.Model;
using Hotel.Persistence.Enums;
using Hotel.Persistence.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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

        public List<Activity> GetAllActivitiesByOrganization(int id)
        {
            List<Activity> list = new();
            string SQLquery = "select Activity.AcitivityID, Activity.Fixture, Activity.Capacity, Description.Duration, Description.Location, Description.Description, Description.Name, PriceInfo.Adultprice, PriceInfo.Childprice, PriceInfo.Discount, PriceInfo.Adultage from Activity left join Description on Activity.DescriptionID = Description.DescriptionID left join PriceInfo on Activity.PriceInfoID = PriceInfo.PriceInfoID where Activity.OrganizationID = @id;";

            try
            {
                using (SqlConnection connection = new(connectionstring))
                using (SqlCommand command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = SQLquery;
                    command.Parameters.AddWithValue("@id", id);
                    IDataReader reader = command.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {

                            Description description = new();
                            PriceInfo priceinfo = new();
                            Activity activity = new();

                            activity.Id = reader.GetInt32(0);
                            activity.Fixture = reader.GetDateTime(1);
                            activity.Capacity = reader.GetInt32(2);

                            description.Duration = reader.GetInt32(3);
                            description.Location = reader.GetString(4);
                            description.DetailedDescription = reader.GetString(5);
                            description.Name = reader.GetString(6);

                            priceinfo.AdultPrice = reader.GetInt32(7);
                            priceinfo.ChildPrice = reader.GetInt32(8);
                            priceinfo.DiscountPercentage = reader.GetInt32(9);
                            priceinfo.AdultAge = reader.GetInt32(10);

                            activity.Description = description;
                            activity.PriceInfo = priceinfo;

                            list.Add(activity);
                        }

                        reader.Close();
                        return list;
                    }
                    catch (Exception)
                    {
                        throw new OrganizationRepositoryException("Something went wrong when retrieving all the activities for thos organization.");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
