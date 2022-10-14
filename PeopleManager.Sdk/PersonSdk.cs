using System.Net.Http.Json;
using PeopleManager.Model;

namespace PeopleManager.Sdk
{
    public class PersonSdk
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PersonSdk(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        
        public async Task<IList<Person>> Find()
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<IList<Person>>();
            if (people is null)
            {
                return new List<Person>();
            }
            return people;
        }
        
        public async Task<Person?> Get(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<Person>();
            
            return people;
        }

        public async Task<Person?> Create(Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = "/api/people";

            var response = await httpClient.PostAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<Person>();

            return people;
        }

        public async Task<Person?> Update(int id, Person person)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.PutAsJsonAsync(route, person);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<Person>();

            return people;
        }

        public async Task<bool> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient("PeopleManagerApi");
            var route = $"/api/people/{id}";

            var response = await httpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            var people = await response.Content.ReadFromJsonAsync<bool>();

            return people;
        }
    }
}