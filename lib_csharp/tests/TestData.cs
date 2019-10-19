using System;
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
        public Guid? Id { get; set; }

        public string Name { get; set; }

        public DateTime BirthDate { get; set; }

        public string BirthCountry { get; set; }

        public int Age
        {
            get
            {
                // https://stackoverflow.com/questions/9/how-do-i-calculate-someones-age-in-c
                // Save today's date.
                var today = DateTime.Today;
                // Calculate the age.
                var age = today.Year - this.BirthDate.Year;
                // Go back to the year the person was born in case of a leap year
                if (this.BirthDate > today.AddYears(-age)) age--;
                return age;
            }
        }

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
                    Id = new Guid("74a46e33-09b3-4f90-9818-7ca3609f887f"),
                    Name = "Peter Pan",
                    BirthDate = new DateTime(DateTime.Now.Year - 10, 1, 1),
                    BirthCountry = "United States",
                    Gender = Gender.Male,
                    ContactInfo = new ContactInfo { Telephone = "(99)9999-1111", Email = "peterpan@neverland.com" },
                    Addresses =
                    {
                        new PersonAdress { AddressLine1 = "Never Land Street", AddressLine2 = "9999" }
                    }
                },
                new Person
                {
                    Name = "João da Sílva Conceição Menêzes",
                    BirthDate = new DateTime(DateTime.Now.Year - 36, 1, 1),
                    BirthCountry = "Brazil",
                    Gender = Gender.Male,
                    ContactInfo = new ContactInfo { Telephone = "(99)98765-4321", Email = "joaodasilva@gmail.com" },
                    Addresses =
                    {
                        new PersonAdress { AddressLine1 = "Rua das Flores", AddressLine2 = "9876" }
                    }
                },
                new Person
                {
                    Name = "John Doe",
                    BirthDate = new DateTime(DateTime.Now.Year - 21, 1, 1),
                    BirthCountry = "United States",
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
                    BirthDate = new DateTime(DateTime.Now.Year - 22, 1, 1),
                    BirthCountry = "Russia",
                    Gender = Gender.Male,
                    Addresses =
                    {
                        new PersonAdress { AddressLine1 = "Some Russian Address", AddressLine2 = "N/A" }
                    }
                },
                new Person
                {
                    Name = "Alicia Florick",
                    BirthDate = new DateTime(DateTime.Now.Year - 39, 1, 1),
                    BirthCountry = "United States",
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
                    BirthDate = new DateTime(DateTime.Now.Year - 150, 1, 1),
                    BirthCountry = "Fantasy Land",
                    Gender = Gender.Female,
                    Addresses =
                    {
                        new PersonAdress { AddressLine1 = "Some Forest Adress", AddressLine2 = "N/A" },
                        new PersonAdress { AddressLine1 = "Some Forest Adress", AddressLine2 = "N/A" }
                    }
                },
            };
            return list.AsQueryable();
        }
    }
}