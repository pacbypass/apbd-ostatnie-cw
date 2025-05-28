using ostatniezadanie_s27359.DTOs;

// w sumie niepotrzebne ale niech bedzie in the name of solid
namespace ostatniezadanie_s27359.Services
{
    public interface IPrescriptionService
    {
        Task<int> CreatePrescriptionAsync(CreatePrescriptionRequest request);
        Task<PatientDetailsResponse?> GetPatientDetailsAsync(int patientId);
    }
} 