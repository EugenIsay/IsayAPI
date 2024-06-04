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
            var sensors = await _sensorsContext.Sensors.Select(s => new SensorResponse {sensor_id = s.SensorId, sensor_name = s.SensorName}).ToListAsync();
            foreach (var sensor in sensors) {
                sensor.measurements = await _sensorsContext.SensorsMeasurements.Where(mes => mes.SensorId == sensor.sensor_id).Join(
                    _sensorsContext.MeasurementsTypes,
                    e => e.TypeId,
                    m => m.TypeId,
                    (e, m) => new SensorMeasurementsResponse
                    {
                        MeasurementFormula = e.MeasurementFormula,
                        TypeName = m.TypeName,
                        TypeUnits = m.TypeUnits
                    }
                    ).ToListAsync();
            }
            return sensors;
        }
        [HttpGet(template: "{id}")]
        public async Task<ActionResult<SensorResponse>> GetSensorsItem(int id)
        {
            var sensor = new SensorResponse {sensor_id = _sensorsContext.Sensors.Find(id).SensorId, sensor_name = _sensorsContext.Sensors.Find(id).SensorName };
            sensor.measurements = await _sensorsContext.SensorsMeasurements.Where(mes => mes.SensorId == sensor.sensor_id).Join(
                    _sensorsContext.MeasurementsTypes,
                    e => e.TypeId,
                    m => m.TypeId,
                    (e, m) => new SensorMeasurementsResponse
                    {
                        MeasurementFormula = e.MeasurementFormula,
                        TypeName = m.TypeName,
                        TypeUnits = m.TypeUnits
                    }
                    ).ToListAsync();
            return sensor ?? throw new InvalidOperationException();

        }
        [HttpDelete(template: "{id}")]
        public async Task<ActionResult<List<Sensor>>> DeleteSensor(int id)
        {
            List<MeteostationsSensor> tmp = _sensorsContext.MeteostationsSensors.Where(s => s.SensorId == id).ToList();
            if (_sensorsContext.MeteostationsSensors.Where(s => s.SensorId == id).ToList().Count() != 0)
            {
                Sensor? sensor = await _sensorsContext.Sensors.FindAsync(id);
                if (sensor != null) { 
                    _sensorsContext.Sensors.Remove(sensor); 
                    foreach (var sm in _sensorsContext.SensorsMeasurements)
                    {
                        if (sm.SensorId == sensor.SensorId)
                        {
                            _sensorsContext.Remove(sm);
                        }
                    }
                }
            }
            await _sensorsContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<List<Sensor>>> AddSensor(SensorResponse sensor)
        {
            var TypeNames = _sensorsContext.MeasurementsTypes.Select(mt => mt.TypeName).ToList();
            foreach (var mes in sensor.measurements)
            {
                if (TypeNames.Contains(mes.TypeName))
                {
                    _sensorsContext.SensorsMeasurements.Add(new SensorsMeasurement { 
                        TypeId = Array.IndexOf(TypeNames.ToArray(), mes.TypeName),
                        SensorId = sensor.sensor_id,
                        MeasurementFormula = mes.MeasurementFormula
                    });;
                }
            }
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
