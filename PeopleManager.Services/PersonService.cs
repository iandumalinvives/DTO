using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeopleManager.Data;
using PeopleManager.Model;
using PeopleManager.Services.Abstractions;

namespace PeopleManager.Services
{
	public class PersonService : IPersonService
	{
		private readonly PeopleManagerDbContext _database;

		public PersonService(PeopleManagerDbContext database)
		{
			_database = database;
		}

		public async Task<Person> GetAsync(int id)
		{
			var person = await _database.People
				.SingleOrDefaultAsync(p => p.Id == id);

			return person;
		}

		public async Task<IList<Person>> FindAsync()
		{
			var people = await _database.People
				.ToListAsync();

			return people;
		}


		public async Task<Person> CreateAsync(Person person)
		{
			_database.People.Add(person);
			await _database.SaveChangesAsync();

			return person;
		}

		public async Task<Person> UpdateAsync(int id, Person person)
		{
			var dbPerson = await GetAsync(id);

			if (dbPerson is null)
			{
				return null;
			}

			dbPerson.FirstName = person.FirstName;
			dbPerson.LastName = person.LastName;
			dbPerson.Email = person.Email;
			dbPerson.Age = person.Age;

			await _database.SaveChangesAsync();

			return dbPerson;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			var dbPerson = await GetAsync(id);

			if (dbPerson is null)
			{
				return false;
			}

			_database.People.Remove(dbPerson);
			await _database.SaveChangesAsync();

			return true;
		}
	}
}
