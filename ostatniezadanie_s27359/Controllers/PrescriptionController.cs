using Microsoft.AspNetCore.Mvc;
using ostatniezadanie_s27359.DTOs;
using ostatniezadanie_s27359.Services;

namespace ostatniezadanie_s27359.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;

        public PrescriptionController(IPrescriptionService prescriptionService)
        {
            _prescriptionService = prescriptionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePrescription([FromBody] CreatePrescriptionRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var prescriptionId = await _prescriptionService.CreatePrescriptionAsync(request);
                return Ok(new { PrescriptionId = prescriptionId, Message = "Prescription created successfully" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An internal server error occurred", Details = ex.Message });
            }
        }

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetPatientDetails(int patientId)
        {
            try
            {
                var patientDetails = await _prescriptionService.GetPatientDetailsAsync(patientId);
                
                if (patientDetails == null)
                {
                    return NotFound(new { Error = $"Patient with ID {patientId} not found" });
                }

                return Ok(patientDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = "An internal server error occurred", Details = ex.Message });
            }
        }
    }
} 