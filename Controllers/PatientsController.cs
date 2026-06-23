using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Models;

namespace PatientManagementSystem.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var patients = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .ToListAsync();

            return View(patients);
        }

        // GET: Patients/Details/1234567890
        public async Task<IActionResult> Details(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var patient = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .FirstOrDefaultAsync(p => p.medicalNumber == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Patient patient)
        {
            if (ModelState.IsValid)
            {
                if (patient.InsurancePolicies == null)
                    patient.InsurancePolicies = new List<PatientInsurancePolicy>();

                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }

        // GET: Patients/Edit/1234567890
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var patient = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .FirstOrDefaultAsync(p => p.medicalNumber == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: Patients/Edit/1234567890
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Patient patient)
        {
            if (id != patient.medicalNumber)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.medicalNumber!))
                        return NotFound();

                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(patient);
        }

        // GET: Patients/Delete/1234567890
        public async Task<IActionResult> Delete(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var patient = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .FirstOrDefaultAsync(p => p.medicalNumber == id);

            if (patient == null)
                return NotFound();

            return View(patient);
        }

        // POST: Patients/Delete/1234567890
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var patient = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .FirstOrDefaultAsync(p => p.medicalNumber == id);

            if (patient != null)
            {
                _context.Patients.Remove(patient);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(string id)
        {
            return _context.Patients.Any(e => e.medicalNumber == id);
        }
    }
}