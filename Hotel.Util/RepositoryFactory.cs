using Hotel.Domain.Interfaces;
using Hotel.Persistence.Repositories;
using System.Configuration;

namespace Hotel.Util
{
    public static class RepositoryFactory
    {
        public static ICustomerRepository CustomerRepository
        {
            get
            {
                return new CustomerRepository(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString);
            }
        }

        public static IOrganizationRepository OrganizationRepository
        {
            get
            {
                return new OrganizationRepository(ConfigurationManager.ConnectionStrings["HotelDB"].ConnectionString);
            }
        }
    }
}