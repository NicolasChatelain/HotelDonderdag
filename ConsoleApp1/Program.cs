using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");

            string conn = "Data Source=.\\SQLEXPRESS;Initial Catalog=Hotel;Integrated Security=True";

            CustomerRepository repo = new(conn);

            Customer c = new("janssens", new ContactInfo("janssesn@gmail.com", "12456", new Address("Gent", "lodewijklaan", "9000", "87")));
            c.AddMember(new Member("willy", new DateOnly(1928, 5, 8)));
            c.AddMember(new Member("michel", new DateOnly(1928, 1, 1)));
            repo.AddCustomer(c);


        }
    }
}