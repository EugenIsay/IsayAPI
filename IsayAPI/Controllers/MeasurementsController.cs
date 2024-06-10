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
        [HttpGet("/measurements")]
        public async Task<ActionResult<List<Measurement>>> GetMeasurements()
        {
            return await _measurementContext.Measurements.ToListAsync();
        }
        [HttpGet]
        public async Task<ActionResult<List<Measurement>>> GetMeasurementsWithValues(int? meteostation_id, int? inventory_num)
        {
            List<Measurement> answer = new List<Measurement>();
            if (meteostation_id != null)
            {
                var MetsSens = _measurementContext.MeteostationsSensors.Where(mt => mt.StationId == meteostation_id).ToList();
                var ids = _measurementContext.Measurements.Select(mt => mt.SensorInventoryNumber).ToList();
                foreach (var MetSen in MetsSens)
                {
                    if (ids.Contains(MetSen.SensorInventoryNumber))
                    {
                        List<Measurement> tmp = _measurementContext.Measurements
                            .Where(m => m.SensorInventoryNumber == MetSen.SensorInventoryNumber).ToList();
                        answer.AddRange(tmp);
                    }
                }
            }                
            else if (inventory_num != null)
            {
                answer = _measurementContext.Measurements.Where(m => m.SensorInventoryNumber == inventory_num).ToList();
            }
            return answer;
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
        public async Task<ActionResult<List<Measurement>>> DeleteConnection(int? Inventory_number, int? meteostation_id)
        {

            if (Inventory_number != null)
            {
                _measurementContext.Measurements.RemoveRange(_measurementContext.Measurements.Where(m => m.SensorInventoryNumber == Inventory_number));
            }
            else if (meteostation_id != null)
            {
                var MetsSens = _measurementContext.MeteostationsSensors.Where(mt => mt.StationId == meteostation_id).ToList();
                var ids = _measurementContext.Measurements.Select(mt => mt.SensorInventoryNumber).ToList();
                foreach (var MetSen in MetsSens)
                {
                    if (ids.Contains(MetSen.SensorInventoryNumber))
                    {
                        List<Measurement> tmp = _measurementContext.Measurements
                            .Where(m => m.SensorInventoryNumber == MetSen.SensorInventoryNumber).ToList();
                        _measurementContext.Measurements.RemoveRange(tmp);
                    }
                }
            }
            await _measurementContext.SaveChangesAsync();
            return NoContent();

        }
    }
}