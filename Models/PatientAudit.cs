using System;
using System.ComponentModel.DataAnnotations;

namespace PatientManagementSystem.Models
{
    public class PatientAudit
    {
        public int Id { get; set; }

        [Required]
        public string PatientMedicalNumber { get; set; } = string.Empty;

        [Required]
        public string Action { get; set; } = string.Empty;

        [Required]
        public string FieldName { get; set; } = string.Empty;

        public string? OldValue { get; set; }

        public string? NewValue { get; set; }

        public string ChangedBy { get; set; } = string.Empty;

        public DateTime ChangedAt { get; set; } = DateTime.UtcNow; // Store the timestamp in UTC
    }
}