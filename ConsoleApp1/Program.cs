using Hotel.Domain.Model;
using Hotel.Persistence.Repositories;
using System.Threading.Channels;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Address ad1 = new("Gtown", "zarlar", "9500", "16");
            ContactInfo info1 = new("qsd@qsd", "987987", ad1);
            Customer c1 = new(1, "chatelain", info1);


            Member m1 = new("Nicolas", new DateOnly(1999, 5, 23));
            c1.AddMember(m1);

            //Member m2 = new("Bjarne", new DateOnly(1999, 7, 9));
            //c1.AddMember(m2);

            //Member m3 = new("dries", new DateOnly(1991, 2, 26));
            //c1.AddMember(m3);



            List<Member> comparisonList = new()
            {
                new("nicolas", new DateOnly(1999, 5, 23)),
                //new("Bjarne", new DateOnly(1999, 7, 9)),
                //new("Tom", new DateOnly(1985, 5, 5)),
            };


            List<Member> members = c1.GetMembers().Intersect(comparisonList).ToList();
            List<Member> diffMembers = c1.GetMembers().Except(comparisonList).Concat(comparisonList.Except(c1.GetMembers())).ToList();



            Console.WriteLine("these members are in both lists: ");
            members.ForEach(m => Console.WriteLine(m.Name));

            Console.WriteLine("\n\nThese members are unique: ");
            diffMembers.ForEach(m => Console.WriteLine(m.Name));



        }
    }
}