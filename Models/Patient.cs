using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientManagementSystem.Models
{
    /// <summary>
    /// Patient class represents a patient in the patient management system. It contains properties for the patient's medical number, name, address, and insurance policies. The class provides methods to validate the patient's information and retrieve display text for the patient.
    /// </summary>
    public class Patient : IDisposable
    {
        /// <summary>
        /// This is used to store the medical number of the patient. It acts as the primary key.
        /// </summary>
        public string medicalNumber { get; set; } = string.Empty;

        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;

        // Address
        /// <summary>
        /// City, state, and zip code of the patient.
        /// </summary>
        public string city { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string zipCode { get; set; } = string.Empty;

        // Insurance policy
        public List<PatientInsurancePolicy> InsurancePolicies { get; set; } = new();

        public Patient()
        {
        }

        public Patient(string medicalNumber, string firstName, string lastName, string city, string state, string zipCode, List<PatientInsurancePolicy> insurancePolicy)
        {
            this.medicalNumber = medicalNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.InsurancePolicies = insurancePolicy ?? new List<PatientInsurancePolicy>();
        }

        public string getAddress()
        {
            return this.city + " " + this.state + " " + this.zipCode;
        }

        public virtual bool IsMedicalNumberValid()
        {
            return !string.IsNullOrEmpty(this.medicalNumber) && this.medicalNumber.Length == 10;
        }

        public virtual bool IsNameValid()
        {
            return !string.IsNullOrEmpty(this.firstName) && !string.IsNullOrEmpty(this.lastName);
        }

        public virtual bool IsAddressValid()
        {
            return !string.IsNullOrWhiteSpace(city)
                && !string.IsNullOrWhiteSpace(state)
                && !string.IsNullOrWhiteSpace(zipCode);
        }

        public virtual bool isInsurancePolicyValid()
        {
            if (this.InsurancePolicies == null || this.InsurancePolicies.Count == 0)
            {
                return false;
            }

            if (this.InsurancePolicies.Any(policy =>
                policy == null ||
                string.IsNullOrWhiteSpace(policy.providerName) ||
                string.IsNullOrWhiteSpace(policy.policyNumber)))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// This method checks if the patient is valid by checking if the insurance policy, name, address, and medical number are all valid. It returns true if all of these are valid, and false otherwise.
        /// </summary>
        public virtual bool IsValid()
        {
            return this.isInsurancePolicyValid()
                && this.IsNameValid()
                && this.IsAddressValid()
                && this.IsMedicalNumberValid();
        }

        /// <summary>
        /// This method returns a string representation of the patient's information, including their name and insurance policy details.
        /// </summary>
        public virtual string GetDisplayText1()
        {
            string result = this.firstName + " " + this.lastName;
            if (this.InsurancePolicies != null && InsurancePolicies.Count > 0)
            {
                result += " - ";
                result += InsurancePolicies.First().providerName;
                result += " ";
                result += InsurancePolicies.First().policyNumber;
            }
            return result;
        }

        public virtual string GetDisplayText2()
        {
            string result = string.Format("{0} {1}", this.firstName, this.lastName);
            if (this.InsurancePolicies != null && InsurancePolicies.Count > 0)
            {
                result += string.Format(" - {0} {1}",
                    this.InsurancePolicies.First().providerName,
                    this.InsurancePolicies.First().policyNumber);
            }
            return result;
        }

        public virtual string GetDisplayText3()
        {
            string result = $"{this.firstName} {this.lastName}";
            if (this.InsurancePolicies != null && InsurancePolicies.Count > 0)
            {
                result += $" - {this.InsurancePolicies.First().providerName} {this.InsurancePolicies.First().policyNumber}";
            }
            return result;
        }

        public virtual void Dispose()
        {
            medicalNumber = null;
            firstName = null;
            lastName = null;
            city = null;
            state = null;
            zipCode = null;
            InsurancePolicies = null!;

            GC.SuppressFinalize(this);
        }

        ~Patient()
        {
            Dispose();
        }
    }
}