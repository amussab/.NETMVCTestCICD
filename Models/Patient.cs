using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace PatientManagementSystem.Models
{
    /// <summary>
    /// Patient class represents a patient in the patient management system. It contains properties for the patient's medical number, name, address, and insurance policies. The class provides methods to validate the patient's information and retrieve display text for the patient.
    /// </summary>
    ///
    public class Patient : IDisposable
    {
        /// <summary>
        /// This is used to store the medical number of the patient. It is a string that can be null.
        /// </summary>
        public string? medicalNumber { get; set; } = string.Empty; //this shall act as a primary key for the patient, and it is a string that can be null.
        public string? firstName { get; set; }
        public string? lastName { get; set; }

        //Address
        /// <summary>
        /// City, state, and zip code of the patient. These properties are used to store the address information of the patient. They are all strings that can be null and are retreived as a full address all together using the getAddress() method.
        /// </summary>
        public string? city { get; set; }
        public string? state { get; set; }
        public string? zipCode { get; set; }

        //insurance policy
        public List<PatientInsurancePolicy> InsurancePolicies { get; set; } = new();
        public Patient() { }

        public Patient(string medicalNumber, string firstName, string lastName, string city, string state, string zipCode, List<PatientInsurancePolicy> insurancePolicy)
        {
            this.medicalNumber = medicalNumber;
            this.firstName = firstName;
            this.lastName = lastName;
            this.city = city;
            this.state = state;
            this.zipCode = zipCode;
            this.InsurancePolicies = insurancePolicy;
        }

        public string getAddress()
        {
            return this.city + " " + this.state + " " + this.zipCode;
        }

        public virtual bool IsMedicalNumberValid()
        {
            if (string.IsNullOrEmpty(this.medicalNumber) || this.medicalNumber.Length != 10)
            {
                return false;
            }
            else { return true; }
        }

        public virtual bool IsNameValid()
        {
            if (string.IsNullOrEmpty(this.firstName) || string.IsNullOrEmpty(this.lastName))
            {
                return false;
            }
            else { return true; }
        }

        public virtual bool IsAddressValid()
        {
            return !string.IsNullOrWhiteSpace(city) && !string.IsNullOrWhiteSpace(state) && !string.IsNullOrWhiteSpace(zipCode);
        }

        public virtual bool isInsurancePolicyValid()
        {
            if (this.InsurancePolicies == null || this.InsurancePolicies.Count == 0)
            {
                return false;
            }

            if (this.InsurancePolicies.Any(policy =>
                policy == null || string.IsNullOrWhiteSpace(policy.providerName) || string.IsNullOrWhiteSpace(policy.policyNumber)))
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
            return this.isInsurancePolicyValid() && this.IsNameValid() && this.IsAddressValid() && this.IsMedicalNumberValid();
        }

        /// <summary>
        /// This method returns a string representation of the patient's information, including their name and insurance policy details. It constructs a display text by combining the patient's first name, last name, and insurance policy information if available. The method checks if the insurance policies are not null and contain at least one entry before concatenating the insurance provider name and policy number to the display text. The resulting string provides a concise summary of the patient's identity and insurance coverage, which can be used for display purposes in the patient management system.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// This method returns a string representation of the patient's information, including their name and insurance policy details. It constructs a display text by combining the patient's first name, last name, and insurance policy information if available. The method checks if the insurance policies are not null and contain at least one entry before concatenating the insurance provider name and policy number to the display text. The resulting string provides a concise summary of the patient's identity and insurance coverage, which can be used for display purposes in the patient management system.
        /// </summary>
        /// <returns></returns>
        public virtual string GetDisplayText2()
        {
            string result = string.Format("{0} {1}", this.firstName, this.lastName);
            if (this.InsurancePolicies != null && InsurancePolicies.Count > 0)
            {
                result += string.Format(" - {0} {1}", this.InsurancePolicies.First().providerName, this.InsurancePolicies.First().policyNumber);
            }
            return result;
        }

        /// <summary>
        /// This method returns a string representation of the patient's information, including their name and insurance policy details. It constructs a display text by combining the patient's first name, last name, and insurance policy information if available. The method checks if the insurance policies are not null and contain at least one entry before concatenating the insurance provider name and policy number to the display text. The resulting string provides a concise summary of the patient's identity and insurance coverage, which can be used for display purposes in the patient management system.
        /// </summary>
        /// <returns></returns>
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
            // Dispose of unmanaged resources here if any, did that using setting the values to null.
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
            Dispose(); //apparently destructors are written as a backup only. The IDisposable interface is the preferred way of cleaning up resources.
        }
    }
}