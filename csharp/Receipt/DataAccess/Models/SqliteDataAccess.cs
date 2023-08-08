using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataAccess.Models
{
    public class SqliteDataAccess
    {
        public static void Init()
        {
            DatabaseFacade facade = new DatabaseFacade(new PersonsContext());
            facade.EnsureCreated();
        }

        public static bool create(Person person)
        {
            using (PersonsContext context = new PersonsContext())
            {
                var persons = context.Persons.ToList();
                context.Persons.Add(person);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Person> read()
        {
            using (PersonsContext context = new PersonsContext())
            {
                var persons = context.Persons.Include(p => p.Addresses).Include(p=>p.Emails);
                if(persons == null)
                {
                    return null;
                }
                return persons.ToList();
            }
        }

        public static Person? readOne(int Id)
        {
            var persons = read();
            foreach(var person in persons)
            {
                if(person.Id == Id)
                {
                    return person;
                }
            }
            return null;
        }

        public static Person? update(Person person)
        {
            using (PersonsContext context = new PersonsContext())
            {
                var _person = context.Persons.Find(person.Id);
                if(_person != null)
                {
                    _person.FirstName = person.FirstName;
                    _person.LastName = person.LastName;
                    _person.Addresses = person.Addresses;
                    _person.Emails = person.Emails;
                    context.SaveChanges();
                    return _person;
                }
                return null;
            }
        }

        public static bool delete(Person person)
        {
            using (PersonsContext context = new PersonsContext())
            {
                var p = readOne(person.Id);
                // Delete addresses
                foreach (var address in person.Addresses)
                {
                    context.Remove(address);
                }
                // Delete emails
                foreach (var email in person.Emails)
                {
                    context.Remove(email);
                }
#pragma warning disable CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                context.Remove(p);
#pragma warning restore CS8634 // The type cannot be used as type parameter in the generic type or method. Nullability of type argument doesn't match 'class' constraint.
                context.SaveChanges();
            }
            return true;
        }
    }
}
