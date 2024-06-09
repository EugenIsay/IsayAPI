using IsayAPI.Models.Request;
using IsayAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementsController : ControllerBase
    {
        private readonly User8Context _measurementContext;
        public MeasurementsController(User8Context context)
        {
            _measurementContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Measurement>>> GetMeasurements()
        {
            return await _measurementContext.Measurements.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<List<Measurement>>> AddConnection(List<MeasuremeantRequest> measurements)
        {
            foreach (var measuremeant in measurements)
            {
                _measurementContext.Measurements.Add(new Measurement
                {
                    MeasurementTs = measuremeant.MeasurementTs,
                    MeasurementType = measuremeant.MeasurementType,
                    MeasurementValue = measuremeant.MeasurementValue,
                    SensorInventoryNumber = measuremeant.SensorInventoryNumber,
                });
            }
            await _measurementContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete]
        public async Task<ActionResult<List<Measurement>>> DeleteConnection(List<MeasuremeantRequest> measurements)
        {
            foreach (var measuremeant in measurements)
            {
                _measurementContext.Measurements.Remove(new Measurement
                {
                    MeasurementTs = measuremeant.MeasurementTs,
                    MeasurementType = measuremeant.MeasurementType,
                    MeasurementValue = measuremeant.MeasurementValue,
                    SensorInventoryNumber = measuremeant.SensorInventoryNumber,
                });
            }
            await _measurementContext.SaveChangesAsync();
            return NoContent();

        }
    }
}