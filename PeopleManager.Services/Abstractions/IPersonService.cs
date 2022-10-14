using System.Collections.Generic;
using System.Threading.Tasks;
using PeopleManager.Model;

namespace PeopleManager.Services.Abstractions
{
	public interface IPersonService
	{
		Task<Person> GetAsync(int id);
		Task<IList<Person>> FindAsync();
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(int id, Person person);
        Task<bool> DeleteAsync(int id);
	}
}
