using Microsoft.AspNetCore.Mvc;
using SchoolApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;


namespace SchoolApp.Controllers
{
    public class PeopleController : Controller
    {
        private readonly Ex51SchoolDatabaseContext _dbContext;

        public PeopleController(Ex51SchoolDatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            try
            {
                var peopleQuery = from p in _dbContext.People
                                  orderby p.Idpeople
                                  select new Person
                                  {
                                      Idpeople = p.Idpeople,
                                      FirstName = p.FirstName,
                                      LastName = p.LastName,
                                      BirthDate = p.BirthDate,
                                      RolesNavigation = p.RolesNavigation,
                                  };

                if (!String.IsNullOrEmpty(searchString))
                {
                    peopleQuery = peopleQuery.Where(p =>
                        EF.Functions.Like(p.FirstName, $"%{searchString}%") ||
                        EF.Functions.Like(p.LastName, $"%{searchString}%")
                    );
                }

                int pageSize = 10;
                int pageNumber = (page ?? 1);

                var paginatedPeople = await peopleQuery
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                ViewBag.TotalPages = (int)Math.Ceiling((double)peopleQuery.Count() / pageSize);
                ViewBag.CurrentPage = pageNumber;
                ViewBag.CurrentFilter = searchString;

                return View(paginatedPeople);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var personDetails = await _dbContext.People
                    .Include(p => p.RolesNavigation)
                    .FirstOrDefaultAsync(p => p.Idpeople == id);

                if (personDetails != null)
                {
                    return View(personDetails);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            try
            {
                var roles = _dbContext.Roles.ToList();
                ViewBag.Roles = new SelectList(roles, "Idrole", "Label");

                return View();
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(Person addperson)
        {
            try
            {
                var person = new Person()
                {
                    FirstName = addperson.FirstName,
                    LastName = addperson.LastName,
                    BirthDate = addperson.BirthDate,
                    Roles = addperson.Roles,
                };

                await _dbContext.People.AddAsync(person);
                await _dbContext.SaveChangesAsync();

                TempData["SuccessMessage"] = "Person added successfully!";

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the person.";
                return View();
            }
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _dbContext.People
                .Include(p => p.RolesNavigation)
                .FirstOrDefaultAsync(p => p.Idpeople == id);

            var roles = _dbContext.Roles.ToList();
            ViewBag.Roles = new SelectList(roles, "Idrole", "Label");

            if (person != null)
            {
                var viewModel = new Person()
                {
                    Idpeople = person.Idpeople,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    BirthDate = person.BirthDate,
                    Roles = person.Roles,
                };

                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Person updatePerson)
        {

            try
            {

                var person = await _dbContext.People.FindAsync(updatePerson.Idpeople);

                if (person != null)
                {
                    person.FirstName = updatePerson.FirstName;
                    person.LastName = updatePerson.LastName;
                    person.BirthDate = updatePerson.BirthDate;
                    person.Roles = updatePerson.Roles;

                    await _dbContext.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Person updated successfully!";

                    return RedirectToAction(nameof(Index));
                }

            } 
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the person.";
                return View();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var personDetails = await _dbContext.People
                    .Include(p => p.RolesNavigation)
                    .FirstOrDefaultAsync(p => p.Idpeople == id);

                if (personDetails != null)
                {
                    return View(personDetails);
                }
            }
            catch (Exception ex)
            {
                return View("Error");
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
		public async Task<IActionResult> Delete(int id, Person person)
		{
			try
			{
				var personToDelete = await _dbContext.People.FindAsync(id);

				if (personToDelete != null)
				{
					_dbContext.People.Remove(personToDelete);

					await _dbContext.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Person deleted successfully!";

                    return RedirectToAction(nameof(Index));
				}

				return RedirectToAction(nameof(Index)); 
			}
			catch (Exception ex)
			{
                TempData["ErrorMessage"] = "An error occurred while deleting the person.";
                return View();
			}
		}


	}
}
