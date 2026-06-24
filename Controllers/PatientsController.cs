using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace PatientManagementSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Patients
        public async Task<IActionResult> Index(string searchString)
        {
            var patients = _context.Patients
                .Include(p => p.InsurancePolicies)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                patients = patients.Where(p =>
                    p.medicalNumber!.Contains(searchString));
            }

            return View(await patients.ToListAsync());
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
            var model = new PatientFormViewModel();
            model.InsurancePolicies.Add(new InsurancePolicyInputViewModel());
            return View(model);
        }

        // POST: Patients/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientFormViewModel model)
        {
            model.InsurancePolicies ??= new List<InsurancePolicyInputViewModel>();

            if (model.InsurancePolicies.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "At least one insurance policy is required.");
                model.InsurancePolicies.Add(new InsurancePolicyInputViewModel());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var patient = new Patient
            {
                medicalNumber = GenerateMedicalNumber(),
                firstName = model.firstName,
                lastName = model.lastName,
                city = model.city,
                state = model.state,
                zipCode = model.zipCode,
                InsurancePolicies = model.InsurancePolicies
                    .Select(p => new PatientInsurancePolicy
                    {
                        providerName = p.providerName,
                        policyNumber = p.policyNumber,
                        PatientMedicalNumber = string.Empty
                    })
                    .ToList()
            };

            foreach (var policy in patient.InsurancePolicies)
            {
                policy.PatientMedicalNumber = patient.medicalNumber;
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Patients/Edit/1234567890
        [HttpGet]
        public async Task<IActionResult> Edit(string? id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound();

            var patient = await _context.Patients
                .Include(p => p.InsurancePolicies)
                .FirstOrDefaultAsync(p => p.medicalNumber == id);

            if (patient == null)
                return NotFound();

            var model = new PatientFormViewModel
            {
                medicalNumber = patient.medicalNumber,
                firstName = patient.firstName,
                lastName = patient.lastName,
                city = patient.city,
                state = patient.state,
                zipCode = patient.zipCode,
                InsurancePolicies = patient.InsurancePolicies
                    .Select(p => new InsurancePolicyInputViewModel
                    {
                        Id = p.Id,
                        providerName = p.providerName,
                        policyNumber = p.policyNumber
                    })
                    .ToList()
            };

            if (model.InsurancePolicies.Count == 0)
            {
                model.InsurancePolicies.Add(new InsurancePolicyInputViewModel());
            }

            return View(model);
        }

        // POST: Patients/Edit/1234567890
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PatientFormViewModel model)
        {
            if (id != model.medicalNumber)
                return NotFound();

            model.InsurancePolicies ??= new List<InsurancePolicyInputViewModel>();

            if (model.InsurancePolicies.Count == 0)
            {
                ModelState.AddModelError(string.Empty, "At least one insurance policy is required.");
                model.InsurancePolicies.Add(new InsurancePolicyInputViewModel());
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var existingPatient = await _context.Patients
                    .Include(p => p.InsurancePolicies)
                    .FirstOrDefaultAsync(p => p.medicalNumber == id);

                if (existingPatient == null)
                    return NotFound();

                existingPatient.firstName = model.firstName;
                existingPatient.lastName = model.lastName;
                existingPatient.city = model.city;
                existingPatient.state = model.state;
                existingPatient.zipCode = model.zipCode;

                _context.RemoveRange(existingPatient.InsurancePolicies);

                existingPatient.InsurancePolicies = model.InsurancePolicies
                    .Select(p => new PatientInsurancePolicy
                    {
                        providerName = p.providerName,
                        policyNumber = p.policyNumber,
                        PatientMedicalNumber = existingPatient.medicalNumber
                    })
                    .ToList();

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(model.medicalNumber))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Index));
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

        private string GenerateMedicalNumber()
        {
            string medicalNumber;

            do
            {
                medicalNumber = Guid.NewGuid().ToString("N")[..10].ToUpperInvariant();
            }
            while (_context.Patients.Any(p => p.medicalNumber == medicalNumber));

            return medicalNumber;
        }
    }
}