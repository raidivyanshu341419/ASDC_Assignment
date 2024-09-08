using AuthSystem.Areas.Identity.Data;
using AuthSystem.Data;
using AuthSystem.DbModel;
using AuthSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AuthSystem.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {

        private readonly AuthDbContext _context;
        private readonly ILogger<CustomerController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public CustomerController(ILogger<CustomerController> logger, AuthDbContext context, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var genders = _context.Genders.ToList();
                ViewBag.Genders = new SelectList(genders, "Id", "GenderName");
                var states = _context.States.ToList();
                ViewBag.States = new SelectList(states, "Id", "StateName");
                var customers = await _context.CustomerInfos
                    .Include(c => c.District)
                    .ThenInclude(d => d.State)
                    .Include(c => c.Gender)
                    .ToListAsync();

                return View(customers);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid && viewModel.customerId != 0)
                {
                    return Json(new
                    {
                        success = false,
                        errors = ModelState.ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        )
                    });
                }
                var custData = await _context.CustomerInfos.Where(x => x.CustomerId == viewModel.customerId).FirstOrDefaultAsync();
                if (custData != null)
                {
                    custData.DistrictId = viewModel.DistrictId;
                    custData.Name = viewModel.Name;
                    custData.GenderId = viewModel.GenderId;
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
                else
                {
                    // Check if GenderId exists in the database
                    var genderExists = _context.Genders.Any(g => g.Id == viewModel.GenderId);
                    if (!genderExists)
                    {
                        return Json(new
                        {
                            success = false,
                            errors = new { GenderId = new[] { "Invalid Gender ID." } }
                        });

                    }
                    // Check if DistrictId exists in the database
                    var districtExists = _context.Districts.Any(d => d.Id == viewModel.DistrictId);
                    if (!districtExists)
                    {
                        return Json(new
                        {
                            success = false,
                            errors = new { DistrictId = new[] { "Invalid District ID." } }
                        });
                    }
                    var customerInfo = new CustomerInfo
                    {
                        Name = viewModel.Name,
                        GenderId = viewModel.GenderId,
                        DistrictId = viewModel.DistrictId
                    };
                    _context.CustomerInfos.Add(customerInfo);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true });
                }
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                var customer = _context.CustomerInfos
                .Include(c => c.Gender)
                .Include(c => c.District)
                .ThenInclude(d => d.State)
                .SingleOrDefault(c => c.CustomerId == id);

                if (customer == null)
                {
                    return Json(new { success = false, message = "Customer not found" });
                }
                var viewModel = new EditCustomerViewModel
                {
                    Name = customer.Name,
                    GenderId = customer.GenderId,
                    StateId = customer.District.StateId,
                    DistrictId = customer.DistrictId,
                    CustomerId = customer.CustomerId
                };
                return Json(new { success = true, customer = viewModel });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var customerInfo = await _context.CustomerInfos.FindAsync(id);
                if (customerInfo == null)
                {
                    return NotFound();
                }
                _context.CustomerInfos.Remove(customerInfo);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult GetDropdowns()
        {
            try
            {
                var genders = _context.Genders
               .Select(g => new { g.Id, g.GenderName })
               .ToList();
                var states = _context.States
                    .Select(s => new { s.Id, s.StateName })
                    .ToList();

                return Json(new
                {
                    genders = new SelectList(genders, "Id", "GenderName"),
                    states = new SelectList(states, "Id", "StateName")
                });
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public IActionResult GetDistrictsByState(int stateId)
        {
            try
            {
                var districts = _context.Districts
               .Where(d => d.StateId == stateId)
               .Select(d => new { Id = d.Id, DistrictName = d.DistrictName })
               .ToList();

                return Json(districts);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private bool CustomerInfoExists(int id)
        {
            try
            {
                return _context.CustomerInfos.Any(e => e.CustomerId == id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
