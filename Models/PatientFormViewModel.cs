using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Models
{
    public class PatientFormViewModel
    {
        public string medicalNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string firstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string lastName { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string city { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string state { get; set; } = string.Empty;

        [Required]
        [StringLength(10)]
        public string zipCode { get; set; } = string.Empty;

        public List<InsurancePolicyInputViewModel> InsurancePolicies { get; set; } = new();
    }

    public class InsurancePolicyInputViewModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100)]
        public string providerName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string policyNumber { get; set; } = string.Empty;
    }
}