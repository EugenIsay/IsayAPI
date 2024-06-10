using IsayAPI.Models;
using IsayAPI.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IsayAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeteostationsSensorsController : ControllerBase
    {
        private readonly User8Context _msContext;
        public MeteostationsSensorsController(User8Context context)
        {
            _msContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<MeteostationsSensorResponse>>> GetConnections()
        {
            var Connections = await _msContext.Meteostations.Select(s => new MeteostationsSensorResponse 
            { 
                StationId = s.StationId, 
                StationName = s.StationName,
                StationLatitude = s.StationLatitude,
                StationLongitude = s.StationLongitude
            }).ToListAsync();
            foreach (var Connection in Connections) 
            {
                Connection.sensors = await _msContext.MeteostationsSensors.Where(e => e.StationId == Connection.StationId)
                    .Join( _msContext.Sensors,
                    ms => ms.SensorId,
                    s => s.SensorId,
                    (ms, s) => new SensorResponseForMS
                    {
                        sensor_id = s.SensorId,
                        sensor_name = s.SensorName,
                        AddedTs = ms.AddedTs,
                        SensorInventoryNumber = ms.SensorInventoryNumber,
                        RemovedTs = ms.RemovedTs,
                    }
                    ).ToListAsync();
            }
            return Connections;
        }
        [HttpPost]
        public async Task<ActionResult<List<MeteostationsSensor>>> AddConnection (List<MSRequest> meteostations_sensors)
        {
            foreach (MSRequest ms in meteostations_sensors)
            {
                if (ms.added_ts == null)
                {
                    ms.added_ts = DateTime.UtcNow;
                }
                _msContext.MeteostationsSensors.Add(new MeteostationsSensor { SensorId = ms.sensor_id, StationId = ms.station_id, AddedTs = ms.added_ts });
            }
            await _msContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete]
        public async Task<ActionResult<List<MeteostationsSensor>>> DeleteSensor(int id, DateTime removed_ts)
        {

            MeteostationsSensor ms = await _msContext.MeteostationsSensors.FindAsync(id);
            if (removed_ts == null)
            {
                removed_ts = DateTime.UtcNow;
            }
            ms.RemovedTs = removed_ts;
            await _msContext.SaveChangesAsync();
            return NoContent();
        }
    }
}