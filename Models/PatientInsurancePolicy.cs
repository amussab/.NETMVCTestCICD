using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Models
{
    public sealed class PatientInsurancePolicy
    {
        public int Id { get; set; }   // primary key

        public string PatientMedicalNumber { get; set; } = string.Empty;
        [Required]
        [StringLength(100)]

        public string providerName { get; set; } = string.Empty;
        [Required]
        [StringLength(50)]
        public string policyNumber { get; set; } = string.Empty;

        public Patient? Patient { get; set; }

        public PatientInsurancePolicy()
        {
        }

        public PatientInsurancePolicy(string providerName, string policyNumber)
        {
            this.policyNumber = policyNumber;
            this.providerName = providerName;
        }
    }
}