using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PeopleManager.Model;
using PeopleManager.Sdk;

namespace PeopleManager.Ui.Mvc.Controllers
{
	public class PeopleController : Controller
	{
        private readonly PersonSdk _personSdk;

        public PeopleController(PersonSdk personSdk)
		{
            _personSdk = personSdk;
		}

		public async Task<IActionResult> Index()
		{
			var people = await _personSdk.Find();
			return View(people);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Create(Person person)
		{
			if (!string.IsNullOrEmpty(person.FirstName) && person.FirstName.ToLower().Trim().Contains("john"))
			{
				ModelState.AddModelError("", "People with the name John are not welcome here!");
			}

			if (!ModelState.IsValid)
			{
				return View(person);
			}

			await _personSdk.Create(person);

			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			var person = await _personSdk.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(Person person)
		{
			if (!ModelState.IsValid)
			{
				return View(person);
			}

			await _personSdk.Update(person.Id, person);
			
			return RedirectToAction("Index");
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			var person = await _personSdk.Get(id);

			if (person is null)
			{
				return RedirectToAction("Index");
			}

			return View(person);
		}

		[HttpPost]
		[Route("People/Delete/{id}")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			await _personSdk.Delete(id);

			return RedirectToAction("Index");
		}
	}
}
