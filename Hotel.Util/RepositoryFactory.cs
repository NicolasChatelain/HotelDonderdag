using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
        private readonly static string _connectionString = ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString;

        public static ICustomerRepository CustomerRepository
        {
            get
            {
                return new CustomerRepository(_connectionString);
            }
        }

        public static IOrganizationRepository OrganizationRepository
        {
            get
            {
                return new OrganizationRepository(_connectionString);
            }
        }

        public static IRegistrationsRepository RegistrationRepository
        {
            get
            {
                return new RegistrationsRepository(_connectionString);
            }
        }
    }
}