using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
 
        public class EmployeeController : Controller
        {
            private readonly ApplicationDbContext _context;
            private readonly ILogger<EmployeeController> _logger;

            public EmployeeController(
                ApplicationDbContext context,
                ILogger<EmployeeController> logger)
            {
                _context = context;
                _logger = logger;
            }

            // GET: Employee
            public async Task<IActionResult> Index()
            {
                try
                {
                    var employees = await _context.Employees
                        .AsNoTracking()
                        .ToListAsync();

                    return View(employees);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching employees.");

                    TempData["ErrorMessage"] = "Unable to load employees. Please try again.";

                    return View(new List<Employee>());
                }
            }

            // GET: Employee/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                try
                {
                    if (id == null)
                        return NotFound();

                    var employee = await _context.Employees
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (employee == null)
                        return NotFound();

                    return View(employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while fetching employee with Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] = "Unable to load employee details.";

                    return RedirectToAction(nameof(Index));
                }
            }

            // GET: Employee/Create
            public IActionResult Create()
            {
                return View();
            }

            // POST: Employee/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(Employee employee)
            {
                try
                {
                    if (!ModelState.IsValid)
                        return View(employee);

                    _context.Employees.Add(employee);

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Employee created successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while creating employee. Name: {Name}",
                        employee.Name);

                    TempData["ErrorMessage"] =
                        "Unable to create employee. Please try again.";

                    return View(employee);
                }
            }

            // GET: Employee/Edit/5
            public async Task<IActionResult> Edit(int? id)
            {
                try
                {
                    if (id == null)
                        return NotFound();

                    var employee = await _context.Employees
                        .FindAsync(id);

                    if (employee == null)
                        return NotFound();

                    return View(employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while loading employee for edit. Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] =
                        "Unable to load employee for editing.";

                    return RedirectToAction(nameof(Index));
                }
            }

            // POST: Employee/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, Employee employee)
            {
                try
                {
                    if (id != employee.Id)
                        return NotFound();

                    if (!ModelState.IsValid)
                        return View(employee);

                    _context.Employees.Update(employee);

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Employee updated successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    _logger.LogError(ex,
                        "Concurrency error while updating employee. Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] =
                        "The employee was modified by another user. Please refresh and try again.";

                    return View(employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while updating employee. Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] =
                        "Unable to update employee. Please try again.";

                    return View(employee);
                }
            }

            // GET: Employee/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                try
                {
                    if (id == null)
                        return NotFound();

                    var employee = await _context.Employees
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Id == id);

                    if (employee == null)
                        return NotFound();

                    return View(employee);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while loading employee for deletion. Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] =
                        "Unable to load employee.";

                    return RedirectToAction(nameof(Index));
                }
            }

            // POST: Employee/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> DeleteConfirmed(int id)
            {
                try
                {
                    var employee = await _context.Employees
                        .FindAsync(id);

                    if (employee == null)
                    {
                        TempData["ErrorMessage"] = "Employee not found.";
                        return RedirectToAction(nameof(Index));
                    }

                    _context.Employees.Remove(employee);

                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Employee deleted successfully.";

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex,
                        "Error occurred while deleting employee. Id: {EmployeeId}",
                        id);

                    TempData["ErrorMessage"] =
                        "Unable to delete employee. Please try again.";

                    return RedirectToAction(nameof(Index));
                }
            }
        }  
}
