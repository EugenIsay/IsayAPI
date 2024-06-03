using IsayAPI.Models;
using IsayAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Linq;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {

        private readonly User8Context _sensorsContext;
        public SensorController(User8Context context) 
        {
            _sensorsContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<SensorResponse>>> GetSensors()
        {
            //List<Sensor> sensors = new List<Sensor>();
            //_sensorsContext.SensorsMeasurements.Select(s => s.MeasurementFormula);
            //return await _sensorsContext.Sensors.Select(sensor => sensor).LeftJoin(_sensorsContext.SensorsMeasurements, s => s.SensorId, e => e.SensorId, (s, e) => sensors);
            return await _sensorsContext.Sensors.Select(sensor => sensor)
                .Join(_sensorsContext.SensorsMeasurements,
                s => s.SensorId,
                e => e.SensorId,
                (s, e) => new List<SensorResponse>
                {  
                    new SensorResponse { 
                        sensor_id = s.SensorId,
                        sensor_name = s.SensorName,
                        measurements = _sensorsContext.MeasurementsTypes.Where(u => u.TypeId == e.TypeId).ToList()
                    }

                }).ToListAsync();
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<Sensor>> GetSensorsItem(int id)
        {
            return await _sensorsContext.Sensors.FindAsync(id) ?? throw new InvalidOperationException();
        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<Sensor>>> DeleteSensor(int id)
        {
            Sensor? sensor = await _sensorsContext.Sensors.FindAsync(id);
            if (sensor != null) { _sensorsContext.Remove(sensor); }
            await _sensorsContext.SaveChangesAsync();
            return NoContent();

        }

        [HttpPost]
        public async Task<ActionResult<List<Sensor>>> AddSensor(Sensor sensor)
        {
            _sensorsContext.Add(sensor);
            await _sensorsContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpPut(template: "{id}, {sensor}")]
        public async Task<ActionResult<List<Sensor>>> UpdateSensor(int id, Sensor sensor)
        {
            Sensor need_sensor = await _sensorsContext.Sensors.FindAsync(id);
            if (sensor != null)
            {
                need_sensor.SensorName = sensor.SensorName;
                need_sensor.SensorId = sensor.SensorId;
                await _sensorsContext.SaveChangesAsync();
            }

            return NoContent();
        }
    }
}
