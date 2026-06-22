namespace PatientManagementSystem.Models
{
    public sealed class PatientInsurancePolicy
    {
        public string providerName { get; set; } = string.Empty;
        public string policyNumber { get; set; } = string.Empty;
        public PatientInsurancePolicy(string providerName, string policyNumber) {
           this.policyNumber = policyNumber;
           this.providerName = providerName;
        }
    }
}