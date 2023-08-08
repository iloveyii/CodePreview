using DataAccess.Models;
using ApiClient.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using DA = ApiClient.Models.DataAccess;

namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Init db");
            SqliteDataAccess.Init();

            // Create and show fake data at run time
            var persons = CreateFakeData();
            ShowPersons(persons);
            SavePersons(persons);
            List<Person> persons2 = SqliteDataAccess.read();
            ShowPersons(persons2);

            // Test ApiClient
            //DA.Run();
        }

        static void SavePersons(List<Person> persons)
        {
            // Iterate
            foreach (var person in persons)
            {
                Console.WriteLine("Persons saving");
                SqliteDataAccess.create(person);
            }
            Console.WriteLine("Persons saved to db!");
        }

        static void ShowPersons(List<Person> persons) 
        {
            // Iterate
            foreach (var person in persons)
            {
                Console.WriteLine($"Person {person.Id}: {person.FullName} has");
                Console.WriteLine("Address(s):");

                foreach (var address in person.Addresses)
                {
                    Console.WriteLine($"{address.Id}: {address.Street}, {address.City}, {address.State}, {address.Zip}");
                }
                Console.WriteLine();

                Console.WriteLine("Email(s):");
                foreach (var email in person.Emails)
                {
                    Console.WriteLine($"{email.Id}: {email.EmailAddress}");
                }

                Console.WriteLine();
            }
        }

        static List<Person> CreateFakeData()
        {
            var persons = new List<Person>
            {
                new Person
                {
                   FirstName="Hazrat", LastName="Ali", Addresses=new List<Address>
                    {
                        new Address
                        {
                           Street="Doktor libs", City="Gulheden", State="Goteborgy", Zip="41323"
                        },
                        new Address
                        {
                            Street="Abcd", City="Efgh", State="Xyz", Zip="12345"
                        }
                    }, Emails=new List<Email>
                    {
                        new Email
                        {
                           EmailAddress="ali.dev.se@gmail.com"
                        },
                        new Email
                        {
                           EmailAddress="tocomputerscientist@gmail.com"
                        },
                        new Email
                        {
                           EmailAddress="ali.hazrat@ki.se"
                        },
                        new Email
                        {
                            EmailAddress="ali.sweden19@yahoo.com"
                        }
                    },
                   
                }
            };

            return persons;
        }
    }

}
