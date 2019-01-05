using System.Collections.Generic;
using System.Linq;

namespace JollazApiQueries.Tests
{
    public enum Gender
    {
        Male,
        Female
    }

    public class ContactInfo
    {
        public string Telephone { get; set; }

        public string Email { get; set; }
    }

    public class PersonAdress
    {
        public string AddressLine1 { get; set; }
        
        public string AddressLine2 { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public Gender Gender { get; set; }

        public bool IsChild => this.Age < 18;

        public ContactInfo ContactInfo { get; set; }

        public ICollection<PersonAdress> Addresses { get; set; }

        public Person()
        {
            this.Addresses = new List<PersonAdress>();
        }

        public static IQueryable<Person> GetPersonQuery()
        {
            var list = new List<Person>
            {
                new Person
                {
                    Name = "Peter Pan",
                    Age = 10,
                    Gender = Gender.Male,
                    ContactInfo = new ContactInfo { Telephone = "(99)9999-1111", Email = "peterpan@neverland.com" },
                    Addresses = 
                    {
                        new PersonAdress { AddressLine1 = "Never Land Street", AddressLine2 = "9999" }
                    }
                },
                new Person
                {
                    Name = "John Doe",
                    Age = 21,
                    Gender = Gender.Male,
                    ContactInfo = new ContactInfo { Telephone = "(123)456-7890", Email = "johndoe@gmail.com" },
                    Addresses = 
                    {
                        new PersonAdress { AddressLine1 = "Sesame Street", AddressLine2 = "2112" }
                    }
                },
                new Person
                {
                    Name = "White Death",
                    Age = 22,
                    Gender = Gender.Male,
                    Addresses = 
                    {
                        new PersonAdress { AddressLine1 = "Some Russian Address", AddressLine2 = "N/A" }
                    }
                },
                new Person
                {
                    Name = "Alicia Florick",
                    Age = 39,
                    Gender = Gender.Female,
                    ContactInfo = new ContactInfo { Telephone = "(11)3210-4567", Email = "aliciaflorick@hotmail.com" },
                    Addresses = 
                    {
                        new PersonAdress { AddressLine1 = "Home Street Address", AddressLine2 = "9123" },
                        new PersonAdress { AddressLine1 = "Work Street Address", AddressLine2 = "1234" }
                    }
                },
                new Person
                {
                    Name = "Snow White",
                    Age = 150,
                    Gender = Gender.Female,
                    Addresses = 
                    {
                        new PersonAdress { AddressLine1 = "Some Forest Adress", AddressLine2 = "N/A" }
                    }
                },
            };
            return list.AsQueryable();
        }
    }
}