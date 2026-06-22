using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagementSystem.Models
{
    /// <summary>
    /// this class is used to store a list of patients and provide methods to search for patients based on certain criteria. It contains a private list of patients and a constructor that takes an IEnumerable of patients as input. The Search method takes a filter function as input and returns an IEnumerable of patients that match the filter criteria.
    /// </summary>
    public class PatientRepository
    {
        private List<Patient> patients = new List<Patient>();

        public PatientRepository(IEnumerable<Patient> patients)
        {
            this.patients = patients.ToList();
        }
        /// <summary>
        /// This function takes a filter funcion as input and returns an IEnumerable of patients that match the filter criteria. The filter fucntion is a Func delegate that takes a Patient object as input and returns a boolean value indicating whether the patient matches the filter criteria. The method uses LINQ to filter the list of patients based on the provided filter function and returns the matching patients as an IEnumerable.
        /// </summary>
        ///<returns></returns>
        public IEnumerable<Patient> Search(Func<Patient, bool> filter)
        {
            return patients.Where(filter);
        }
    }
}