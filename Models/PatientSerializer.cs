using PatientManagementSystem.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PatientManagementSystem.Models
{
    public static class PatientExtension
    {
        /// <summary>
        /// this method serializes the patient object to a JSON string. It uses the JsonSerializer class from the System.Text.Json namespace to perform the serialization to convert the patient object into a JSON string representation. This allows for easy storage, transmission, or logging of patient data in a standardized format.
        /// </summary>
        public static string ToJSON(this Patient patient)
        {
            return JsonSerializer.Serialize(patient);
        }
    }
}