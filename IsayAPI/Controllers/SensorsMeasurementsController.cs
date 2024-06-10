using IsayAPI.Models;
using IsayAPI.Models.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorsMeasurementsController : ControllerBase
    {
        private readonly User8Context _smContext;
        public SensorsMeasurementsController(User8Context context)
        {
            _smContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SensorsMeasurement>>> GetSensorMeasurement()
        {
            return await _smContext.SensorsMeasurements.Select(SensorsMeasurement => SensorsMeasurement).ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<List<SensorsMeasurement>>> AddSensorMeasurement(SensorMeasurementsRequest SensorMeasurement, int sensor_id)
        {
            SensorsMeasurement new_sm = new SensorsMeasurement { SensorId = sensor_id, TypeId = SensorMeasurement.TypeId, MeasurementFormula = SensorMeasurement.MeasurementFormula };
            _smContext.SensorsMeasurements.Add(new_sm);
            await _smContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<List<SensorsMeasurement>>> DeleteSensorMeasurement(int? sensor_id, int? measurement_type)
        {
            if (sensor_id != null)
            {
                _smContext.SensorsMeasurements.RemoveRange(_smContext.SensorsMeasurements.Where(sm => sm.SensorId == sensor_id));
            }
            else if (sensor_id != null)
            {
                _smContext.SensorsMeasurements.RemoveRange(_smContext.SensorsMeasurements.Where(sm => sm.SensorId == sensor_id));
            }
            await _smContext.SaveChangesAsync();
            return NoContent();
        }
    }
}